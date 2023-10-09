﻿
	[Route("api/v{version:apiVersion}/[Controller]/[Action]")]
	[ApiController]


		[HttpGet(Name = "GetEnquirys")]
		[ResponseCache(CacheProfileName = "Default30")]


        [HttpGet(Name = "EnquiryByPagination")]
                term = string.IsNullOrEmpty(term) ? "" : term.ToLower();

                EnquiryIndexVM enquiryIndexVM = new EnquiryIndexVM();
                IEnumerable<Enquiry> list = await _unitOfWork.Enquiry.GetAllAsync(includeProperties: "Company,ApplicationUser");

                var List = _mapper.Map<List<EnquiryDTO>>(list);

                enquiryIndexVM.NameSortOrder = string.IsNullOrEmpty(orderBy) ? "countryName_desc" : "";

                if (!string.IsNullOrEmpty(term))
                {
                    List = List.Where(u => u.UserName.ToLower().Contains(term, StringComparison.OrdinalIgnoreCase) ||
                                u.Company.CompanyName.ToLower().Contains(term, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                switch (orderBy)
                {
                    case "countryName_desc":
                        List = List.OrderByDescending(a => a.UserName).ToList();
                        break;

                    default:
                        List = List.OrderBy(a => a.UserName).ToList();
                        break;
                }
                int totalRecords = List.Count();
                int pageSize = 10;
                int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
                List = List.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                // current=1, skip= (1-1=0), take=5 
                // currentPage=2, skip (2-1)*5 = 5, take=5 ,
                enquiryIndexVM.enquiries = List;
                enquiryIndexVM.CurrentPage = currentPage;
                enquiryIndexVM.TotalPages = totalPages;
                enquiryIndexVM.Term = term;
                enquiryIndexVM.PageSize = pageSize;
                enquiryIndexVM.OrderBy = orderBy;

                _response.Result = _mapper.Map<EnquiryIndexVM>(enquiryIndexVM);
        //[ProducesResponseType(200, Type =typeof(CategoryDTO))]
        //        [ResponseCache(Location =ResponseCacheLocation.None,NoStore =true)]
        public async Task<ActionResult<APIResponse>> GetEnquiry(int id)
        // [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]

        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<ActionResult<APIResponse>> DeleteEnquiry(int id)