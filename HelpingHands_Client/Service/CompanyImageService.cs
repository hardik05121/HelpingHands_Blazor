using HelpingHands_Common;
using HelpingHands_Models;
using HelpingHands_Models.API;
using HelpingHands_Models.ViewModels;
using HelpingHands_Client.Service.IService;

namespace HelpingHands_Client.Service
{
    public class CompanyImageService : BaseService, ICompanyImageService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string categoryUrl;

        public CompanyImageService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            categoryUrl = configuration.GetValue<string>("ServiceUrls:HelpingHandAPI");
        }

        public Task<T> CreateAsync<T>(CompanyImageCreateVM dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = categoryUrl + "/api/v1/CompanyImageAPI",
                //Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = categoryUrl + "/api/v1/CompanyImageAPI/" + id,
                //Token = token
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = categoryUrl + "/api/v1/CompanyImageAPI",
                //Token = token
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = categoryUrl + "/api/v1/CompanyImageAPI/" + id,
                //Token = token
            });
        }

        public Task<T> SetAsync<T>(int imageId, int companyId)
        {
            string apiUrl = $"{categoryUrl}/api/v1/CompanyImageAPI/{imageId}/{companyId}";

            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                // Data = dto,
                Url = apiUrl,
                //Token = token
            });
        }

    }
}
