﻿using AutoMapper;using Azure;using Microsoft.AspNetCore.Authorization;using Microsoft.AspNetCore.Http.HttpResults;using Microsoft.AspNetCore.JsonPatch;using Microsoft.AspNetCore.Mvc;using Microsoft.EntityFrameworkCore;using System.Data;using System.Linq;
using System.Net;using System.Security.Claims;using System.Text.Json;using HelpingHands_Business.Repository.IRepostiory;
using HelpingHands_DataAccess;
using HelpingHands_Models;
using HelpingHands_Models.Index;

namespace HelpingHands_API.Controllers.v1{
	[Route("api/v{version:apiVersion}/[Controller]/[Action]")]
	[ApiController]    [ApiVersion("1.0")]    public class StateAPIController : ControllerBase    {        protected APIResponse _response;        private readonly IUnitOfWork _unitOfWork;        private readonly IMapper _mapper;        public StateAPIController(IUnitOfWork unitOfWork, IMapper mapper)        {            _unitOfWork = unitOfWork;            _mapper = mapper;            _response = new();        }


		[HttpGet(Name = "GetStates")]
		[ResponseCache(CacheProfileName = "Default30")]        [ProducesResponseType(StatusCodes.Status403Forbidden)]        [ProducesResponseType(StatusCodes.Status401Unauthorized)]        [ProducesResponseType(StatusCodes.Status200OK)]        public async Task<ActionResult<APIResponse>> GetStates([FromQuery(Name = "filterDisplayOrder")] int? Id,           [FromQuery] string? search, int pageSize = 0, int pageNumber = 1)        {            try            {                IEnumerable<State> stateList;                if (Id > 0)                {                    stateList = await _unitOfWork.State.GetAllAsync(u => u.Id == Id, includeProperties: "Country", pageSize: pageSize,                        pageNumber: pageNumber);                }                else                {                    stateList = await _unitOfWork.State.GetAllAsync(includeProperties: "Country", pageSize: pageSize,                        pageNumber: pageNumber);                }                if (!string.IsNullOrEmpty(search))                {                    stateList = stateList.Where(u => u.StateName.ToLower().Contains(search) ||                                                 u.Country.CountryName.ToLower().Contains(search));                }                Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));                _response.Result = _mapper.Map<List<StateDTO>>(stateList);                _response.StatusCode = HttpStatusCode.OK;                return Ok(_response);            }            catch (Exception ex)            {                _response.IsSuccess = false;                _response.ErrorMessages                     = new List<string>() { ex.ToString() };            }            return _response;        }


        [HttpGet(Name = "StateByPagination")]        [ResponseCache(CacheProfileName = "Default30")]        [ProducesResponseType(StatusCodes.Status403Forbidden)]        [ProducesResponseType(StatusCodes.Status401Unauthorized)]        [ProducesResponseType(StatusCodes.Status200OK)]        public async Task<ActionResult<APIResponse>> StateByPagination(string term, string orderBy, int currentPage = 1)        {            try            {
                term = string.IsNullOrEmpty(term) ? "" : term.ToLower();

                StateIndexVM stateIndexVM = new StateIndexVM();
                IEnumerable<State> list = await _unitOfWork.State.GetAllAsync(includeProperties: "Country");

                var List = _mapper.Map<List<StateDTO>>(list);

                stateIndexVM.NameSortOrder = string.IsNullOrEmpty(orderBy) ? "countryName_desc" : "";

                if (!string.IsNullOrEmpty(term))
                {
                    List = List.Where(u => u.StateName.ToLower().Contains(term, StringComparison.OrdinalIgnoreCase) ||
                    u.Country.CountryName.ToLower().Contains(term, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                switch (orderBy)
                {
                    case "countryName_desc":
                        List = List.OrderByDescending(a => a.StateName).ToList();
                        break;

                    default:
                        List = List.OrderBy(a => a.StateName).ToList();
                        break;
                }
                int totalRecords = List.Count();
                int pageSize = 10;
                int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
                List = List.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                // current=1, skip= (1-1=0), take=5 
                // currentPage=2, skip (2-1)*5 = 5, take=5 ,
                stateIndexVM.states = List;
                stateIndexVM.CurrentPage = currentPage;
                stateIndexVM.TotalPages = totalPages;
                stateIndexVM.Term = term;
                stateIndexVM.PageSize = pageSize;
                stateIndexVM.OrderBy = orderBy;

                _response.Result = _mapper.Map<StateIndexVM>(stateIndexVM);                _response.StatusCode = HttpStatusCode.OK;                return Ok(_response);            }            catch (Exception ex)            {                _response.IsSuccess = false;                _response.ErrorMessages                     = new List<string>() { ex.ToString() };            }            return _response;        }        [HttpGet("{id:int}", Name = "GetState")]        [ProducesResponseType(StatusCodes.Status403Forbidden)]        [ProducesResponseType(StatusCodes.Status401Unauthorized)]        [ProducesResponseType(StatusCodes.Status200OK)]        [ProducesResponseType(StatusCodes.Status400BadRequest)]        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type =typeof(CategoryDTO))]
        //        [ResponseCache(Location =ResponseCacheLocation.None,NoStore =true)]
        public async Task<ActionResult<APIResponse>> GetState(int id)        {            try            {                if (id == 0)                {                    _response.StatusCode = HttpStatusCode.BadRequest;                    return BadRequest(_response);                }                var state = await _unitOfWork.State.GetAsync(u => u.Id == id, includeProperties: "Country");                if (state == null)                {                    _response.StatusCode = HttpStatusCode.NotFound;                    return NotFound(_response);                }                _response.Result = _mapper.Map<StateDTO>(state);                _response.StatusCode = HttpStatusCode.OK;                return Ok(_response);            }            catch (Exception ex)            {                _response.IsSuccess = false;                _response.ErrorMessages                     = new List<string>() { ex.ToString() };            }            return _response;        }



		[HttpPost(Name = "CreateState")]
		// [Authorize(Roles = "admin")]
		[ProducesResponseType(StatusCodes.Status201Created)]        [ProducesResponseType(StatusCodes.Status400BadRequest)]        [ProducesResponseType(StatusCodes.Status500InternalServerError)]        public async Task<ActionResult<APIResponse>> CreateState([FromBody] StateCreateDTO createDTO)        {            try            {
                //if (!ModelState.IsValid)
                //{
                //    return BadRequest(ModelState);
                //}
                var existingState = await _unitOfWork.State.GetAsync(u => u.StateName.Trim().ToLower() == createDTO.StateName.Trim().ToLower() && u.CountryId == createDTO.CountryId);

                if (existingState != null)
                {
                        ModelState.AddModelError("ErrorMessages", "State name already Exists!");
                        return BadRequest(ModelState);
                }

                //if (await _unitOfWork.State.GetAsync(u => u.StateName.ToLower() == createDTO.StateName.ToLower() && u.CountryId == createDTO.CountryId) != null)
                //{
                //    ModelState.AddModelError("ErrorMessages", "State already Exists!");
                //    return BadRequest(ModelState);
                //}


                if (await _unitOfWork.Country.GetAsync(u => u.Id == createDTO.CountryId) == null)                {                    ModelState.AddModelError("ErrorMessages", "CountryId ID is Invalid!");                    return BadRequest(ModelState);                }                if (createDTO == null)                {                    return BadRequest(createDTO);                }                State state = _mapper.Map<State>(createDTO);                await _unitOfWork.State.CreateAsync(state);                _response.Result = _mapper.Map<StateDTO>(state);                _response.StatusCode = HttpStatusCode.Created;                return CreatedAtRoute("GetState", new { id = state.Id }, _response);            }            catch (Exception ex)            {                _response.IsSuccess = false;                _response.ErrorMessages                     = new List<string>() { ex.ToString() };            }            return _response;        }        [ProducesResponseType(StatusCodes.Status204NoContent)]        [ProducesResponseType(StatusCodes.Status403Forbidden)]        [ProducesResponseType(StatusCodes.Status401Unauthorized)]        [ProducesResponseType(StatusCodes.Status404NotFound)]        [ProducesResponseType(StatusCodes.Status400BadRequest)]        [HttpDelete("{id:int}", Name = "DeleteState")]

        public async Task<ActionResult<APIResponse>> DeleteState(int id)        {            try            {                if (id == 0)                {                    return BadRequest();                }                var state = await _unitOfWork.State.GetAsync(u => u.Id == id);                if (state == null)                {                    return NotFound();                }                await _unitOfWork.State.RemoveAsync(state);                _response.StatusCode = HttpStatusCode.NoContent;                _response.IsSuccess = true;                return Ok(_response);            }            catch (Exception ex)            {                _response.IsSuccess = false;                _response.ErrorMessages                     = new List<string>() { ex.ToString() };            }            return _response;        }        [HttpPut("{id:int}", Name = "UpdateState")]        [ProducesResponseType(StatusCodes.Status204NoContent)]        [ProducesResponseType(StatusCodes.Status400BadRequest)]        public async Task<ActionResult<APIResponse>> UpdateState(int id, [FromBody] StateUpdateDTO updateDTO)        {            try            {                if (updateDTO == null || id != updateDTO.Id)                {                    return BadRequest();                }                if (await _unitOfWork.State.GetAsync(u => u.StateName.ToLower() == updateDTO.StateName.ToLower() && u.CountryId == updateDTO.CountryId && u.Id != updateDTO.Id) != null)                {                    ModelState.AddModelError("ErrorMessages", "State already Exists!");                    return BadRequest(ModelState);                }                if (await _unitOfWork.Country.GetAsync(u => u.Id == updateDTO.CountryId) == null)                {                    ModelState.AddModelError("ErrorMessages", "State ID is Invalid!");                    return BadRequest(ModelState);                }                State model = _mapper.Map<State>(updateDTO);                await _unitOfWork.State.UpdateAsync(model);                _response.StatusCode = HttpStatusCode.NoContent;                _response.IsSuccess = true;                return Ok(_response);            }            catch (Exception ex)            {                _response.IsSuccess = false;                _response.ErrorMessages                     = new List<string>() { ex.ToString() };            }            return _response;        }        //[HttpPatch("{id:int}", Name = "UpdatePartialState")]        //[ProducesResponseType(StatusCodes.Status204NoContent)]        //[ProducesResponseType(StatusCodes.Status400BadRequest)]        //public async Task<IActionResult> UpdatePartialState(int id, JsonPatchDocument<StateUpdateDTO> patchDTO)        //{        //    if (patchDTO == null || id == 0)        //    {        //        return BadRequest();        //    }        //    var state = await _unitOfWork.State.GetAsync(u => u.Id == id, tracked: false);        //    StateUpdateDTO stateDTO = _mapper.Map<StateUpdateDTO>(state);        //    if (state == null)        //    {        //        return BadRequest();        //    }        //    patchDTO.ApplyTo(stateDTO, ModelState);        //    State model = _mapper.Map<State>(stateDTO);        //    await _unitOfWork.State.UpdateAsync(model);        //    if (!ModelState.IsValid)        //    {        //        return BadRequest(ModelState);        //    }        //    return NoContent();        //}    }}