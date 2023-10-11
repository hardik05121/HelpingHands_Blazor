using HelpingHands_Client.Service.IService;
using HelpingHands_Common;
using HelpingHands_Models;
using HelpingHands_Models.API;

namespace HelpingHands_Client.Service{
	public class ApplicationRoleService : BaseService, IApplicationRoleService    {
		private readonly IHttpClientFactory _clientFactory;
		private string categoryUrl;

		public ApplicationRoleService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
            _clientFactory = clientFactory;
            categoryUrl = configuration.GetValue<string>("ServiceUrls:HelpingHandAPI");

		}

        public Task<T> CreateAsync<T>(ApplicationRoleDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = categoryUrl + "/api/v1/applicationRoleAPI/CreateApplicationRole",
                //Token = token
            });
        }

        public Task<T> DeleteAsync<T>(string id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = categoryUrl + "/api/v1/applicationRoleAPI/DeleteApplicationRole/" + id,
                //Token = token
            });
        }

        public Task<T> GetAllAsync<T>()        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.GET,                Url = categoryUrl + "/api/v1/applicationRoleAPI/GetApplicationRoles",                //Token = token            });        }

        public Task<T> ApplicationRoleByPagination<T>(string term, string orderBy, int currentPage)        {
            //string apiUrl = $"{carUrl}/api/v1/StateAPI/GetStatesData/{Id}/{search}/{pageSize}/{pageNumber}";
            string apiUrl = $"{categoryUrl}/api/v1/applicationRoleAPI/ApplicationRoleByPagination?term={term}&orderBy={orderBy}&currentPage={currentPage}";            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.GET,                Url = apiUrl,                //Token = token            });
        }        public Task<T> GetAsync<T>(string Id)        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.GET,                Url = categoryUrl + "/api/v1/applicationRoleAPI/GetApplicationRole/" + Id,
                //Token = token
            });        }        public Task<T> UpdateAsync<T>(ApplicationRoleDTO dto)        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.PUT,                Data = dto,                Url = categoryUrl + "/api/v1/applicationRoleAPI/UpdateApplicationRole/" + dto.Id,
                //Token = token
            });        }    }}
