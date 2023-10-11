﻿
using HelpingHands_Client.Service.IService;
using HelpingHands_Common;
using HelpingHands_Models;
using HelpingHands_Models.API;

namespace HelpingHands_Client.Service

        public Task<T> AmenityByPagination<T>(string term, string orderBy, int currentPage)
            //string apiUrl = $"{carUrl}/api/v1/StateAPI/GetStatesData/{Id}/{search}/{pageSize}/{pageNumber}";
            string apiUrl = $"{categoryUrl}/api/v1/AmenityAPI/AmenityByPagination?term={term}&orderBy={orderBy}&currentPage={currentPage}";