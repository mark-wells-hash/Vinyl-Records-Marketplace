using Microsoft.EntityFrameworkCore;
using AlbumsGalore.Server.Utilities;
using System.Diagnostics;
using AlbumsGalore.Server.Models;
using AlbumsGalore.Server.Models.CustomModels.DiscogsAlbums;

namespace AlbumsGalore.Server.DataAccess
{
    public class SalesDataAccessLayer
    {
        ArtistsContext artists = new ArtistsContext();
        private readonly ILogger<SalesDataAccessLayer> _logger;
        public SalesDataAccessLayer(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<SalesDataAccessLayer>();
            _logger.LogWarning("This is a WARNING message ArtistDataAccessLayer");
            _logger.LogInformation("This is an INFORMATION message");
        }

        public int AddToShoppingCart(int itemId, int userId)
        {
            try
            {
                if (itemId != 0 && userId != 0)
                {
                    SalesShoppingCart cart = new SalesShoppingCart()
                    {
                        ItemId = itemId,
                        UserId = userId,
                        SalesStatusId = 1 //The sale is pending. The item will be added to the shopping cart
                    };
                    artists.SalesShoppingCarts.Add(cart);
                    artists.SaveChanges();
                    return cart.SalesShoppingCartId;
                }
                return 0;
            }
            catch
            {
                throw;
            }
        }

        public int UpdateShoppingCartStatus(int ShoppingCartId, int SalesStatusId)
        {
            try
            {
                if (ShoppingCartId != 0 && SalesStatusId != 0)
                {
                    SalesShoppingCart cart = artists.SalesShoppingCarts.Find(ShoppingCartId)!;
                    cart.SalesStatusId = SalesStatusId;
                    artists.Entry(cart).State = EntityState.Modified;
                    artists.SaveChanges();
                    return 1;
                }
                return 0;
            }
            catch
            {
                throw;
            }
        }
        public int AddToOrders(SalesOrder order)
        {
            try
            {
                if (order != null)
                {
                    
                    artists.SalesOrders.Add(order);
                    artists.SaveChanges();
                    return order.OrderId;
                }
                return 0;
            }
            catch
            {
                throw;
            }
        }

        public int UpdateOrders(int itemId, int userId)
        {
            try
            {
                if (itemId != 0 && userId != 0)
                {
                    SalesShoppingCart cart = new SalesShoppingCart()
                    {
                        ItemId = itemId,
                        UserId = userId,
                        SalesStatusId = 1 //The sale is pending. The item will be added to the shopping cart
                    };
                    artists.SalesShoppingCarts.Add(cart);
                    artists.SaveChanges();
                    return cart.SalesShoppingCartId;
                }
                return 0;
            }
            catch
            {
                throw;
            }
        }

        public int AddToOrdersItems(int itemId, int userId)
        {
            try
            {
                if (itemId != 0 && userId != 0)
                {
                    SalesShoppingCart cart = new SalesShoppingCart()
                    {
                        ItemId = itemId,
                        UserId = userId,
                        SalesStatusId = 1 //The sale is pending. The item will be added to the shopping cart
                    };
                    artists.SalesShoppingCarts.Add(cart);
                    artists.SaveChanges();
                    return cart.SalesShoppingCartId;
                }
                return 0;
            }
            catch
            {
                throw;
            }
        }
        public List<SalesShoppingCart> GetShoppingCartByUserBareObject(int userId)
        {
            try
            {
                List<SalesShoppingCart> newList = artists.SalesShoppingCarts.Where(m => m.UserId == userId)
                .Select(m => new SalesShoppingCart
                {
                    SalesShoppingCartId = m.SalesShoppingCartId,
                    UserId = userId,
                    SalesStatusId = 1,
                    Item = m.Item,
                    ItemId = m.ItemId,
                    DateInserted = m.DateInserted,
                    DateUpdated = m.DateUpdated,
                    SalesOffers = m.SalesOffers,
                    SalesStatus = m.SalesStatus,
                    User = m.User
                }).ToList();
                return newList;
            }
            catch
            {
                throw;
            }
        }

        //List<SalesShoppingCartExtend>
        public List<SalesShoppingCartExtend> GetShoppingCartByUser(int userId)
        {
            try
            {
                var listCart =
                          (from Cart in artists.SalesShoppingCarts
                           join Albums in artists.Albums
                             on Cart.ItemId equals Albums.AlbumId
                           join Artists in artists.Artists 
                            on Albums.ArtistId equals Artists.ArtistId 
                           join SalesStatus in artists.SalesStatuses
                            on Cart.SalesStatusId equals SalesStatus.SalesStatusId
                           join User in artists.Users
                            on Albums.UserId equals  User.UserId
                           where Cart.UserId! == userId
                           select new SalesShoppingCartExtend { 
                               SalesShoppingCartId = Cart.SalesShoppingCartId,
                               ItemId = Albums.AlbumId, 
                               AlbumName = Albums.AlbumName,
                               AlbumDescription = Albums.Description, //TODO: Truncate this either here or when sending to PayPal
                               ArtistName = Artists.Name,
                               Price = Albums.Price,
                               SalesStatusId = Cart.SalesStatusId,
                               SalesStatusValue = SalesStatus.StatusName,
                               UserId = Cart.UserId,
                               AlbumOwnerId = Albums.UserId,
                               AlbumOwnerName = User.FirstName! + " " + User.LastName!,
                               TaxRate = User.TaxRate,
                               PayPalMerchantID = User.PayPalMerchantId,
                               PayPalEmail = User.PayPalEmail,
                               Currency = User.Currency
                           });
               
                return listCart.ToList();
               
            }
            catch
            {
                throw;
            }
        }
    }
}
