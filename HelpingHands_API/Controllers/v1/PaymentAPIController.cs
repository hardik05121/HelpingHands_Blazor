using AutoMapper;
using HelpingHands_Business.Repository.IRepostiory;
using HelpingHands_DataAccess;
using HelpingHands_Models;
using HelpingHands_Models.Index;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using System.Text.Json;

namespace HelpingHands_API.Controllers.v1
{
	//[Route("api/[controller]")]
	[Route("api/v{version:apiVersion}/[Controller]/[Action]")]
	[ApiController]
    [ApiVersion("1.0")]
    public class PaymentAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PaymentAPIController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _response = new();
        }


		[HttpGet(Name = "GetPayments")]
		[ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetPayments([FromQuery(Name = "filterDisplayOrder")] int? Id,
            [FromQuery] string search, int pageSize = 0, int pageNumber = 1)
        {
            try
            {
                IEnumerable<Payment> paymentList = await _unitOfWork.Payment.GetAllAsync(pageSize: pageSize,
                        pageNumber: pageNumber);
             
                if (!string.IsNullOrEmpty(search))
                {
                    paymentList = paymentList.Where(u => u.PaymentName.ToLower().Contains(search));
                }
                Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                _response.Result = _mapper.Map<List<PaymentDTO>>(paymentList);
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


        [HttpGet(Name = "PaymentByPagination")]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> PaymentByPagination(string term, string orderBy, int currentPage = 1)
        {
            try
            {
                term = string.IsNullOrEmpty(term) ? "" : term.ToLower();

                PaymentIndexVM paymentIndexVM = new PaymentIndexVM();
                IEnumerable<Payment> list = await _unitOfWork.Payment.GetAllAsync();
                list.OrderBy(a => a.PaymentName).ToList();

                var List = _mapper.Map<List<PaymentDTO>>(list);

                paymentIndexVM.NameSortOrder = string.IsNullOrEmpty(orderBy) ? "countryName_desc" : "";

                if (!string.IsNullOrEmpty(term))
                {
                    List = List.Where(u => u.PaymentName.ToLower().Contains(term, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                switch (orderBy)
                {
                    case "countryName_desc":
                        List = List.OrderByDescending(a => a.PaymentName).ToList();
                        break;

                    default:
                        List = List.OrderBy(a => a.PaymentName).ToList();
                        break;
                }
                int totalRecords = List.Count();
                int pageSize = 10;
                int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
                List = List.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                // current=1, skip= (1-1=0), take=5 
                // currentPage=2, skip (2-1)*5 = 5, take=5 ,
                paymentIndexVM.payments = List;
                paymentIndexVM.CurrentPage = currentPage;
                paymentIndexVM.TotalPages = totalPages;
                paymentIndexVM.Term = term;
                paymentIndexVM.PageSize = pageSize;
                paymentIndexVM.OrderBy = orderBy;

                _response.Result = _mapper.Map<PaymentIndexVM>(paymentIndexVM);
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

        [HttpGet("{id:int}", Name = "GetPayment")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type =typeof(CategoryDTO))]
        //        [ResponseCache(Location =ResponseCacheLocation.None,NoStore =true)]
        public async Task<ActionResult<APIResponse>> GetPayment(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var payment = await _unitOfWork.Payment.GetAsync(u => u.Id == id);
                if (payment == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<PaymentDTO>(payment);
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

        [HttpPost(Name = "CreatePayment")]
       // [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreatePayment([FromBody] PaymentCreateDTO createDTO)
        {

            try
            {
                if (await _unitOfWork.Payment.GetAsync(u => u.PaymentName.Trim().ToLower() == createDTO.PaymentName.Trim().ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Payment name already Exists!");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }
              
                Payment payment = _mapper.Map<Payment>(createDTO);
            
                await _unitOfWork.Payment.CreateAsync(payment);
                _response.Result = _mapper.Map<PaymentDTO>(payment);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetPayment", new { id = payment.Id }, _response);
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
        [HttpDelete("{id:int}", Name = "DeletePayment")]
     //   [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> DeletePayment(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var payment = await _unitOfWork.Payment.GetAsync(u => u.Id == id);
                if (payment == null)
                {
                    return NotFound();
                }
                await _unitOfWork.Payment.RemoveAsync(payment);
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
    //    [Authorize(Roles = "admin")]
        [HttpPut("{id:int}", Name = "UpdatePayment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdatePayment(int id, [FromBody] PaymentUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }
                if (await _unitOfWork.Payment.GetAsync(u => u.PaymentName.ToLower() == updateDTO.PaymentName.ToLower() && u.Id != updateDTO.Id) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "PaymentName already Exists!");
                    return BadRequest(ModelState);
                }

                Payment model = _mapper.Map<Payment>(updateDTO);
                await _unitOfWork.Payment.UpdateAsync(model);
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

        [HttpPatch("{id:int}", Name = "UpdatePartialPayment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialPayment(int id, JsonPatchDocument<PaymentUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var payment = await _unitOfWork.Payment.GetAsync(u => u.Id == id, tracked: false);

            PaymentUpdateDTO paymentDTO = _mapper.Map<PaymentUpdateDTO>(payment);


            if (payment == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(paymentDTO, ModelState);
            Payment model = _mapper.Map<Payment>(paymentDTO);

            await _unitOfWork.Payment.UpdateAsync(model);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }


    }
}
