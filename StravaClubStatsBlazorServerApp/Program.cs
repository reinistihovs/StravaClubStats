using MudBlazor.Services;
using StravaClubStatsEngine;
using StravaClubStatsEngine.Service;
using StravaClubStatsEngine.Service.API;
using StravaClubStatsEngine.Service.API.Interface;
using StravaClubStatsEngine.Service.CosmosDb;
using StravaClubStatsEngine.Service.CosmosDb.Interface;
using StravaClubStatsEngine.Service.Interface;
using StravaClubStatsShared.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

Int32.TryParse(builder.Configuration["ClientID"], out int clientID);

Int32.TryParse(builder.Configuration["ClubID"], out int clubID);

Int32.TryParse(builder.Configuration["NumberOfPages"], out int numberOfPages);

var stravaClubStatsEngineInput = new StravaClubStatsEngineInput()
{
    StravaClubAPIUrl = builder.Configuration["StravaClubAPIUrl"],
    ClientID = clientID,
    ClientSecret = builder.Configuration["ClientSecret"],
    RefreshToken = builder.Configuration["RefreshToken"],
    ClubID = clubID,
    NumberOfPages = numberOfPages,
    CosmosDbEndointUrl = builder.Configuration["CosmosDbEndpointUrl"],
    CosmosDbPrimaryKey = builder.Configuration["CosmosDbPrimaryKey"],
    CosmosDatabase = builder.Configuration["CosmosDatabase"],
    CosmosPartitionKey = builder.Configuration["CosmosPartitionKey"],
};

builder.Services.AddSingleton(stravaClubStatsEngineInput);
builder.Services.AddHttpClient<IHttpAPIClient, HttpAPIClient>();
builder.Services.AddSingleton<IStravaClubStatsService, StravaClubStatsService>();
builder.Services.AddSingleton<ICosmosDbConnection, CosmosDbConnection>();
builder.Services.AddSingleton<IStravaClubStatsForYearService, StravaClubStatsForYearService>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(StravaClubStatsEngineMediatREntryPoint).Assembly));

builder.Services.AddMudServices();

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
