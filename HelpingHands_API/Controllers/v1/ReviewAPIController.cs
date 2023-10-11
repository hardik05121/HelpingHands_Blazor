using AutoMapper;using Azure;using Microsoft.AspNetCore.Authorization;using Microsoft.AspNetCore.Http.HttpResults;using Microsoft.AspNetCore.JsonPatch;using Microsoft.AspNetCore.Mvc;using Microsoft.EntityFrameworkCore;using System.Data;using System.Net;using System.Security.Claims;using System.Text.Json;using HelpingHands_Business.Repository.IRepostiory;
using HelpingHands_DataAccess;
using HelpingHands_Models;
using HelpingHands_Models.Index;using HelpingHands_Models.ViewModels;

namespace HelpingHands_API.Controllers.v1{
	[Route("api/v{version:apiVersion}/[Controller]/[Action]")]
	[ApiController]    [ApiVersion("1.0")]    public class ReviewAPIController : ControllerBase    {        protected APIResponse _response;        private readonly IUnitOfWork _unitOfWork;        private readonly IMapper _mapper;        public ReviewAPIController(IUnitOfWork unitOfWork, IMapper mapper)        {            _unitOfWork = unitOfWork;            _mapper = mapper;            _response = new();        }


		[HttpGet(Name = "GetReviews")]
		[ResponseCache(CacheProfileName = "Default30")]        [ProducesResponseType(StatusCodes.Status403Forbidden)]        [ProducesResponseType(StatusCodes.Status401Unauthorized)]        [ProducesResponseType(StatusCodes.Status200OK)]        public async Task<ActionResult<APIResponse>> GetReviews([FromQuery(Name = "filterDisplayOrder")] int? Id,           [FromQuery] string? search, int pageSize = 0, int pageNumber = 1)        {
            {
                try
                {
                    IEnumerable<Review> reviewList;

                    if (Id > 0)
                    {

                        reviewList = await _unitOfWork.Review.GetAllAsync(u => u.Id == Id, includeProperties: "Company,ApplicationUser", pageSize: pageSize,
                            pageNumber: pageNumber);
                    }
                    else
                    {
                        reviewList = await _unitOfWork.Review.GetAllAsync(includeProperties: "Company,ApplicationUser", pageSize: pageSize,
                            pageNumber: pageNumber);
                    }
                    if (!string.IsNullOrEmpty(search))
                    {
                        reviewList = reviewList.Where(u => u.Title.ToLower().Contains(search) ||
                                                     u.Company.CompanyName.ToLower().Contains(search));

                    }
                    Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };

                    Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                    _response.Result = _mapper.Map<List<ReviewDTO>>(reviewList);
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
        }



        [HttpGet(Name = "ReviewByPagination")]        [ResponseCache(CacheProfileName = "Default30")]        [ProducesResponseType(StatusCodes.Status403Forbidden)]        [ProducesResponseType(StatusCodes.Status401Unauthorized)]        [ProducesResponseType(StatusCodes.Status200OK)]        public async Task<ActionResult<APIResponse>> ReviewByPagination(string term, string orderBy, int currentPage = 1)        {            try            {
                term = string.IsNullOrEmpty(term) ? "" : term.ToLower();

                ReviewIndexVM reviewIndexVM = new ReviewIndexVM();
                IEnumerable<Review> list = await _unitOfWork.Review.GetAllAsync(includeProperties: "Company,ApplicationUser");

                var List = _mapper.Map<List<ReviewDTO>>(list);

                reviewIndexVM.NameSortOrder = string.IsNullOrEmpty(orderBy) ? "countryName_desc" : "";

                if (!string.IsNullOrEmpty(term))
                {
                    List = List.Where(u => u.Title.ToLower().Contains(term, StringComparison.OrdinalIgnoreCase) ||
                   u.Company.CompanyName.ToLower().Contains(term, StringComparison.OrdinalIgnoreCase) ||
                   u.ApplicationUser.UserName.ToLower().Contains(term, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                switch (orderBy)
                {
                    case "countryName_desc":
                        List = List.OrderByDescending(a => a.Title).ToList();
                        break;

                    default:
                        List = List.OrderBy(a => a.Title).ToList();
                        break;
                }
                int totalRecords = List.Count();
                int pageSize = 10;
                int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
                List = List.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                // current=1, skip= (1-1=0), take=5 
                // currentPage=2, skip (2-1)*5 = 5, take=5 ,
                reviewIndexVM.reviews = List;
                reviewIndexVM.CurrentPage = currentPage;
                reviewIndexVM.TotalPages = totalPages;
                reviewIndexVM.Term = term;
                reviewIndexVM.PageSize = pageSize;
                reviewIndexVM.OrderBy = orderBy;

                _response.Result = _mapper.Map<ReviewIndexVM>(reviewIndexVM);                _response.StatusCode = HttpStatusCode.OK;                return Ok(_response);            }            catch (Exception ex)            {                _response.IsSuccess = false;                _response.ErrorMessages                     = new List<string>() { ex.ToString() };            }            return _response;        }

        [HttpGet("{id:int}", Name = "GetReview")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetReview(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var review = await _unitOfWork.Review.GetAsync(u => u.Id == id, includeProperties: "Company,ApplicationUser");
                if (review == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<ReviewDTO>(review);
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

		//[Authorize(Roles = "admin")]
		[HttpPost(Name = "CreateReview")]
		[ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateReview([FromBody] ReviewCreateVM createDTO)
        {
            try
            {
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                if (await _unitOfWork.Review.GetAsync(u => u.ApplicationUserId == createDTO.Review.ApplicationUserId && u.CompanyId == createDTO.Company.Id) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "your Review On This Comapany already Exists!");
                    return BadRequest(ModelState);
                }
                Review review = _mapper.Map<Review>(createDTO.Review);
                review.CompanyId = createDTO.Company.Id;


                await _unitOfWork.Review.CreateAsync(review);
                _response.Result = _mapper.Map<ReviewDTO>(review);
                _response.StatusCode = HttpStatusCode.Created;
                return Ok(_response);
                // return CreatedAtRoute("GetReview", new { id = review.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        //[Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteReview")]
        public async Task<ActionResult<APIResponse>> DeleteReview(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var review = await _unitOfWork.Review.GetAsync(u => u.Id == id);
                if (review == null)
                {
                    return NotFound();
                }
                await _unitOfWork.Review.RemoveAsync(review);
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

        //[Authorize(Roles = "admin")]
        [HttpPut("{id:int}", Name = "UpdateReview")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateReview(int id, [FromBody] ReviewUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }
                //if (await _dbCategory.GetAsync(u => u.Id == updateDTO.CategoryId,false) == null)
                //{
                //	ModelState.AddModelError("ErrorMessages", "Category ID is Invalid!");
                //	return BadRequest(ModelState);
                //}
                Review model = _mapper.Map<Review>(updateDTO);


                await _unitOfWork.Review.UpdateAsync(model);
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


        [HttpPatch("{id:int}", Name = "UpdatePartialReview")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialReview(int id, JsonPatchDocument<ReviewUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var review = await _unitOfWork.Review.GetAsync(u => u.Id == id, tracked: false);

            ReviewUpdateDTO reviewDTO = _mapper.Map<ReviewUpdateDTO>(review);


            if (review == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(reviewDTO, ModelState);
            Review model = _mapper.Map<Review>(reviewDTO);

            await _unitOfWork.Review.UpdateAsync(model);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }

    }
}






