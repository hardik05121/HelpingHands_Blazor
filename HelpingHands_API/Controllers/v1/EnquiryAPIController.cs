using AutoMapper;using Azure;using Microsoft.AspNetCore.Authorization;using Microsoft.AspNetCore.Http.HttpResults;using Microsoft.AspNetCore.JsonPatch;using Microsoft.AspNetCore.Mvc;using Microsoft.EntityFrameworkCore;using System.Data;using System.Net;using System.Security.Claims;using System.Text.Json;using HelpingHands_Business.Repository.IRepostiory;
using HelpingHands_DataAccess;
using HelpingHands_Models;
using HelpingHands_Models.Index;

namespace HelpingHands_API.Controllers.v1{
	[Route("api/v{version:apiVersion}/[Controller]/[Action]")]
	[ApiController]    [ApiVersion("1.0")]    [Authorize]    public class EnquiryAPIController : ControllerBase    {        protected APIResponse _response;        private readonly IUnitOfWork _unitOfWork;        private readonly IMapper _mapper;        public EnquiryAPIController(IUnitOfWork unitOfWork, IMapper mapper)        {            _unitOfWork = unitOfWork;            _mapper = mapper;            _response = new();        }


		[HttpGet(Name = "GetEnquirys")]
		[ResponseCache(CacheProfileName = "Default30")]        [ProducesResponseType(StatusCodes.Status403Forbidden)]        [ProducesResponseType(StatusCodes.Status401Unauthorized)]        [ProducesResponseType(StatusCodes.Status200OK)]        public async Task<ActionResult<APIResponse>> GetEnquirys([FromQuery(Name = "filterDisplayOrder")] int? Id,           [FromQuery] string? search, int pageSize = 0, int pageNumber = 1)        {            try            {                IEnumerable<Enquiry> enquiryList;                if (Id > 0)                {                    enquiryList = await _unitOfWork.Enquiry.GetAllAsync(u => u.Id == Id, includeProperties: "Company", pageSize: pageSize,                        pageNumber: pageNumber);                }                else                {                    enquiryList = await _unitOfWork.Enquiry.GetAllAsync(includeProperties: "Company", pageSize: pageSize,                        pageNumber: pageNumber);                }                if (!string.IsNullOrEmpty(search))                {                    enquiryList = enquiryList.Where(u => u.UserName.ToLower().Contains(search) ||                                                 u.Company.CompanyName.ToLower().Contains(search));                }                Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));                _response.Result = _mapper.Map<List<EnquiryDTO>>(enquiryList);                _response.StatusCode = HttpStatusCode.OK;                return Ok(_response);            }            catch (Exception ex)            {                _response.IsSuccess = false;                _response.ErrorMessages                     = new List<string>() { ex.ToString() };            }            return _response;        }


        [HttpGet(Name = "EnquiryByPagination")]        [ResponseCache(CacheProfileName = "Default30")]        [ProducesResponseType(StatusCodes.Status403Forbidden)]        [ProducesResponseType(StatusCodes.Status401Unauthorized)]        [ProducesResponseType(StatusCodes.Status200OK)]        public async Task<ActionResult<APIResponse>> EnquiryByPagination(string term, string orderBy, int currentPage = 1)        {            try            {
                term = string.IsNullOrEmpty(term) ? "" : term.ToLower();

                EnquiryIndexVM enquiryIndexVM = new EnquiryIndexVM();
                IEnumerable<Enquiry> list = await _unitOfWork.Enquiry.GetAllAsync(includeProperties: "Company");

                var List = _mapper.Map<List<EnquiryDTO>>(list);

                enquiryIndexVM.NameSortOrder = string.IsNullOrEmpty(orderBy) ? "countryName_desc" : "";

                if (!string.IsNullOrEmpty(term))
                {
                    List = List.Where(u => u.UserName.ToLower().Contains(term, StringComparison.OrdinalIgnoreCase) ||
                                u.Company.CompanyName.ToLower().Contains(term, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                switch (orderBy)
                {
                    case "countryName_desc":
                        List = List.OrderByDescending(a => a.UserName).ToList();
                        break;

                    default:
                        List = List.OrderBy(a => a.UserName).ToList();
                        break;
                }
                int totalRecords = List.Count();
                int pageSize = 10;
                int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
                List = List.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                // current=1, skip= (1-1=0), take=5 
                // currentPage=2, skip (2-1)*5 = 5, take=5 ,
                enquiryIndexVM.enquiries = List;
                enquiryIndexVM.CurrentPage = currentPage;
                enquiryIndexVM.TotalPages = totalPages;
                enquiryIndexVM.Term = term;
                enquiryIndexVM.PageSize = pageSize;
                enquiryIndexVM.OrderBy = orderBy;

                _response.Result = _mapper.Map<EnquiryIndexVM>(enquiryIndexVM);                _response.StatusCode = HttpStatusCode.OK;                return Ok(_response);            }            catch (Exception ex)            {                _response.IsSuccess = false;                _response.ErrorMessages                     = new List<string>() { ex.ToString() };            }            return _response;        }        [HttpGet("{id:int}", Name = "GetEnquiry")]        [ProducesResponseType(StatusCodes.Status403Forbidden)]        [ProducesResponseType(StatusCodes.Status401Unauthorized)]        [ProducesResponseType(StatusCodes.Status200OK)]        [ProducesResponseType(StatusCodes.Status400BadRequest)]        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type =typeof(CategoryDTO))]
        //        [ResponseCache(Location =ResponseCacheLocation.None,NoStore =true)]
        public async Task<ActionResult<APIResponse>> GetEnquiry(int id)        {            try            {                if (id == 0)                {                    _response.StatusCode = HttpStatusCode.BadRequest;                    return BadRequest(_response);                }                var enquiry = await _unitOfWork.Enquiry.GetAsync(u => u.Id == id, includeProperties: "Company");                if (enquiry == null)                {                    _response.StatusCode = HttpStatusCode.NotFound;                    return NotFound(_response);                }                _response.Result = _mapper.Map<EnquiryDTO>(enquiry);                _response.StatusCode = HttpStatusCode.OK;                return Ok(_response);            }            catch (Exception ex)            {                _response.IsSuccess = false;                _response.ErrorMessages                     = new List<string>() { ex.ToString() };            }            return _response;        }        [HttpPost(Name = "CreateEnquiry")]
        // [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]        [ProducesResponseType(StatusCodes.Status400BadRequest)]        [ProducesResponseType(StatusCodes.Status500InternalServerError)]        public async Task<ActionResult<APIResponse>> CreateEnquiry([FromBody] EnquiryDTO createDTO)        {            try            {                //if (await _unitOfWork.Enquiry.GetAsync(u => u.UserName.ToLower() == createDTO.UserName.ToLower()) != null)                //{                //    ModelState.AddModelError("ErrorMessages", "Enquiry already Exists!");                //    return BadRequest(ModelState);                //}                //if (await _unitOfWork.Company.GetAsync(u => u.Id == createDTO.CompanyID) == null)                //{                //    ModelState.AddModelError("ErrorMessages", "CompanyID ID is Invalid!");                //    return BadRequest(ModelState);                //}                if (createDTO == null)                {                    return BadRequest(createDTO);                }                Enquiry enquiry = _mapper.Map<Enquiry>(createDTO);                await _unitOfWork.Enquiry.CreateAsync(enquiry);                _response.Result = _mapper.Map<EnquiryDTO>(enquiry);                _response.StatusCode = HttpStatusCode.Created;                return Ok(_response);            }            catch (Exception ex)            {                _response.IsSuccess = false;                _response.ErrorMessages                     = new List<string>() { ex.ToString() };            }            return _response;        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]        [ProducesResponseType(StatusCodes.Status403Forbidden)]        [ProducesResponseType(StatusCodes.Status401Unauthorized)]        [ProducesResponseType(StatusCodes.Status404NotFound)]        [ProducesResponseType(StatusCodes.Status400BadRequest)]        [HttpDelete("{id:int}", Name = "DeleteEnquiry")]

        public async Task<ActionResult<APIResponse>> DeleteEnquiry(int id)        {            try            {                if (id == 0)                {                    return BadRequest();                }                var amenity = await _unitOfWork.Enquiry.GetAsync(u => u.Id == id);                if (amenity == null)                {                    return NotFound();                }                await _unitOfWork.Enquiry.RemoveAsync(amenity);                _response.StatusCode = HttpStatusCode.NoContent;                _response.IsSuccess = true;                return Ok(_response);            }            catch (Exception ex)            {                _response.IsSuccess = false;                _response.ErrorMessages                     = new List<string>() { ex.ToString() };            }            return _response;        }    }}