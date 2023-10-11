using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;
using System.Security.Claims;
using System.Text.Json;

using HelpingHands_Business.Repository.IRepostiory;
using HelpingHands_DataAccess;
using HelpingHands_Models;
using HelpingHands_Models.Index;
using HelpingHands_Models.ViewModels;

namespace HelpingHands_API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/CompanyXServiceAPI")]
    [ApiController]
    [ApiVersion("1.0")]

    public class CompanyXServiceAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CompanyXServiceAPIController(IUnitOfWork unitOfWork, IMapper mapper)
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
			public async Task<ActionResult<APIResponse>> GetCompanyXServices([FromQuery(Name = "filterDisplayOrder")] int? Id,
				   [FromQuery] string? search, int pageSize = 0, int pageNumber = 1)
			{
				try
				{
					IEnumerable<CompanyXService> companyXServiceList;

					if (Id > 0)
					{

					companyXServiceList = await _unitOfWork.CompanyXService.GetAllAsync(u => u.Id == Id, includeProperties: "Company,Service", pageSize: pageSize,
							pageNumber: pageNumber);
					}
					else
					{
					companyXServiceList = await _unitOfWork.CompanyXService.GetAllAsync(includeProperties: "Company,Service", pageSize: pageSize,
							pageNumber: pageNumber);
					}
					if (!string.IsNullOrEmpty(search))
					{
					companyXServiceList = companyXServiceList.Where(u => u.Company.CompanyName.ToLower().Contains(search) ||
													 u.Service.ServiceName.ToLower().Contains(search));

					}
					Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };

					Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
					_response.Result = _mapper.Map<List<CompanyXServiceDTO>>(companyXServiceList);
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



    	[HttpGet("{id:int}", Name = "GetCompanyXService")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type =typeof(CategoryDTO))]
        //        [ResponseCache(Location =ResponseCacheLocation.None,NoStore =true)]
        public async Task<ActionResult<APIResponse>> GetCompanyXService(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var category = await _unitOfWork.CompanyXService.GetAsync(u => u.Id == id, includeProperties: "Service,Company");
                if (category == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<CompanyXServiceDTO>(category);
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
		// [Authorize(Roles = "admin")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResponse>> CreateCompanyXService([FromBody] CompanyXServiceVM createDTO)
		{

			try
			{
                List<Service> ServiceList = _mapper.Map<List<Service>>(createDTO.ServiceList);
                // CarXColor carxcolor = _mapper.Map<CarXColor>(createDTO.CarXColor);
                Company company = _mapper.Map<Company>(createDTO.Company);


                await _unitOfWork.CompanyXService.RemoveRangeAsync(x => x.CompanyId == company.Id, false);



                foreach (var amenityList in ServiceList)
                {

                    if (amenityList.IsActive == true)
                    {
                        CompanyXService cxa = new();
                        cxa.CompanyId = company.Id;
                        cxa.ServiceId = amenityList.Id;
                        await _unitOfWork.CompanyXService.CreateAsync(cxa);
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
        [HttpDelete("{id:int}", Name = "DeleteCompanyXService")]
      
        public async Task<ActionResult<APIResponse>> DeleteCompanyXService(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var category = await _unitOfWork.CompanyXService.GetAsync(u => u.Id == id);
                if (category == null)
                {
                    return NotFound();
                }
                await _unitOfWork.CompanyXService.RemoveAsync(category);
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

