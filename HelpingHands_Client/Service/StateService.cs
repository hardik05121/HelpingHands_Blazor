﻿using HelpingHands_Common;
using HelpingHands_Models;
using HelpingHands_Models.API;
using HelpingHands_Models.ViewModels;
using HelpingHands_Client.Service.IService;

namespace HelpingHands_Client.Service

        public Task<T> StateByPagination<T>(string term, string orderBy, int currentPage)
            //string apiUrl = $"{carUrl}/api/v1/StateAPI/GetStatesData/{Id}/{search}/{pageSize}/{pageNumber}";
            string apiUrl = $"{categoryUrl}/api/v1/StateAPI/StateByPagination?term={term}&orderBy={orderBy}&currentPage={currentPage}";