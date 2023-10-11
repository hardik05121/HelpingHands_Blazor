using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
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
	//var claimsIdentity = (ClaimsIdentity)User.Identity;
	//var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
	//string ApplicationUserId = userId;

	[Route("api/v{version:apiVersion}/[Controller]/[Action]")]
	[ApiController]
    [ApiVersion("1.0")]

    public class ApplicationRoleAPIController : ControllerBase
    {
        protected APIResponse _response;
		private readonly IUnitOfWork _unitOfWork;
 		private readonly IMapper _mapper;
        

        public ApplicationRoleAPIController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _response = new();
        }

        [HttpGet(Name = "GetApplicationRoles")]
        //[MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetApplicationRoles()
        {
            try
            {
                IEnumerable<ApplicationRole> applicationRoleList = await _unitOfWork.ApplicationRole.GetAllAsync(); /*_dbApplicationUser.GetAllAsync();*/
                _response.Result = _mapper.Map<List<ApplicationRoleDTO>>(applicationRoleList);
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

        [HttpGet(Name = "ApplicationRoleByPagination")]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> ApplicationRoleByPagination(string term, string orderBy, int currentPage = 1)
        {
            try
            {
                term = string.IsNullOrEmpty(term) ? "" : term.ToLower();

                ApplicationRoleIndexVM applicationRoleIndexVM = new ApplicationRoleIndexVM();
                IEnumerable<ApplicationRole> list = await _unitOfWork.ApplicationRole.GetAllAsync();

                var List = _mapper.Map<List<ApplicationRoleDTO>>(list);

                applicationRoleIndexVM.NameSortOrder = string.IsNullOrEmpty(orderBy) ? "countryName_desc" : "";

                if (!string.IsNullOrEmpty(term))
                {
                    List = List.Where(u => u.Name.ToLower().Contains(term)).ToList();
                }

                switch (orderBy)
                {
                    case "countryName_desc":
                        List = List.OrderByDescending(a => a.Name).ToList();
                        break;

                    default:
                        List = List.OrderBy(a => a.Name).ToList();
                        break;
                }
                int totalRecords = List.Count();
                int pageSize = 10;
                int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
                List = List.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                // current=1, skip= (1-1=0), take=5 
                // currentPage=2, skip (2-1)*5 = 5, take=5 ,
                applicationRoleIndexVM.applicationRoles = List;
                applicationRoleIndexVM.CurrentPage = currentPage;
                applicationRoleIndexVM.TotalPages = totalPages;
                applicationRoleIndexVM.Term = term;
                applicationRoleIndexVM.PageSize = pageSize;
                applicationRoleIndexVM.OrderBy = orderBy;

                _response.Result = _mapper.Map<ApplicationRoleIndexVM>(applicationRoleIndexVM);
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

        [HttpGet("{Id}", Name = "GetApplicationRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetApplicationRole(string Id)
        {
            try
            {
                //if (Id == 0)
                //{
                //    _response.StatusCode = HttpStatusCode.BadRequest;
                //    return BadRequest(_response);
                //}
                var applicationRole = await _unitOfWork.ApplicationRole.GetAsync(u => u.Id == Id);
                if (applicationRole == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<ApplicationRoleDTO>(applicationRole);
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

        [HttpPost(Name = "CreateApplicationRole")]
        //[Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateApplicationRole([FromBody] ApplicationRoleDTO createDTO)
        {

            try
            {

                if (await _unitOfWork.ApplicationRole.GetAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Role already Exists!");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                //ApplicationRole applicationRole = _mapper.Map<ApplicationRole>(createDTO);
                ApplicationRole applicationRole = new();
                applicationRole.Id = createDTO.Id;
                applicationRole.Name = createDTO.Name;
                applicationRole.NormalizedName = createDTO.Name.ToUpper();

                await _unitOfWork.ApplicationRole.CreateAsync(applicationRole);
                    _response.Result = _mapper.Map<ApplicationRoleDTO>(applicationRole);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetApplicationRole", new { id = applicationRole.Id }, _response);
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
        [HttpDelete("{id}", Name = "DeleteApplicationRole")]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> DeleteApplicationRole(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }
                var category = await _unitOfWork.ApplicationRole.GetAsync(u => u.Id == id);
                if (category == null)
                {
                    return NotFound();
                }
                await _unitOfWork.ApplicationRole.RemoveAsync(category);
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

        [HttpPut("{id}", Name = "UpdateApplicationRole")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateApplicationRole(string id, [FromBody] ApplicationRoleDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }
                if (await _unitOfWork.ApplicationRole.GetAsync(u => u.Name.ToLower() == updateDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Role already Exists!");
                    return BadRequest(ModelState);
                }

                ApplicationRole model = _mapper.Map<ApplicationRole>(updateDTO);
                await _unitOfWork.ApplicationRole.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                //return Ok(_response);
                return CreatedAtRoute("GetApplicationRole", new { id = updateDTO.Id }, _response);
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