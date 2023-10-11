using HelpingHands_Client.Service.IService;
using HelpingHands_Common;
using HelpingHands_Models;
using HelpingHands_Models.API;

namespace HelpingHands_Client.Service{    public class ApplicationUserRoleService : BaseService, IApplicationUserRoleService    {        private readonly IHttpClientFactory _clientFactory;        private string categoryUrl;        public ApplicationUserRoleService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)        {            _clientFactory = clientFactory;            categoryUrl = configuration.GetValue<string>("ServiceUrls:HelpingHandAPI");        }

        public Task<T> GetAllAsync<T>()        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.GET,                Url = categoryUrl + "/api/v1/applicationUserRoleAPI/GetApplicationUserRoles",
                //Token = token
            });        }
        public Task<T> GetAsync<T>(string Id)        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.GET,                Url = categoryUrl + "/api/v1/applicationUserRoleAPI/GetApplicationUserRole/" + Id,
                //Token = token
            });        }


    }}