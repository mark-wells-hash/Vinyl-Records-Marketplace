using Microsoft.AspNetCore.Http;
using AlbumsGalore.Server.DataAccess;
using AlbumsGalore.Server.Models;
using AlbumsGalore.Server.Utilities;
using Microsoft.AspNetCore.Mvc;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace AlbumsGalore.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ILogger<SalesController> _logger;
        private SalesDataAccessLayer? objSales = null;
        public SalesController(ILoggerFactory loggerFactory, IConfiguration config)
        {
            _logger = loggerFactory.CreateLogger<SalesController>();
            _logger.LogWarning("This is a WARNING message");
            _logger.LogInformation("This is an INFORMATION message");
            objSales = new SalesDataAccessLayer(loggerFactory);
        }

        [HttpGet]
        [Route("AddToShoppingCart/{albumId}/{userId}")]
        public int AddToShoppingCart(int albumId, int userId)
        {
            try
            {
                if (albumId != 0)
                {
                    return objSales!.AddToShoppingCart(albumId, userId);
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                _logger.LogError(userId, ex.StackTrace);
                return -1;
            }

            return -1;
        }

        [HttpGet]
        [Route("GetShoppingCartByUser/{userId}")]
        public List<SalesShoppingCartExtend> GetShoppingCartByUser(int userId)
        {
            try
            {
                List<SalesShoppingCartExtend> newList = objSales!.GetShoppingCartByUser(userId);
                return newList;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                _logger.LogError(userId, ex.StackTrace);
                return new List<SalesShoppingCartExtend>();
            }
        }

        [HttpGet]
        [Route("GetShoppingCartByUserBareObject/{userId}")]
        public List<SalesShoppingCart> GetShoppingCartByUserBareObject(int userId)
        {
            try
            {
                List<SalesShoppingCart> newList = objSales!.GetShoppingCartByUserBareObject(userId);
                return newList;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                _logger.LogError(userId, ex.StackTrace);
                return new List<SalesShoppingCart>();
            }
        }

        [HttpGet]
        [Route("GetShoppingCartCountByUser/{userId}")]
        public int GetShoppingCartCountByUser(int userId)
        {
            try
            {
                //TODO: Use a more direct query approach in data access to get count
                return GetShoppingCartByUserBareObject(userId).Count();
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                _logger.LogError(userId, ex.StackTrace);
                return -1;
            }
        }
    }
}
