using HelpingHands_Business.Repository.IRepostiory;
using HelpingHands_DataAccess.Data;

using HelpingHands_Business.Repository.IRepostiory;
using HelpingHands_DataAccess;
using HelpingHands_DataAccess.Data;

namespace HelpingHands_Business.Repository
{
        public class UnitOfWork : IUnitOfWork
        {
            private ApplicationDbContext _db;
            // public ICategoryRepository Category { get; private set; }
            public IAmenityRepository Amenity { get; private set; }
            public ICityRepository City { get; private set; }
            public ICompanyImageRepository CompanyImage { get; private set; }
            public ICompanyRepository Company { get; private set; }
            public ICompanyXAmenityRepository CompanyXAmenity { get; private set; }
            public ICompanyXPaymentRepository CompanyXPayment { get; private set; }
            public ICompanyXServiceRepository CompanyXService { get; private set; }
            public ICountryRepository Country { get; private set; }
            public IEnquiryRepository Enquiry { get; private set; }
            public IFirstCategoryRepository FirstCategory { get; private set; }
            public IPaymentRepository Payment { get; private set; }
            public IReviewRepository Review { get; private set; }
            public   ISecondCategoryRepository SecondCategory { get; private set; }
            public IReviewXCommentRepository ReviewXComment { get; private set; }
            public IServiceRepository Service { get; private set; }
            public IStateRepository State { get; private set; }

           public IThirdCategoryRepository ThirdCategory { get; private set; }

        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IApplicationRoleRepository ApplicationRole { get; private set; }
        public IApplicationUserRoleRepository ApplicationUserRole { get; private set; }
        public IUserRepository User { get; private set; }


        public UnitOfWork(ApplicationDbContext db)
            {
                _db = db;

            //Category = new CategoryRepository(_db);
            Amenity = new AmenityRepository(_db);
            City = new CityRepository(_db);
            CompanyImage = new CompanyImageRepository(_db);
            Company = new CompanyRepository(_db);
            CompanyXAmenity = new CompanyXAmenityRepository(_db);
            CompanyXPayment = new CompanyXPaymentRepository(_db);
            CompanyXService = new CompanyXServiceRepository(_db);
            Country = new CountryRepository(_db);
            Enquiry = new EnquiryRepository(_db);
            FirstCategory = new FirstCategoryRepository(_db);
            Payment = new PaymentRepository(_db);
                Review = new ReviewRepository(_db);
            SecondCategory = new SecondCategoryRepository(_db);
                ReviewXComment = new ReviewXCommentRepository(_db);
            Service = new ServiceRepository(_db);
            State = new StateRepository(_db);
            ThirdCategory = new ThirdCategoryRepository(_db);

            ApplicationUser = new ApplicationUserRepository(_db);
            ApplicationRole = new ApplicationRoleRepository(_db);
            ApplicationUserRole = new ApplicationUserRoleRepository(_db);
        }

            public void Save()
            {
                _db.SaveChanges();
            }
        }
    }

