using AutoMapper;
using Azure;
using HelpingHands_Business.Repository.IRepostiory;
using HelpingHands_DataAccess;
using HelpingHands_Models;
using HelpingHands_Models.Index;
using HelpingHands_Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using System.Security.Claims;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HelpingHands_API.Controllers.v1
{

	[Route("api/v{version:apiVersion}/[Controller]/[Action]")]
	[ApiController]
    [ApiVersion("1.0")]

    public class ApplicationUserAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ApplicationUserAPIController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet(Name = "GetApplicationUsers")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetApplicationUsers()
        {
            try
            {
                IEnumerable<ApplicationUser> applicationUserList = await _unitOfWork.ApplicationUser.GetAllAsync();
                _response.Result = _mapper.Map<List<ApplicationUserDTO>>(applicationUserList);
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

        [HttpGet(Name = "ApplicationUserByPagination")]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> ApplicationUserByPagination(string term, string orderBy, int currentPage = 1)
        {
            try
            {
                term = string.IsNullOrEmpty(term) ? "" : term.ToLower();

                ApplicationUserIndexVM applicationUserIndexVM = new ApplicationUserIndexVM();
                IEnumerable<ApplicationUser> list = await _unitOfWork.ApplicationUser.GetAllAsync();

                var List = _mapper.Map<List<ApplicationUserDTO>>(list);

                applicationUserIndexVM.NameSortOrder = string.IsNullOrEmpty(orderBy) ? "countryName_desc" : "";

                if (!string.IsNullOrEmpty(term))
                {
                    List = List.Where(u => u.UserName.ToLower().Contains(term)).ToList();
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
                applicationUserIndexVM.applicationUsers = List;
                applicationUserIndexVM.CurrentPage = currentPage;
                applicationUserIndexVM.TotalPages = totalPages;
                applicationUserIndexVM.Term = term;
                applicationUserIndexVM.PageSize = pageSize;
                applicationUserIndexVM.OrderBy = orderBy;

                _response.Result = _mapper.Map<ApplicationUserIndexVM>(applicationUserIndexVM);
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

        [HttpGet("{Id}", Name = "GetApplicationUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetApplicationUser(string Id)
        {
            try
            {

                var applicationUser = await _unitOfWork.ApplicationUser.GetAsync(u => u.Id == Id);
                if (applicationUser == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<ApplicationUserDTO>(applicationUser);
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


        [HttpPost(Name = "CreateApplicationUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateApplicationUser([FromBody] ApplicationUserDTO createDTO)
        {

            try
            {

                if (await _unitOfWork.ApplicationUser.GetAsync(u => u.UserName.ToLower() == createDTO.UserName.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "User already Exists!");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }
                ApplicationUser applicationUser = _mapper.Map<ApplicationUser>(createDTO);

                await _unitOfWork.ApplicationUser.CreateAsync(applicationUser);

                _response.Result = _mapper.Map<ApplicationUserDTO>(applicationUser);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetApplicationUser", new { id = applicationUser.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut(Name = "UpdateApplicationUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> UpdateApplicationUser([FromBody] UserVM updateDTO)

        {
            try
            {
                List<ApplicationRoleDTO> RoleList = _mapper.Map<List<ApplicationRoleDTO>>(updateDTO.ApplicationRoleList);
                // CarXColor carxcolor = _mapper.Map<CarXColor>(createDTO.CarXColor);
                ApplicationUserDTO User = _mapper.Map<ApplicationUserDTO>(updateDTO.ApplicationUser);

                await _unitOfWork.ApplicationUserRole.RemoveRangeAsync(x => x.UserId == User.Id, false);

                foreach (var roleList in RoleList)
                {
                    if (roleList.IsChecked == true)
                    {
                        ApplicationUserRole applicationUserRole = new();
                        applicationUserRole.UserId = User.Id;
                        applicationUserRole.RoleId = roleList.Id;
                        await _unitOfWork.ApplicationUserRole.CreateAsync(applicationUserRole);
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
    }
}