using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using HelpingHands_Business.Repository.IRepostiory;
using HelpingHands_DataAccess;
using HelpingHands_Models;
using HelpingHands_Models.Index;

namespace HelpingHands_API.Controllers.v1
{
    public class HomeAPIController : Controller
    {
        protected APIResponse _response;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HomeAPIController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _response = new();
        }

        public IActionResult Index()
        {

            return View();
        }
    }
}
