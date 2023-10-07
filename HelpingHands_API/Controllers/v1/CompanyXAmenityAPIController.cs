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
using System.Drawing;
using System.Net;
using System.Security.Claims;
using System.Text.Json;

namespace HelpingHands_API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/CompanyXAmenityAPI")]
    [ApiController]
    [ApiVersion("1.0")]

    public class CompanyXAmenityAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CompanyXAmenityAPIController(IUnitOfWork unitOfWork, IMapper mapper)
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
        public async Task<ActionResult<APIResponse>> GetCompanyXAmenitys([FromQuery(Name = "filterDisplayOrder")] int? Id,
           [FromQuery] string? search, int pageSize = 0, int pageNumber = 1)
        {
            try
            {
                IEnumerable<CompanyXAmenity> companyXAmenityList;

                if (Id > 0)
                {

                    companyXAmenityList = await _unitOfWork.CompanyXAmenity.GetAllAsync(u => u.Id == Id, includeProperties: "Company,Amenity", pageSize: pageSize,
                        pageNumber: pageNumber);
                }
                else
                {
                    companyXAmenityList = await _unitOfWork.CompanyXAmenity.GetAllAsync(includeProperties: "Company,Amenity", pageSize: pageSize,
                        pageNumber: pageNumber);
                }
                if (!string.IsNullOrEmpty(search))
                {
                    companyXAmenityList = companyXAmenityList.Where(u => u.Company.CompanyName.ToLower().Contains(search) ||
                                                 u.Amenity.AmenityName.ToLower().Contains(search));

                }
                Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                _response.Result = _mapper.Map<List<CompanyXAmenityDTO>>(companyXAmenityList);
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



        [HttpGet("{id:int}", Name = "GetCompanyXAmenity")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type =typeof(CategoryDTO))]
        //        [ResponseCache(Location =ResponseCacheLocation.None,NoStore =true)]
        public async Task<ActionResult<APIResponse>> GetCompanyXAmenity(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var category = await _unitOfWork.CompanyXAmenity.GetAsync(u => u.Id == id, includeProperties: "Amenity,Company");
                if (category == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<CompanyXAmenityDTO>(category);
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateCompanyXAmenity([FromBody] CompanyXAmenityVM createDTO)
        {
            try
            {
                List<Amenity> AmenityList = _mapper.Map<List<Amenity>>(createDTO.Amenitylist);
                // CarXColor carxcolor = _mapper.Map<CarXColor>(createDTO.CarXColor);
                Company company = _mapper.Map<Company>(createDTO.Company);

                await _unitOfWork.CompanyXAmenity.RemoveRangeAsync(x => x.CompanyId == company.Id,false);

                foreach (var amenityList in AmenityList)
                {

                    if (amenityList.IsCheked == true)
                    {
                        CompanyXAmenity cxa = new();
						cxa.CompanyId = company.Id;
						cxa.AmenityId = amenityList.Id;
                        await _unitOfWork.CompanyXAmenity.CreateAsync(cxa);
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
        [HttpDelete("{id:int}", Name = "DeleteCompanyXAmenity")]

        public async Task<ActionResult<APIResponse>> DeleteCompanyXAmenity(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var category = await _unitOfWork.CompanyXAmenity.GetAsync(u => u.Id == id);
                if (category == null)
                {
                    return NotFound();
                }
                await _unitOfWork.CompanyXAmenity.RemoveAsync(category);
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
    }
}

