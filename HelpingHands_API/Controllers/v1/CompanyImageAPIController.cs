using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Drawing;
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
    [Route("api/v{version:apiVersion}/CompanyImageAPI")]
    [ApiController]
    [ApiVersion("1.0")]

    public class CompanyImageAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CompanyImageAPIController(IUnitOfWork unitOfWork, IMapper mapper)
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
        public async Task<ActionResult<APIResponse>> GetCompanyImages([FromQuery(Name = "filterDisplayOrder")] int? Id,
           [FromQuery] string? search, int pageSize = 0, int pageNumber = 1)
        {
            try
            {
                IEnumerable<CompanyImage> companyImageList;

                if (Id > 0)
                {
                    companyImageList = await _unitOfWork.CompanyImage.GetAllAsync(u => u.Id == Id, includeProperties: "Company", pageSize: pageSize,
                        pageNumber: pageNumber);
                }
                else
                {
                    companyImageList = await _unitOfWork.CompanyImage.GetAllAsync(includeProperties: "Company", pageSize: pageSize,
                        pageNumber: pageNumber);
                }
                if (!string.IsNullOrEmpty(search))
                {
                    companyImageList = companyImageList.Where(u => u.Company.CompanyName.ToLower().Contains(search));
                }
                Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                _response.Result = _mapper.Map<List<CompanyImageDTO>>(companyImageList);
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


        [HttpGet("{id:int}", Name = "GetCompanyImage")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type =typeof(CategoryDTO))]
        //        [ResponseCache(Location =ResponseCacheLocation.None,NoStore =true)]
        public async Task<ActionResult<APIResponse>> GetCompanyImage(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var category = await _unitOfWork.CompanyImage.GetAsync(u => u.Id == id, includeProperties: "Company");
                if (category == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<CompanyImageDTO>(category);
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
        //[Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateCompanyImage([FromBody] CompanyImageCreateVM createDTO)
        {
            try
            {
                List<CompanyImage> companyImageList = _mapper.Map<List<CompanyImage>>(createDTO.CompanyImagelist);
                CompanyImage companyImage1 = _mapper.Map<CompanyImage>(createDTO.CompanyImage);
                // CarXColor carxcolor = _mapper.Map<CarXColor>(createDTO.CarXColor);
                Company company = _mapper.Map<Company>(createDTO.Company);
                foreach (var item in companyImageList)
                {
                    CompanyImage companyImage = new();
                    companyImage.CompanyId = company.Id;
                    companyImage.Image = item.Image;
                    companyImage.IsActive = companyImage1.IsActive;

                    _unitOfWork.CompanyImage.CreateAsync(companyImage);
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
        [HttpDelete("{id:int}", Name = "DeleteCompanyImage")]

        public async Task<ActionResult<APIResponse>> DeleteCompanyImage(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var category = await _unitOfWork.CompanyImage.GetAsync(u => u.Id == id);
                if (category == null)
                {
                    return NotFound();
                }
                await _unitOfWork.CompanyImage.RemoveAsync(category);
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

        [HttpPut("{imageId:int}/{companyId:int}", Name = "SetCompanyImage")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> SetCompanyImage(int imageId, int companyId)
        {
            try
            {
                if (companyId == 0 && imageId == 0)
                {
                    return BadRequest();
                }

                var company = await _unitOfWork.Company.GetAsync(u => u.Id == companyId, tracked: false);
                var companyImage = await _unitOfWork.CompanyImage.GetAsync(u => u.Id == imageId, tracked: false);

                CompanyUpdateDTO companyUpdateDTO = _mapper.Map<CompanyUpdateDTO>(company);
                companyUpdateDTO.CompanyLogo = companyImage.Image;

                if (company == null)
                {
                    return BadRequest();
                }

                Company model = _mapper.Map<Company>(companyUpdateDTO);
                await _unitOfWork.Company.UpdateAsync(model);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}

