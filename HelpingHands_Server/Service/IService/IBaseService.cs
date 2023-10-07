﻿
using HelpingHands_Models;
using HelpingHands_Models.API;

namespace HelpingHands_Server.Service.IService
{
    public interface IBaseService
    {
        APIResponse responseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
