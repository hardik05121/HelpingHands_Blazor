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
    public class FirstCategoryAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FirstCategoryAPIController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _response = new();
        }


		[HttpGet(Name = "GetFirstCategorys")]
		[ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetFirstCategorys([FromQuery(Name = "filterDisplayOrder")] int? Id,
            [FromQuery] string search, int pageSize = 0, int pageNumber = 1)
        {
            try
            {
                IEnumerable<FirstCategory> categoryList;

                if (Id > 0)
                {
                  categoryList = await _unitOfWork.FirstCategory.GetAllAsync(u => u.Id == Id, pageSize: pageSize,
                        pageNumber: pageNumber);
                }
                else
                {
                     categoryList = await _unitOfWork.FirstCategory.GetAllAsync(pageSize: pageSize,
                        pageNumber: pageNumber);
                }
             
                if (!string.IsNullOrEmpty(search))
                {
                    categoryList = categoryList.Where(u => u.FirstCategoryName.ToLower().Contains(search));
                }
                Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                _response.Result = _mapper.Map<List<FirstCategoryDTO>>(categoryList);
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


        [HttpGet(Name = "FirstCategoryByPagination")]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> FirstCategoryByPagination(string term, string orderBy, int currentPage = 1)
        {
            try
            {
                term = string.IsNullOrEmpty(term) ? "" : term.ToLower();

                FirstCategoryIndexVM firstCategoryIndexVM = new FirstCategoryIndexVM();
                IEnumerable<FirstCategory> list = await _unitOfWork.FirstCategory.GetAllAsync();

                var List = _mapper.Map<List<FirstCategoryDTO>>(list);

                firstCategoryIndexVM.NameSortOrder = string.IsNullOrEmpty(orderBy) ? "countryName_desc" : "";

                if (!string.IsNullOrEmpty(term))
                {
                    List = List.Where(u => u.FirstCategoryName.ToLower().Contains(term, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                switch (orderBy)
                {
                    case "countryName_desc":
                        List = List.OrderByDescending(a => a.FirstCategoryName).ToList();
                        break;

                    default:
                        List = List.OrderBy(a => a.FirstCategoryName).ToList();
                        break;
                }
                int totalRecords = List.Count();
                int pageSize = 10;
                int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
                List = List.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                // current=1, skip= (1-1=0), take=5 
                // currentPage=2, skip (2-1)*5 = 5, take=5 ,
                firstCategoryIndexVM.firstCategories = List;
                firstCategoryIndexVM.CurrentPage = currentPage;
                firstCategoryIndexVM.TotalPages = totalPages;
                firstCategoryIndexVM.Term = term;
                firstCategoryIndexVM.PageSize = pageSize;
                firstCategoryIndexVM.OrderBy = orderBy;

                _response.Result = _mapper.Map<FirstCategoryIndexVM>(firstCategoryIndexVM);
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

        [HttpGet("{id:int}", Name = "GetFirstCategory")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type =typeof(CategoryDTO))]
        //        [ResponseCache(Location =ResponseCacheLocation.None,NoStore =true)]
        public async Task<ActionResult<APIResponse>> GetFirstCategory(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var category = await _unitOfWork.FirstCategory.GetAsync(u => u.Id == id);
                if (category == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<FirstCategoryDTO>(category);
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

        [HttpPost(Name = "CreateFirstCategory")]
       // [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateFirstCategory([FromBody] FirstCategoryCreateDTO createDTO)
        {

            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return BadRequest(ModelState);
                //}
                if (await _unitOfWork.FirstCategory.GetAsync(u => u.FirstCategoryName.Trim().ToLower() == createDTO.FirstCategoryName.Trim().ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "FirstCategory name already Exists!");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }
                //if (categoryDTO.Id > 0)
                //{
                //    return StatusCode(StatusCodes.Status500InternalServerError);
                //}
                FirstCategory category = _mapper.Map<FirstCategory>(createDTO);
                //Category model = new()
                //{
                //    Amenity = createDTO.Amenity,
                //    Details = createDTO.Details,
                //    ImageUrl = createDTO.ImageUrl,
                //    Name = createDTO.Name,
                //    Occupancy = createDTO.Occupancy,
                //    Rate = createDTO.Rate,
                //    Sqft = createDTO.Sqft
                //};
                await _unitOfWork.FirstCategory.CreateAsync(category);
                _response.Result = _mapper.Map<FirstCategoryDTO>(category);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetFirstCategory", new { id = category.Id }, _response);
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
        [HttpDelete("{id:int}", Name = "DeleteFirstCategory")]
       // [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> DeleteFirstCategory(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var category = await _unitOfWork.FirstCategory.GetAsync(u => u.Id == id);
                if (category == null)
                {
                    return NotFound();
                }
                await _unitOfWork.FirstCategory.RemoveAsync(category);
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
      //  [Authorize(Roles = "admin")]
        [HttpPut("{id:int}", Name = "UpdateFirstCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateFirstCategory(int id, [FromBody] FirstCategoryUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }
                if (await _unitOfWork.FirstCategory.GetAsync(u => u.FirstCategoryName.ToLower() == updateDTO.FirstCategoryName.ToLower() && u.Id != updateDTO.Id) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "FirstCategory name already Exists!");
                    return BadRequest(ModelState);
                }

                FirstCategory model = _mapper.Map<FirstCategory>(updateDTO);
                await _unitOfWork.FirstCategory.UpdateAsync(model);
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

        //[HttpPatch("{id:int}", Name = "UpdatePartialFirstCategory")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> UpdatePartialFirstCategory(int id, JsonPatchDocument<FirstCategoryUpdateDTO> patchDTO)
        //{
        //    if (patchDTO == null || id == 0)
        //    {
        //        return BadRequest();
        //    }
        //    var category = await _unitOfWork.FirstCategory.GetAsync(u => u.Id == id, tracked: false);

        //    FirstCategoryUpdateDTO categoryDTO = _mapper.Map<FirstCategoryUpdateDTO>(category);


        //    if (category == null)
        //    {
        //        return BadRequest();
        //    }
        //    patchDTO.ApplyTo(categoryDTO, ModelState);
        //    FirstCategory model = _mapper.Map<FirstCategory>(categoryDTO);

        //    await _unitOfWork.FirstCategory.UpdateAsync(model);

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    return NoContent();
        //}


    }
}
