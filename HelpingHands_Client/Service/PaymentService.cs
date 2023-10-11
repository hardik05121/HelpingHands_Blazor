using HelpingHands_Common;
using HelpingHands_Models;
using HelpingHands_Models.API;
using HelpingHands_Models.ViewModels;
using HelpingHands_Client.Service.IService;

namespace HelpingHands_Client.Service{    public class PaymentService : BaseService, IPaymentService    {        private readonly IHttpClientFactory _clientFactory;        private string categoryUrl;        public PaymentService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)        {            _clientFactory = clientFactory;            categoryUrl = configuration.GetValue<string>("ServiceUrls:HelpingHandAPI");        }        public Task<T> CreateAsync<T>(PaymentCreateDTO dto)        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.POST,                Data = dto,                Url = categoryUrl + "/api/v1/paymentAPI/CreatePayment",
                //Token = token
            });        }        public Task<T> DeleteAsync<T>(int id)        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.DELETE,                Url = categoryUrl + "/api/v1/PaymentAPI/DeletePayment/" + id,
                //Token = token
            });        }        public Task<T> GetAllAsync<T>()        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.GET,                Url = categoryUrl + "/api/v1/paymentAPI/GetPayments",
                //Token = token
            });        }

        public Task<T> PaymentByPagination<T>(string term, string orderBy, int currentPage)        {
            //string apiUrl = $"{carUrl}/api/v1/StateAPI/GetStatesData/{Id}/{search}/{pageSize}/{pageNumber}";
            string apiUrl = $"{categoryUrl}/api/v1/paymentAPI/PaymentByPagination?term={term}&orderBy={orderBy}&currentPage={currentPage}";            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.GET,                Url = apiUrl,
                //Token = token
            });

        }        public Task<T> GetAsync<T>(int id)        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.GET,                Url = categoryUrl + "/api/v1/PaymentAPI/GetPayment/" + id,
                //Token = token
            });        }        public Task<T> UpdateAsync<T>(PaymentUpdateDTO dto)        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.PUT,                Data = dto,                Url = categoryUrl + "/api/v1/PaymentAPI/UpdatePayment/" + dto.Id,
                //Token = token
            });        }    }}
