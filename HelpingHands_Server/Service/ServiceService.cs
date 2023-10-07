using HelpingHands_Common;
using HelpingHands_Models;
using HelpingHands_Models.API;
using HelpingHands_Models.ViewModels;
using HelpingHands_Server.Service.IService;

namespace HelpingHands_Server.Service{    public class ServiceService : BaseService, IServiceService    {        private readonly IHttpClientFactory _clientFactory;        private string categoryUrl;        public ServiceService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)        {            _clientFactory = clientFactory;            categoryUrl = configuration.GetValue<string>("ServiceUrls:HelpingHandAPI");        }        public Task<T> CreateAsync<T>(ServiceCreateDTO dto)        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.POST,                Data = dto,                Url = categoryUrl + "/api/v1/ServiceAPI/CreateService",
                //Token = token
            });        }        public Task<T> DeleteAsync<T>(int id)        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.DELETE,                Url = categoryUrl + "/api/v1/ServiceAPI/DeleteService/" + id,
                //Token = token
            });        }        public Task<T> GetAllAsync<T>()        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.GET,                Url = categoryUrl + "/api/v1/ServiceAPI/GetServices",
                //Token = token
            });        }

        public Task<T> ServiceByPagination<T>(string term, string orderBy, int currentPage)        {
            //string apiUrl = $"{carUrl}/api/v1/StateAPI/GetStatesData/{Id}/{search}/{pageSize}/{pageNumber}";
            string apiUrl = $"{categoryUrl}/api/v1/ServiceAPI/ServiceByPagination?term={term}&orderBy={orderBy}&currentPage={currentPage}";            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.GET,                Url = apiUrl,
                //Token = token
            });

        }        public Task<T> GetAsync<T>(int id)        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.GET,                Url = categoryUrl + "/api/v1/ServiceAPI/GetService/" + id,
                //Token = token
            });        }        public Task<T> UpdateAsync<T>(ServiceUpdateDTO dto)        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.PUT,                Data = dto,                Url = categoryUrl + "/api/v1/ServiceAPI/UpdateService/" + dto.Id,
                //Token = token
            });        }    }}
