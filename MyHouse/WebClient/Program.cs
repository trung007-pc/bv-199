using Blazored.LocalStorage;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Domain.FileFolders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor.Services;
using Radzen;


using WebClient.Data;
using WebClient.Identity;
using WebClient.RequestHttp;
using WebClient.Service.Dashboards;
using WebClient.Service.Departments;
using WebClient.Service.DocumentFiles;
using WebClient.Service.FileFolders;
using WebClient.Service.FileTypes;
using WebClient.Service.IssuingAgencys;
using WebClient.Service.JS;
using WebClient.Service.Positions;
using WebClient.Service.Roles;
using WebClient.Service.SendingFiles;
using WebClient.Service.UnitReviewDetails;
using WebClient.Service.UnitReviews;
using WebClient.Service.Units;
using WebClient.Service.UnitTypes;
using WebClient.Service.Upload;
using WebClient.Service.Users;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

RequestClient.Initialize(builder.Configuration);

//service
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<RoleManagerService, RoleManagerService>();
builder.Services.AddTransient<UserManagerService,UserManagerService>();
builder.Services.AddTransient<UnitService,UnitService>();
builder.Services.AddTransient<UnitReviewService, UnitReviewService>();
builder.Services.AddTransient<UnitReviewDetailService, UnitReviewDetailService>();
builder.Services.AddTransient<UnitTypeService,UnitTypeService>();
builder.Services.AddTransient<UploadService, UploadService>();
builder.Services.AddTransient<PositionService,PositionService>();
builder.Services.AddTransient<DepartmentService,DepartmentService>();
builder.Services.AddTransient<FileFolderService,FileFolderService>();
builder.Services.AddTransient<IssuingAgencyService,IssuingAgencyService>();
builder.Services.AddTransient<FileTypeService,FileTypeService>();
builder.Services.AddTransient<DocumentFileService,DocumentFileService>();
builder.Services.AddTransient<SendingFileService>();
builder.Services.AddTransient<DashboardService,DashboardService>();

builder.Services.AddScoped<DownloadFileService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ClipboardService>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider , ApiAuthenticationStateProvider >();



builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();




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

app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();