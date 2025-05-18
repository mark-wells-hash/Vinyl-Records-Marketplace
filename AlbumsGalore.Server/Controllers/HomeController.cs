using Microsoft.AspNetCore.Mvc;

namespace AlbumsGalore.Controllers
{
    public class HomeController : Controller
    {
       // private IHttpClientFactory _httpClientFactory;
        private readonly ILogger<HomeController> _logger;
        //public HomeController(ILogger<HomeController> logger,
        //IHttpClientFactory httpClientFactory)
        //{
        //    _logger = logger;
        //    _httpClientFactory = httpClientFactory;
        //}

        public HomeController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HomeController>();
            _logger.LogWarning("This is a WARNING message");
            _logger.LogInformation("This is an INFORMATION message");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
