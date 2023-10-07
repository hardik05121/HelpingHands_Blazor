using AutoMapper;
using HelpingHands_Business.Repository.IRepostiory;
using HelpingHands_DataAccess;
using HelpingHands_Models;
using HelpingHands_Models.Index;
using HelpingHands_Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;
using System.Security.Claims;
using System.Text.Json;

namespace HelpingHands_API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/CompanyXPaymentAPI")]
    [ApiController]
    [ApiVersion("1.0")]

    public class CompanyXPaymentAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CompanyXPaymentAPIController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetCompanyXPayments([FromQuery(Name = "filterDisplayOrder")] int? Id,
                 [FromQuery] string? search, int pageSize = 0, int pageNumber = 1)
        {
            try
            {
                IEnumerable<CompanyXPayment> companyXPaymentList;

                if (Id > 0)
                {

                    companyXPaymentList = await _unitOfWork.CompanyXPayment.GetAllAsync(u => u.Id == Id, includeProperties: "Company,Payment", pageSize: pageSize,
                        pageNumber: pageNumber);
                }
                else
                {
                    companyXPaymentList = await _unitOfWork.CompanyXPayment.GetAllAsync(includeProperties: "Company,Payment", pageSize: pageSize,
                        pageNumber: pageNumber);
                }
                if (!string.IsNullOrEmpty(search))
                {
                    companyXPaymentList = companyXPaymentList.Where(u => u.Company.CompanyName.ToLower().Contains(search) ||
                                                 u.Payment.PaymentName.ToLower().Contains(search));

                }
                Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                _response.Result = _mapper.Map<List<CompanyXPaymentDTO>>(companyXPaymentList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;

        }



        [HttpGet("{id:int}", Name = "GetCompanyXPayment")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type =typeof(CategoryDTO))]
        //        [ResponseCache(Location =ResponseCacheLocation.None,NoStore =true)]
        public async Task<ActionResult<APIResponse>> GetCompanyXPayment(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var category = await _unitOfWork.CompanyXPayment.GetAsync(u => u.Id == id, includeProperties: "Payment,Company");
                if (category == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<CompanyXPaymentDTO>(category);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [HttpPost]
        //[Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateCompanyXPayment([FromBody] CompanyXPaymentVM createDTO)
        {
            try
            {
                List<Payment> PaymentList = _mapper.Map<List<Payment>>(createDTO.Paymentlist);
               
                Company company = _mapper.Map<Company>(createDTO.Company);


                await _unitOfWork.CompanyXPayment.RemoveRangeAsync(x => x.CompanyId == company.Id, false);


                foreach (var paymentList in PaymentList)
                {

                    if (paymentList.IsActive == true)
                    {
                        CompanyXPayment cxp = new();
                        cxp.CompanyId = company.Id;
                        cxp.PaymentId = paymentList.Id;
                        await _unitOfWork.CompanyXPayment.CreateAsync(cxp);
                    }
                    else
                    {
                        continue;
                    }
                }
                _response.StatusCode = HttpStatusCode.Created;
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteCompanyXPayment")]

        public async Task<ActionResult<APIResponse>> DeleteCompanyXPayment(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var category = await _unitOfWork.CompanyXPayment.GetAsync(u => u.Id == id);
                if (category == null)
                {
                    return NotFound();
                }
                await _unitOfWork.CompanyXPayment.RemoveAsync(category);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        //[HttpPut(Name = "UpdateCompanyXPayment")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<APIResponse>> UpdateCompanyXPayment([FromBody] CompanyXPaymentUpdateVM updateDTO)
        //{

        //    try
        //    {
        //        List<Payment> Payments = _mapper.Map<List<Payment>>(updateDTO.PaymentList);

        //        CompanyXPayment companyXAmemity = _mapper.Map<CompanyXPayment>(updateDTO.CompanyXPayment);

        //        List<CompanyXPayment> companyXAmenitiesList = await _unitOfWork.CompanyXPayment.GetAllAsync(u => u.CompanyId == companyXAmemity.CompanyId, 
        //            includeProperties: "Payment,Company");


        //        foreach (var cxpList in companyXAmenitiesList)
        //        {
        //            CompanyXPayment cxpl = new();

        //            cxpl.Id = cxpList.Id;
        //            cxpl.CompanyId = cxpList.CompanyId;
        //            cxpl.PaymentId = cxpList.PaymentId;

        //            await _unitOfWork.CompanyXPayment.RemoveAsync(cxpList);
        //        }


        //        foreach (var paymentlist in Payments)
        //        {
        //            if (paymentlist.IsCheked == true)
        //            {

        //                CompanyXPayment cxp = new();

        //                cxp.CompanyId = companyXAmemity.CompanyId;
        //                cxp.PaymentId = paymentlist.Id;
        //                await _unitOfWork.CompanyXPayment.CreateAsync(cxp);
        //            }
        //            else
        //            {
        //                continue;
        //            }
        //        }
        //        _response.StatusCode = HttpStatusCode.Created;
        //        return _response;
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.ErrorMessages
        //             = new List<string>() { ex.ToString() };
        //    }
        //    return _response;
        //}

        //[HttpPatch("{id:int}", Name = "UpdatePartialCompanyXPayment")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> UpdatePartialCompanyXPayment(int id, JsonPatchDocument<CompanyXPaymentUpdateDTO> patchDTO)
        //{
        //    if (patchDTO == null || id == 0)
        //    {
        //        return BadRequest();
        //    }
        //    var category = await _unitOfWork.CompanyXPayment.GetAsync(u => u.Id == id, tracked: false);

        //    CompanyXPaymentUpdateDTO categoryDTO = _mapper.Map<CompanyXPaymentUpdateDTO>(category);


        //    if (category == null)
        //    {
        //        return BadRequest();
        //    }
        //    patchDTO.ApplyTo(categoryDTO, ModelState);
        //    CompanyXPayment model = _mapper.Map<CompanyXPayment>(categoryDTO);

        //    await _unitOfWork.CompanyXPayment.UpdateAsync(model);

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    return NoContent();
        //}

    }
}

