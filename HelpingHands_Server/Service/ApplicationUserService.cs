using HelpingHands_Common;
using HelpingHands_Models;
using HelpingHands_Models.API;
using HelpingHands_Models.ViewModels;
using HelpingHands_Server.Service.IService;

namespace HelpingHands_Server.Service{
    public class ApplicationUserService : BaseService, IApplicationUserService    {
        private readonly IHttpClientFactory _clientFactory;
        private string categoryUrl;

        public ApplicationUserService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            categoryUrl = configuration.GetValue<string>("ServiceUrls:HelpingHandAPI");

        }

        //public Task<T> CreateAsync<T>(ApplicationUserCreateDTO dto, string token)
        //{
        //    return SendAsync<T>(new APIRequest()
        //    {
        //        ApiType = SD.ApiType.POST,
        //        Data = dto,
        //        Url = categoryUrl + "/api/v1/ApplicationUserAPI",
        //        Token = token
        //    });
        //}

        //public Task<T> DeleteAsync<T>(int id, string token)
        //{
        //    return SendAsync<T>(new APIRequest()
        //    {
        //        ApiType = SD.ApiType.DELETE,
        //        Url = categoryUrl + "/api/v1/ApplicationUserAPI/" + id,
        //        Token = token
        //    });
        //}

        public Task<T> GetAllAsync<T>()        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.GET,                Url = categoryUrl + "/api/v1/applicationUserAPI/GetApplicationUsers",
                //Token = token
            });        }

        public Task<T> ApplicationUserByPagination<T>(string term, string orderBy, int currentPage)        {
            //string apiUrl = $"{carUrl}/api/v1/StateAPI/GetStatesData/{Id}/{search}/{pageSize}/{pageNumber}";
            string apiUrl = $"{categoryUrl}/api/v1/applicationUserAPI/ApplicationUserByPagination?term={term}&orderBy={orderBy}&currentPage={currentPage}";            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.GET,                Url = apiUrl,
                //Token = token
            });
        }        public Task<T> GetAsync<T>(string Id)        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.GET,                Url = categoryUrl + "/api/v1/applicationUserAPI/GetApplicationUser/" + Id,
                //Token = token
            });        }        public Task<T> UpdateAsync<T>(UserVM dto)        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.PUT,                Data = dto,                Url = categoryUrl + "/api/v1/ApplicationUserAPI/UpdateApplicationUser",
                //Token = token
            });        }    }}