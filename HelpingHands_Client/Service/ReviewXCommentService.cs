﻿using HelpingHands_Common;using HelpingHands_Models;using HelpingHands_Models.API;using HelpingHands_Models.ViewModels;using HelpingHands_Client.Service.IService;

namespace HelpingHands_Client.Service{    public class ReviewXCommentService : BaseService, IReviewXCommentService    {        private readonly IHttpClientFactory _clientFactory;        private string categoryUrl;        public ReviewXCommentService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)        {            _clientFactory = clientFactory;            categoryUrl = configuration.GetValue<string>("ServiceUrls:HelpingHandAPI");        }



        public Task<T> CreateAsync<T>(ReviewXCommentCreateDTO dto)        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.POST,                Data = dto,                Url = categoryUrl + "/api/v1/ReviewXCommentAPI",
                //Token = token
            });        }        public Task<T> DeleteAsync<T>(int id)        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.DELETE,                Url = categoryUrl + "/api/v1/ReviewXCommentAPI/" + id,
                //Token = token
            });        }        public Task<T> GetAllAsync<T>()        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.GET,                Url = categoryUrl + "/api/v1/ReviewXCommentAPI",
                //Token = token
            });        }        public Task<T> GetAsync<T>(int id)        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.GET,                Url = categoryUrl + "/api/v1/ReviewXCommentAPI/" + id,
                //Token = token
            });        }        public Task<T> UpdateAsync<T>(ReviewXCommentUpdateDTO dto)        {            return SendAsync<T>(new APIRequest()            {                ApiType = SD.ApiType.PUT,                Data = dto,                Url = categoryUrl + "/api/v1/ReviewXCommentAPI/" + dto.Id,
                //Token = token
            });        }    }}