using HelpingHands_Client;
using HelpingHands_Client.Service;
using HelpingHands_Client.Service.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Configure IConfiguration
//builder.Configuration.AddJsonFile("~/");
var configuration = builder.Configuration.Build();
builder.Services.AddSingleton<IConfiguration>(configuration);



builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddHttpClient<IFirstCategoryService, FirstCategoryService>();
builder.Services.AddScoped<IFirstCategoryService, FirstCategoryService>();
builder.Services.AddHttpClient<ISecondCategoryService, SecondCategoryService>();
builder.Services.AddScoped<ISecondCategoryService, SecondCategoryService>();
builder.Services.AddHttpClient<IThirdCategoryService, ThirdCategoryService>();
builder.Services.AddScoped<IThirdCategoryService, ThirdCategoryService>();
builder.Services.AddHttpClient<IPaymentService, PaymentService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddHttpClient<ICountryService, CountryService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddHttpClient<IStateService, StateService>();
builder.Services.AddScoped<IStateService, StateService>();
builder.Services.AddHttpClient<IAmenityService, AmenityService>();
builder.Services.AddScoped<IAmenityService, AmenityService>();
builder.Services.AddHttpClient<ICityService, CityService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddHttpClient<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddHttpClient<ICompanyXAmenityService, CompanyXAmenityService>();
builder.Services.AddScoped<ICompanyXAmenityService, CompanyXAmenityService>();
builder.Services.AddHttpClient<ICompanyXPaymentService, CompanyXPaymentService>();
builder.Services.AddScoped<ICompanyXPaymentService, CompanyXPaymentService>();
builder.Services.AddHttpClient<IServiceService, ServiceService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddHttpClient<ICompanyXServiceService, CompanyXServiceService>();
builder.Services.AddScoped<ICompanyXServiceService, CompanyXServiceService>();
builder.Services.AddHttpClient<ICompanyImageService, CompanyImageService>();
builder.Services.AddScoped<ICompanyImageService, CompanyImageService>();

builder.Services.AddHttpClient<IReviewService, ReviewService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddHttpClient<IReviewXCommentService, ReviewXCommentService>();
builder.Services.AddScoped<IReviewXCommentService, ReviewXCommentService>();
builder.Services.AddHttpClient<IEnquiryService, EnquiryService>();
builder.Services.AddScoped<IEnquiryService, EnquiryService>();

builder.Services.AddHttpClient<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddHttpClient<IApplicationUserService, ApplicationUserService>();
builder.Services.AddScoped<IApplicationUserService, ApplicationUserService>();
builder.Services.AddHttpClient<IApplicationRoleService, ApplicationRoleService>();
builder.Services.AddScoped<IApplicationRoleService, ApplicationRoleService>();
builder.Services.AddHttpClient<IApplicationUserRoleService, ApplicationUserRoleService>();
builder.Services.AddScoped<IApplicationUserRoleService, ApplicationUserRoleService>();

//builder.Services.AddHttpClient<IFirstCategoryUpload, FirstCategoryUpload>();
//builder.Services.AddScoped<IFirstCategoryUpload, FirstCategoryUpload>();
//builder.Services.AddHttpClient<ISecondCategoryUpload, SecondCategoryUpload>();
//builder.Services.AddScoped<ISecondCategoryUpload, SecondCategoryUpload>();
//builder.Services.AddHttpClient<IThirdCategoryUpload, ThirdCategoryUpload>();
//builder.Services.AddScoped<IThirdCategoryUpload, ThirdCategoryUpload>();
//builder.Services.AddHttpClient<ICompanyUpload, CompanyUpload>();
//builder.Services.AddScoped<ICompanyUpload, CompanyUpload>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

await builder.Build().RunAsync();
