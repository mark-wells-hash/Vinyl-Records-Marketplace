using AlbumsGalore.Server.Models.CustomModels.DiscogsAlbums;
using AlbumsGalore.Server.Models.CustomModels.DiscogsArtists;
using AlbumsGalore.Server.Models.CustomModels.DiscogsMusicians;
using Microsoft.Extensions.Logging.AzureAppServices;
using Azure.Identity;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;

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


//TODO: Change Cors to use the policy.WithOrigins instead of "AllowAnyOrigin". Add Urls to Secret

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

                if (hostContext.HostingEnvironment.IsProduction())
                {
                    var builtConfig = builder.Build();
                    //TODO: Add your-vault-name to appsetting.json
                    var keyVaultEndpoint = builtConfig["KeyVault:VaultUri"];

                    if (!string.IsNullOrEmpty(keyVaultEndpoint))
                    {
                        builder.AddAzureKeyVault(
                            new Uri(keyVaultEndpoint),
                            new DefaultAzureCredential());
                    }

                    // The below code adds to the key vault using the SecretClient object. Try the above method first
                    // See this website for whe difference and whether I need this one or not https://learn.microsoft.com/en-us/aspnet/core/security/key-vault-configuration

                    //var secretClient = new SecretClient(
                    //    new Uri($"https://{builtConfig["KeyVaultName"]}.vault.azure.net/"),
                    //    new DefaultAzureCredential());
                    //builder.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
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
