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

namespace HelpingHands_API.Controllers.v1
{
	[Route("api/v{version:apiVersion}/[Controller]/[Action]")]
	[ApiController]
    [ApiVersion("1.0")]

    public class ThirdCategoryAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ThirdCategoryAPIController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _response = new();
        }


		[HttpGet(Name = "GetThirdCategorys")]
		[ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetThirdCategorys([FromQuery(Name = "filterDisplayOrder")] int? Id,
           [FromQuery] string? search, int pageSize = 0, int pageNumber = 1)
        {
            try
            {
                IEnumerable<ThirdCategory> categoryList;

                if (Id > 0)
                {

                    categoryList = await _unitOfWork.ThirdCategory.GetAllAsync(u => u.Id == Id, includeProperties: "FirstCategory,SecondCategory", pageSize: pageSize,
                        pageNumber: pageNumber);
                }
                else
                {
                    categoryList = await _unitOfWork.ThirdCategory.GetAllAsync(includeProperties: "FirstCategory,SecondCategory", pageSize: pageSize,
                        pageNumber: pageNumber);
                }
                if (!string.IsNullOrEmpty(search))
                {
                    categoryList = categoryList.Where(u => u.ThirdCategoryName.ToLower().Contains(search) ||
                                                 u.FirstCategory.FirstCategoryName.ToLower().Contains(search)|| 
                                                 u.SecondCategory.SecondCategoryName.ToLower().Contains(search));

                }
                Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                _response.Result = _mapper.Map<List<ThirdCategoryDTO>>(categoryList);
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


        [HttpGet(Name = "ThirdCategoryByPagination")]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> ThirdCategoryByPagination(string term, string orderBy, int currentPage = 1)
        {
            try
            {
                term = string.IsNullOrEmpty(term) ? "" : term.ToLower();

                ThirdCategoryIndexVM thirdCategoryIndexVM = new ThirdCategoryIndexVM();
                IEnumerable<ThirdCategory> list = await _unitOfWork.ThirdCategory.GetAllAsync(includeProperties: "FirstCategory,SecondCategory");

                var List = _mapper.Map<List<ThirdCategoryDTO>>(list);

                thirdCategoryIndexVM.NameSortOrder = string.IsNullOrEmpty(orderBy) ? "countryName_desc" : "";

                if (!string.IsNullOrEmpty(term))
                {
                    List = List.Where(u => u.ThirdCategoryName.ToLower().Contains(term, StringComparison.OrdinalIgnoreCase)||
                    u.SecondCategory.SecondCategoryName.ToLower().Contains(term, StringComparison.OrdinalIgnoreCase) ||
                    u.FirstCategory.FirstCategoryName.ToLower().Contains(term, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                switch (orderBy)
                {
                    case "countryName_desc":
                        List = List.OrderByDescending(a => a.ThirdCategoryName).ToList();
                        break;

                    default:
                        List = List.OrderBy(a => a.ThirdCategoryName).ToList();
                        break;
                }
                int totalRecords = List.Count();
                int pageSize = 10;
                int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
                List = List.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                // current=1, skip= (1-1=0), take=5 
                // currentPage=2, skip (2-1)*5 = 5, take=5 ,
                thirdCategoryIndexVM.thirdCategories = List;
                thirdCategoryIndexVM.CurrentPage = currentPage;
                thirdCategoryIndexVM.TotalPages = totalPages;
                thirdCategoryIndexVM.Term = term;
                thirdCategoryIndexVM.PageSize = pageSize;
                thirdCategoryIndexVM.OrderBy = orderBy;

                _response.Result = _mapper.Map<ThirdCategoryIndexVM>(thirdCategoryIndexVM);
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


        [HttpGet("{id:int}", Name = "GetThirdCategory")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type =typeof(CategoryDTO))]
        //        [ResponseCache(Location =ResponseCacheLocation.None,NoStore =true)]
        public async Task<ActionResult<APIResponse>> GetThirdCategory(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var category = await _unitOfWork.ThirdCategory.GetAsync(u => u.Id == id, includeProperties: "FirstCategory,SecondCategory");
                if (category == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<ThirdCategoryDTO>(category);
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


		[HttpPost(Name = "CreateThirdCategory")]
		// [Authorize(Roles = "admin")]
		[ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateThirdCategory([FromBody] ThirdCategoryCreateDTO createDTO)
        {

            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return BadRequest(ModelState);
                //}
                if (await _unitOfWork.ThirdCategory.GetAsync(u => u.ThirdCategoryName.Trim().ToLower() == createDTO.ThirdCategoryName.Trim().ToLower() && u.SecondCategoryId == createDTO.SecondCategoryId) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Thirdcategory name already Exists!");
                    return BadRequest(ModelState);
                }
                if (await _unitOfWork.FirstCategory.GetAsync(u => u.Id == createDTO.FirstCategoryId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "FirstCategory ID is Invalid!");
                    return BadRequest(ModelState);
                }
                if (await _unitOfWork.SecondCategory.GetAsync(u => u.Id == createDTO.SecondCategoryId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "SecondCategory ID is Invalid!");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                ThirdCategory category = _mapper.Map<ThirdCategory>(createDTO);

                await _unitOfWork.ThirdCategory.CreateAsync(category);
                _response.Result = _mapper.Map<ThirdCategoryDTO>(category);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetThirdCategory", new { id = category.Id }, _response);
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
        [HttpDelete("{id:int}", Name = "DeleteThirdCategory")]
      
        public async Task<ActionResult<APIResponse>> DeleteThirdCategory(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var category = await _unitOfWork.ThirdCategory.GetAsync(u => u.Id == id);
                if (category == null)
                {
                    return NotFound();
                }
                await _unitOfWork.ThirdCategory.RemoveAsync(category);
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

        [HttpPut("{id:int}", Name = "UpdateThirdCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateThirdCategory(int id, [FromBody] ThirdCategoryUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }
                if (await _unitOfWork.ThirdCategory.GetAsync(u => u.ThirdCategoryName.ToLower() == updateDTO.ThirdCategoryName.ToLower() && u.SecondCategoryId == updateDTO.SecondCategoryId && u.Id != updateDTO.Id) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "ThirdCategory already Exists!");
                    return BadRequest(ModelState);
                }
                if (await _unitOfWork.FirstCategory.GetAsync(u => u.Id == updateDTO.FirstCategoryId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Category ID is Invalid!");
                    return BadRequest(ModelState);
                }
                if (await _unitOfWork.SecondCategory.GetAsync(u => u.Id == updateDTO.SecondCategoryId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Category ID is Invalid!");
                    return BadRequest(ModelState);
                }
                ThirdCategory model = _mapper.Map<ThirdCategory>(updateDTO);
                await _unitOfWork.ThirdCategory.UpdateAsync(model);
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

        //[HttpPatch("{id:int}", Name = "UpdatePartialThirdCategory")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> UpdatePartialThirdCategory(int id, JsonPatchDocument<ThirdCategoryUpdateDTO> patchDTO)
        //{
        //    if (patchDTO == null || id == 0)
        //    {
        //        return BadRequest();
        //    }
        //    var category = await _unitOfWork.ThirdCategory.GetAsync(u => u.Id == id, tracked: false);

        //    ThirdCategoryUpdateDTO categoryDTO = _mapper.Map<ThirdCategoryUpdateDTO>(category);


        //    if (category == null)
        //    {
        //        return BadRequest();
        //    }
        //    patchDTO.ApplyTo(categoryDTO, ModelState);
        //    ThirdCategory model = _mapper.Map<ThirdCategory>(categoryDTO);

        //    await _unitOfWork.ThirdCategory.UpdateAsync(model);

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    return NoContent();
        //}


    }
}

