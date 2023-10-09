using AutoMapper;
using Azure;
using HelpingHands_Business.Repository.IRepostiory;
using HelpingHands_DataAccess;
using HelpingHands_Models;
using HelpingHands_Models.Index;
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
	[Route("api/v{version:apiVersion}/[Controller]/[Action]")]
	[ApiController]
    [ApiVersion("1.0")]

    public class CompanyAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CompanyAPIController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _response = new();
        }


		[HttpGet(Name = "GetCompanys")]
		[ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetCompanys([FromQuery(Name = "filterDisplayOrder")] int? Id,
           [FromQuery] string? search, int pageSize = 0, int pageNumber = 1)
        {
            try
            {
                IEnumerable<Company> companyList;

                if (Id > 0)
                {

                    companyList = await _unitOfWork.Company.GetAllAsync(u => u.Id == Id, includeProperties: "FirstCategory,SecondCategory,ThirdCategory,Country,State,City", pageSize: pageSize,
                        pageNumber: pageNumber);
                }
                else
                {
                    companyList = await _unitOfWork.Company.GetAllAsync(includeProperties: "FirstCategory,SecondCategory,ThirdCategory,Country,State,City", pageSize: pageSize,
                        pageNumber: pageNumber);
                }
                if (!string.IsNullOrEmpty(search))
                {
                    companyList = companyList.Where(u => u.CompanyName.ToLower().Contains(search) ||
                                                 u.FirstCategory.FirstCategoryName.ToLower().Contains(search) ||
															   (u.SecondCategory != null && u.SecondCategory.SecondCategoryName.ToLower().Contains(search, StringComparison.OrdinalIgnoreCase)) ||
															(u.ThirdCategory != null && u.ThirdCategory.ThirdCategoryName.ToLower().Contains(search, StringComparison.OrdinalIgnoreCase)) ||
												 u.Country.CountryName.ToLower().Contains(search) ||
                                                 u.State.StateName.ToLower().Contains(search) ||
                                                 u.City.CityName.ToLower().Contains(search));
                }
                Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                _response.Result = _mapper.Map<List<CompanyDTO>>(companyList);
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

        [HttpGet(Name = "IndexByPagination")]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> CompanyByPagination(string term, string orderBy, int currentPage = 1)
        {
            try
            {
                term = string.IsNullOrEmpty(term) ? "" : term.ToLower();

                CompanyIndexVM companyIndexVM = new CompanyIndexVM();
                IEnumerable<Company> list = await _unitOfWork.Company.GetAllAsync(includeProperties: "FirstCategory,SecondCategory,ThirdCategory,Country,State,City");

                var List = _mapper.Map<List<CompanyDTO>>(list);

                companyIndexVM.NameSortOrder = string.IsNullOrEmpty(orderBy) ? "countryName_desc" : "";

                if (!string.IsNullOrEmpty(term))
                {
                    List = List.Where(u => u.CompanyName.ToLower().Contains(term, StringComparison.OrdinalIgnoreCase) ||
                                                 u.FirstCategory.FirstCategoryName.ToLower().Contains(term, StringComparison.OrdinalIgnoreCase) ||

                                                 //u.SecondCategory.SecondCategoryName.ToLower().Contains(term, StringComparison.OrdinalIgnoreCase) ||
															(u.SecondCategory != null && u.SecondCategory.SecondCategoryName.ToLower().Contains(term, StringComparison.OrdinalIgnoreCase)) ||
															(u.ThirdCategory != null && u.ThirdCategory.ThirdCategoryName.ToLower().Contains(term, StringComparison.OrdinalIgnoreCase)) ||
												// u.ThirdCategory.ThirdCategoryName.ToLower().Contains(term, StringComparison.OrdinalIgnoreCase) ||
                                                 u.Country.CountryName.ToLower().Contains(term, StringComparison.OrdinalIgnoreCase) ||
                                                 u.State.StateName.ToLower().Contains(term, StringComparison.OrdinalIgnoreCase) ||
                                                 u.City.CityName.ToLower().Contains(term, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                switch (orderBy)
                {
                    case "countryName_desc":
                        List = List.OrderByDescending(a => a.CompanyName).ToList();
                        break;

                    default:
                        List = List.OrderBy(a => a.CompanyName).ToList();
                        break;
                }
                int totalRecords = List.Count();
                int pageSize = 10;
                int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
                List = List.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                // current=1, skip= (1-1=0), take=5 
                // currentPage=2, skip (2-1)*5 = 5, take=5 ,
                companyIndexVM.companies = List;
                companyIndexVM.CurrentPage = currentPage;
                companyIndexVM.TotalPages = totalPages;
                companyIndexVM.Term = term;
                companyIndexVM.PageSize = pageSize;
                companyIndexVM.OrderBy = orderBy;

                _response.Result = _mapper.Map<CompanyIndexVM>(companyIndexVM);
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

        [HttpGet(Name = "CompanySearchByLazyLoading")]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> CompanySearchByLazyLoading(int pageNum, string searchText)
        {
            try
            {
                const int RecordsPerPage = 4;

                IEnumerable<Company> companyList = await _unitOfWork.Company.GetAllAsync(includeProperties: "FirstCategory,SecondCategory,ThirdCategory,Country,State,City");

                if (!string.IsNullOrEmpty(searchText))
                {
                    companyList = companyList.Where(u => u.CompanyName.ToLower().Contains(searchText.ToLower(), StringComparison.OrdinalIgnoreCase) ||
                                                 u.FirstCategory.FirstCategoryName.ToLower().Contains(searchText.ToLower(), StringComparison.OrdinalIgnoreCase) ||
                                                 u.Country.CountryName.ToLower().Contains(searchText.ToLower(), StringComparison.OrdinalIgnoreCase) ||
                                                 u.State.StateName.ToLower().Contains(searchText.ToLower(), StringComparison.OrdinalIgnoreCase) ||
                                                 u.City.CityName.ToLower().Contains(searchText.ToLower(), StringComparison.OrdinalIgnoreCase));
                }
                int skip = pageNum * RecordsPerPage;
                var tempList = companyList.Skip(skip).Take(RecordsPerPage).ToList();

                if (pageNum == 0 && tempList.Count == 0)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages
                         = new List<string>() { "Data does not exists" };
                }
                else
                {
                    _response.Result = _mapper.Map<List<CompanyDTO>>(tempList);

                    _response.StatusCode = HttpStatusCode.OK;
                    return Ok(_response);
                }

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("{id:int}", Name = "GetCompany")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type =typeof(CategoryDTO))]
        //        [ResponseCache(Location =ResponseCacheLocation.None,NoStore =true)]
        public async Task<ActionResult<APIResponse>> GetCompany(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var company = await _unitOfWork.Company.GetAsync(u => u.Id == id, includeProperties: "FirstCategory,SecondCategory,ThirdCategory,Country,State,City");
                if (company == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<CompanyDTO>(company);
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


        [HttpPost(Name = "CreateCompany")]
        // [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateCompany([FromBody] CompanyDTO createDTO)
        {

            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return BadRequest(ModelState);
                //}
                if (await _unitOfWork.Company.GetAsync(u => u.CompanyName.Trim().ToLower() == createDTO.CompanyName.Trim().ToLower()  && u.FirstCategoryId == createDTO.FirstCategoryId) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Company name already Exists!");
                    return BadRequest(ModelState);
                }
                if (await _unitOfWork.FirstCategory.GetAsync(u => u.Id == createDTO.FirstCategoryId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "FirstCategory ID is Invalid!");
                    return BadRequest(ModelState);
                }
                //if (await _unitOfWork.SecondCategory.GetAsync(u => u.Id == createDTO.SecondCategoryId) == null)
                //{
                //    ModelState.AddModelError("ErrorMessages", "SecondCategory ID is Invalid!");
                //    return BadRequest(ModelState);
                //}
                //if (await _unitOfWork.ThirdCategory.GetAsync(u => u.Id == createDTO.ThirdCategoryId) == null)
                //{
                //    ModelState.AddModelError("ErrorMessages", "ThirdCategory ID is Invalid!");
                //    return BadRequest(ModelState);
                //}
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                Company company = _mapper.Map<Company>(createDTO);

                await _unitOfWork.Company.CreateAsync(company);
                _response.Result = _mapper.Map<CompanyDTO>(company);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetCompany", new { id = company.Id }, _response);
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
        [HttpDelete("{id:int}", Name = "DeleteCompany")]

        public async Task<ActionResult<APIResponse>> DeleteCompany(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var company = await _unitOfWork.Company.GetAsync(u => u.Id == id);
                if (company == null)
                {
                    return NotFound();
                }
                await _unitOfWork.Company.RemoveAsync(company);
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

        [HttpPut("{id:int}", Name = "UpdateCompany")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateCompany(int id, [FromBody] CompanyDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }
                if (await _unitOfWork.Company.GetAsync(u => u.CompanyName.ToLower() == updateDTO.CompanyName.ToLower() && u.FirstCategoryId == updateDTO.FirstCategoryId && u.Id != updateDTO.Id) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Company already Exists!");
                    return BadRequest(ModelState);
                }
                if (await _unitOfWork.FirstCategory.GetAsync(u => u.Id == updateDTO.FirstCategoryId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "FirstCategory ID is Invalid!");
                    return BadRequest(ModelState);
                }
                //if (await _unitOfWork.SecondCategory.GetAsync(u => u.Id == updateDTO.SecondCategoryId) == null)
                //{
                //    ModelState.AddModelError("ErrorMessages", "SecondCategory ID is Invalid!");
                //    return BadRequest(ModelState);
                //}
                //if (await _unitOfWork.ThirdCategory.GetAsync(u => u.Id == updateDTO.ThirdCategoryId) == null)
                //{
                //    ModelState.AddModelError("ErrorMessages", "ThirdCategory ID is Invalid!");
                //    return BadRequest(ModelState);
                //}
                Company model = _mapper.Map<Company>(updateDTO);
                model.UpdatedDate = DateTime.Now;
                await _unitOfWork.Company.UpdateAsync(model);
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

        //[HttpPatch("{id:int}", Name = "UpdatePartialCompany")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> UpdatePartialCompany(int id, JsonPatchDocument<CompanyUpdateDTO> patchDTO)
        //{
        //    if (patchDTO == null || id == 0)
        //    {
        //        return BadRequest();
        //    }
        //    var company = await _unitOfWork.Company.GetAsync(u => u.Id == id, tracked: false);

        //    CompanyUpdateDTO companyDTO = _mapper.Map<CompanyUpdateDTO>(company);


        //    if (company == null)
        //    {
        //        return BadRequest();
        //    }
        //    patchDTO.ApplyTo(companyDTO, ModelState);
        //    Company model = _mapper.Map<Company>(companyDTO);

        //    await _unitOfWork.Company.UpdateAsync(model);

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    return NoContent();
        //}


    }
}

