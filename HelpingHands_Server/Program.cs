using HelpingHands_Server.Service;
using HelpingHands_Server.Service.IService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

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

//builder.Services.AddHttpClient<IFileUpload, FileUpload>();
//builder.Services.AddScoped<IFileUpload, FileUpload>();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(100);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
//app.UseSession();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
