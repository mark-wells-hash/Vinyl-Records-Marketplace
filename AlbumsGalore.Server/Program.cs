using AlbumsGalore.Server.Models.CustomModels.DiscogsAlbums;
using AlbumsGalore.Server.Models.CustomModels.DiscogsArtists;
using AlbumsGalore.Server.Models.CustomModels.DiscogsMusicians;
using Microsoft.Extensions.Logging.AzureAppServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;

//HostApplicationBuilder builderHost = Host.CreateApplicationBuilder(args);
//builderHost.Logging.ClearProviders();
//builderHost.Logging.AddConsole();
//builderHost.Logging.AddDebug();
//builderHost.Logging.AddAzureWebAppDiagnostics();
//builderHost.Services.Configure<AzureFileLoggerOptions>(options =>
//{
//    options.FileName = "azure-diagnostics-";
//    options.FileSizeLimit = 50 * 1024;
//    options.RetainedFileCountLimit = 5;
//});
//builderHost.Services.Configure<AzureBlobLoggerOptions>(options =>
//{
//    options.BlobName = "log.txt";
//});

//using IHost host = builderHost.Build();

// Application code should start here.

//await host.RunAsync();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddAzureWebAppDiagnostics();
builder.Services.Configure<AzureFileLoggerOptions>(options =>
{
    options.FileName = "azure-diagnostics-";
    options.FileSizeLimit = 50 * 1024;
    options.RetainedFileCountLimit = 5;
});
builder.Services.Configure<AzureBlobLoggerOptions>(options =>
{
    options.BlobName = "log.txt";
});
// Add services to the container.
builder.Services.AddCors();
builder.Services.AddHttpClient<IDiscogsClientSearchAlbumModel, DiscogsClientSearchAlbumModel>();
builder.Services.AddHttpClient<IDiscogsClientSearchArtistModel, DiscogsClientSearchArtistModel>();
builder.Services.AddHttpClient<IDiscogsClientAlbumModel, DiscogsClientAlbumModel>();
builder.Services.AddHttpClient<IDiscogsClientArtistModel, DiscogsClientArtistModel>();
builder.Services.AddHttpClient<IDiscogsClientMusicianModel, DiscogsClientMusicianModel>();


//Note: could have used this version of adding the policy but the UseCors needs to be called below using variable specified at the top
//of this file. app.UseCors(MyAllowSpecificOrigins)

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      //policy =>
                      //{
                      //    policy.WithOrigins("https://localhost:5173", "http://localhost:3000"); // add the allowed origins  

                      //});
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var host = new HostBuilder()
            .ConfigureAppConfiguration((hostContext, builder) =>
            {
                // Add other providers for JSON, etc.

                if (hostContext.HostingEnvironment.IsDevelopment())
                {
                    builder.AddUserSecrets<Program>();
                }
            })
            .Build();

//
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Make sure you call this before calling app.UseMvc()
app.UseCors(
    MyAllowSpecificOrigins
);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
host.Run();
