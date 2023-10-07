using AutoMapper;
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

    public class SecondCategoryAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SecondCategoryAPIController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _response = new();
        }



        [HttpGet(Name = "SecondCategoryByPagination")]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> SecondCategoryByPagination(string term, string orderBy, int currentPage = 1)
        {
            try
            {
                term = string.IsNullOrEmpty(term) ? "" : term.ToLower();

                SecondCategoryIndexVM secondCategoryIndexVM = new SecondCategoryIndexVM();
                IEnumerable<SecondCategory> list = await _unitOfWork.SecondCategory.GetAllAsync(includeProperties: "FirstCategory");

                var List = _mapper.Map<List<SecondCategoryDTO>>(list);

                secondCategoryIndexVM.NameSortOrder = string.IsNullOrEmpty(orderBy) ? "countryName_desc" : "";

                if (!string.IsNullOrEmpty(term))
                {
                    List = List.Where(u => u.SecondCategoryName.ToLower().Contains(term, StringComparison.OrdinalIgnoreCase) ||
                   u.FirstCategory.FirstCategoryName.ToLower().Contains(term, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                switch (orderBy)
                {
                    case "countryName_desc":
                        List = List.OrderByDescending(a => a.SecondCategoryName).ToList();
                        break;

                    default:
                        List = List.OrderBy(a => a.SecondCategoryName).ToList();
                        break;
                }
                int totalRecords = List.Count();
                int pageSize = 10;
                int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
                List = List.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                // current=1, skip= (1-1=0), take=5 
                // currentPage=2, skip (2-1)*5 = 5, take=5 ,
                secondCategoryIndexVM.secondCategories = List;
                secondCategoryIndexVM.CurrentPage = currentPage;
                secondCategoryIndexVM.TotalPages = totalPages;
                secondCategoryIndexVM.Term = term;
                secondCategoryIndexVM.PageSize = pageSize;
                secondCategoryIndexVM.OrderBy = orderBy;

                _response.Result = _mapper.Map<SecondCategoryIndexVM>(secondCategoryIndexVM);
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

        [HttpGet(Name = "GetSecondCategorys")]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetSecondCategorys([FromQuery(Name = "filterDisplayOrder")] int? Id,
           [FromQuery] string? search, int pageSize = 0, int pageNumber = 1)
        {
            try
            {

                IEnumerable<SecondCategory> categoryList;

                if (Id > 0)
                {

                    categoryList = await _unitOfWork.SecondCategory.GetAllAsync(u => u.Id == Id, includeProperties: "FirstCategory", pageSize: pageSize,
                        pageNumber: pageNumber);
                }
                else
                {
                    categoryList = await _unitOfWork.SecondCategory.GetAllAsync(includeProperties: "FirstCategory", pageSize: pageSize,
                        pageNumber: pageNumber);
                }
                if (!string.IsNullOrEmpty(search))
                {
                    categoryList = categoryList.Where(u => u.SecondCategoryName.ToLower().Contains(search) ||
                                                 u.FirstCategory.FirstCategoryName.ToLower().Contains(search));

                }
                Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                _response.Result = _mapper.Map<List<SecondCategoryDTO>>(categoryList);
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

        [HttpGet(Name = "GetSecondCategoryByFirstCategory")]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetSecondCategoryByFirstCategory([FromQuery(Name = "filterDisplayOrder")] int? Id,
           [FromQuery] string? search, int pageSize = 0, int pageNumber = 1)
        {
            try
            {

                IEnumerable<SecondCategory> categoryList;

                if (Id > 0)
                {

                    categoryList = await _unitOfWork.SecondCategory.GetAllAsync(u => u.FirstCategoryId == Id, includeProperties: "FirstCategory", pageSize: pageSize,
                        pageNumber: pageNumber);
                }
                else
                {
                    categoryList = await _unitOfWork.SecondCategory.GetAllAsync(includeProperties: "FirstCategory", pageSize: pageSize,
                        pageNumber: pageNumber);
                }
                if (!string.IsNullOrEmpty(search))
                {
                    categoryList = categoryList.Where(u => u.SecondCategoryName.ToLower().Contains(search) ||
                                                 u.FirstCategory.FirstCategoryName.ToLower().Contains(search));

                }
                Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                _response.Result = _mapper.Map<List<SecondCategoryDTO>>(categoryList);
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



        [HttpGet("{id:int}", Name = "GetSecondCategory")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type =typeof(CategoryDTO))]
        //        [ResponseCache(Location =ResponseCacheLocation.None,NoStore =true)]
        public async Task<ActionResult<APIResponse>> GetSecondCategory(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var category = await _unitOfWork.SecondCategory.GetAsync(u => u.Id == id, includeProperties: "FirstCategory");
                if (category == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<SecondCategoryDTO>(category);
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


        [HttpPost(Name = "CreateSecondCategory")]
        // [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateSecondCategory([FromBody] SecondCategoryCreateDTO createDTO)
        {

            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return BadRequest(ModelState);
                //}
                if (await _unitOfWork.SecondCategory.GetAsync(u => u.SecondCategoryName.Trim().ToLower() == createDTO.SecondCategoryName.Trim().ToLower() && u.FirstCategoryId == createDTO.FirstCategoryId) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Secondcategory name already Exists!");
                    return BadRequest(ModelState);
                }
                if (await _unitOfWork.FirstCategory.GetAsync(u => u.Id == createDTO.FirstCategoryId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "FirstCategory ID is Invalid!");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                SecondCategory category = _mapper.Map<SecondCategory>(createDTO);

                await _unitOfWork.SecondCategory.CreateAsync(category);
                _response.Result = _mapper.Map<SecondCategoryDTO>(category);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetSecondCategory", new { id = category.Id }, _response);
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
        [HttpDelete("{id:int}", Name = "DeleteSecondCategory")]
      
        public async Task<ActionResult<APIResponse>> DeleteSecondCategory(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var category = await _unitOfWork.SecondCategory.GetAsync(u => u.Id == id);
                if (category == null)
                {
                    return NotFound();
                }
                await _unitOfWork.SecondCategory.RemoveAsync(category);
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

        [HttpPut("{id:int}", Name = "UpdateSecondCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateSecondCategory(int id, [FromBody] SecondCategoryUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }
                if (await _unitOfWork.SecondCategory.GetAsync(u => u.SecondCategoryName.ToLower() == updateDTO.SecondCategoryName.ToLower() && u.FirstCategoryId == updateDTO.FirstCategoryId && u.Id != updateDTO.Id) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "SecondCategory already Exists!");
                    return BadRequest(ModelState);
                }
                if (await _unitOfWork.FirstCategory.GetAsync(u => u.Id == updateDTO.FirstCategoryId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Category ID is Invalid!");
                    return BadRequest(ModelState);
                }
                SecondCategory model = _mapper.Map<SecondCategory>(updateDTO);
                await _unitOfWork.SecondCategory.UpdateAsync(model);
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

        [HttpPatch("{id:int}", Name = "UpdatePartialSecondCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialSecondCategory(int id, JsonPatchDocument<SecondCategoryUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var category = await _unitOfWork.SecondCategory.GetAsync(u => u.Id == id, tracked: false);

            SecondCategoryUpdateDTO categoryDTO = _mapper.Map<SecondCategoryUpdateDTO>(category);


            if (category == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(categoryDTO, ModelState);
            SecondCategory model = _mapper.Map<SecondCategory>(categoryDTO);

            await _unitOfWork.SecondCategory.UpdateAsync(model);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }


    }
}

