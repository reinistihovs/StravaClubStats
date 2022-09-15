using StravaClubStatsEngine.Service;
using StravaClubStatsEngine.Service.API;
using StravaClubStatsEngine.Service.API.Interface;
using StravaClubStatsEngine.Service.Interface;
using StravaClubStatsShared.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var stravaClubStatsEngineInput = new StravaClubStatsEngineInput()
{
    StravaClubAPIUrl = builder.Configuration["StravaClubAPIUrl"],
    APIToken = builder.Configuration["APIToken"],
    ClubID = Convert.ToInt32(builder.Configuration["ClubID"]),
    NumberOfPages = Convert.ToInt32(builder.Configuration["NumberOfPages"]),
};

builder.Services.AddSingleton(stravaClubStatsEngineInput);
builder.Services.AddHttpClient<IHttpAPIClient, HttpAPIClient>();
builder.Services.AddSingleton<IStravaClubStatsService, StravaClubStatsService>();

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
