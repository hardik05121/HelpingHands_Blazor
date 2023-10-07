using HelpingHands_Common;
using HelpingHands_Models;
using HelpingHands_Models.API;
using HelpingHands_Server.Service.IService;

namespace HelpingHands_Server.Service{
	public class AuthService : BaseService, IAuthService    {
		private readonly IHttpClientFactory _clientFactory;
		private string categoryUrl;

		public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			categoryUrl = configuration.GetValue<string>("ServiceUrls:HelpingHandAPI");

		}

		public Task<T> LoginAsync<T>(LoginRequestDTO obj)        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.POST,                Data = obj,                Url = categoryUrl + "/api/v1/UsersAuth/login"            });        }        public Task<T> RegisterAsync<T>(RegisterationRequestDTO obj)        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.POST,                Data = obj,                Url = categoryUrl + "/api/v1/UsersAuth/register"            });        }    }}