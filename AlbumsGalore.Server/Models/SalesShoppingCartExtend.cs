namespace AlbumsGalore.Server.Models
{
    public class SalesShoppingCartExtend: SalesShoppingCart
    {
        public SalesShoppingCartExtend() { }
        public string? AlbumName { get; set; }
        public string? AlbumDescription { get; set; }
        public string? ArtistName { get; set; }
        public string? SalesStatusValue { get; set; }
        public decimal? Price { get; set; }
        public int? AlbumOwnerId { get; set; }
        public string? AlbumOwnerName { get; set; }
        public decimal? TaxRate { get; set; }
        public string? PayPalMerchantID { get; set; }
        public string? PayPalEmail { get; set; }
        public string? Currency { get; set; }
        
      
      
      
    }
}
