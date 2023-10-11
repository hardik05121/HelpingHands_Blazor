using HelpingHands_Common;
using HelpingHands_Models;
using HelpingHands_Models.API;
using HelpingHands_Models.ViewModels;
using HelpingHands_Client.Service.IService;

namespace HelpingHands_Client.Service{    public class CompanyXServiceService : BaseService, ICompanyXServiceService
	{        private readonly IHttpClientFactory _clientFactory;        private string categoryUrl;        public CompanyXServiceService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)        {            _clientFactory = clientFactory;            categoryUrl = configuration.GetValue<string>("ServiceUrls:HelpingHandAPI");        }        public Task<T> CreateAsync<T>(CompanyXServiceVM dto)        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.POST,                Data = dto,                Url = categoryUrl + "/api/v1/CompanyXServiceAPI",
                //Token = token
            });        }        public Task<T> DeleteAsync<T>(int id)        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.DELETE,                Url = categoryUrl + "/api/v1/CompanyXServiceAPI/" + id,
                //Token = token
            });        }        public Task<T> GetAllAsync<T>()        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.GET,                Url = categoryUrl + "/api/v1/CompanyXServiceAPI",
                //Token = token
            });        }        public Task<T> GetAsync<T>(int id)        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.GET,                Url = categoryUrl + "/api/v1/CompanyXServiceAPI/" + id,
                //Token = token
            });        }        //public Task<T> UpdateAsync<T>(CompanyXServiceUpdateVM dto, string token)        //{        //    return SendAsync<T>(new APIRequest()        //    {        //        ApiType = SD.ApiType.PUT,        //        Data = dto,        //        Url = categoryUrl + "/api/v1/CompanyXServiceAPI/",        //        Token = token        //    });        //}    }}
