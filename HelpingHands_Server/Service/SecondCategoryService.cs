using HelpingHands_Common;
using HelpingHands_Models;
using HelpingHands_Models.API;
using HelpingHands_Models.ViewModels;
using HelpingHands_Server.Service.IService;
using NuGet.Common;

namespace HelpingHands_Server.Service{    public class SecondCategoryService : BaseService, ISecondCategoryService    {        private readonly IHttpClientFactory _clientFactory;        private string categoryUrl;        public SecondCategoryService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)        {            _clientFactory = clientFactory;            categoryUrl = configuration.GetValue<string>("ServiceUrls:HelpingHandAPI");        }        public Task<T> CreateAsync<T>(SecondCategoryCreateDTO dto)        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.POST,                Data = dto,                Url = categoryUrl + "/api/v1/secondCategoryAPI/CreateSecondCategory",
                //Token = token
            });        }        public Task<T> DeleteAsync<T>(int id)        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.DELETE,                Url = categoryUrl + "/api/v1/secondCategoryAPI/DeleteSecondCategory/" + id,
                //Token = token
            });        }        public Task<T> GetAllAsync<T>()        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.GET,                Url = categoryUrl + "/api/v1/secondCategoryAPI/GetSecondCategorys",
                //Token = token
            });        }

        public Task<T> SecondCategoryByPagination<T>(string term, string orderBy, int currentPage)        {
            //string apiUrl = $"{carUrl}/api/v1/StateAPI/GetStatesData/{Id}/{search}/{pageSize}/{pageNumber}";
            string apiUrl = $"{categoryUrl}/api/v1/secondCategoryAPI/SecondCategoryByPagination?term={term}&orderBy={orderBy}&currentPage={currentPage}";            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.GET,                Url = apiUrl,
                //Token = token
            });

        }        public Task<T> GetSecondCategoryByFirstCategory<T>(int id)        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.GET,                Url = categoryUrl + "/api/v1/secondCategoryAPI/GetSecondCategoryByFirstCategory" + id,
                //Token = token
            });        }        public Task<T> GetAsync<T>(int id)        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.GET,                Url = categoryUrl + "/api/v1/secondCategoryAPI/GetSecondCategory/" + id,
                //Token = token
            });        }        public Task<T> UpdateAsync<T>(SecondCategoryUpdateDTO dto)        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.PUT,                Data = dto,                Url = categoryUrl + "/api/v1/secondCategoryAPI/UpdateSecondCategory/" + dto.Id,
                //Token = token
            });        }    }}
