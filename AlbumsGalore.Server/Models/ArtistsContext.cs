using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AlbumsGalore.Server.Models;

public partial class ArtistsContext : DbContext
{
    public ArtistsContext()
    {
    }

    public ArtistsContext(DbContextOptions<ArtistsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<AddressType> AddressTypes { get; set; }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<AlbumConditionType> AlbumConditionTypes { get; set; }

    public virtual DbSet<AlbumGenre> AlbumGenres { get; set; }

    public virtual DbSet<AlbumMediaType> AlbumMediaTypes { get; set; }

    public virtual DbSet<AlbumMusician> AlbumMusicians { get; set; }

    public virtual DbSet<AlbumStatus> AlbumStatuses { get; set; }

    public virtual DbSet<AlbumStyle> AlbumStyles { get; set; }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<MediaType> MediaTypes { get; set; }

    public virtual DbSet<Musician> Musicians { get; set; }

    public virtual DbSet<SalesOffer> SalesOffers { get; set; }

    public virtual DbSet<SalesOfferStatus> SalesOfferStatuses { get; set; }

    public virtual DbSet<SalesOrder> SalesOrders { get; set; }

    public virtual DbSet<SalesOrdersItem> SalesOrdersItems { get; set; }

    public virtual DbSet<SalesPayment> SalesPayments { get; set; }

    public virtual DbSet<SalesShoppingCart> SalesShoppingCarts { get; set; }

    public virtual DbSet<SalesStatus> SalesStatuses { get; set; }

    public virtual DbSet<Song> Songs { get; set; }

    public virtual DbSet<Style> Styles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAddress> UserAddresses { get; set; }

    public virtual DbSet<UserAudit> UserAudits { get; set; }

    public virtual DbSet<UserPasswordReset> UserPasswordResets { get; set; }

    public virtual DbSet<UserPermission> UserPermissions { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<UserRolePermissionAssociation> UserRolePermissionAssociations { get; set; }

    public virtual DbSet<UserRolesAssociation> UserRolesAssociations { get; set; }

    public virtual DbSet<UserTwoFactor> UserTwoFactors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=Artists;TrustServerCertificate=True;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.Address1).HasMaxLength(120);
            entity.Property(e => e.Address2).HasMaxLength(120);
            entity.Property(e => e.Address3).HasMaxLength(120);
            entity.Property(e => e.AddressTypeId).HasColumnName("AddressTypeID");
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country)
                .HasMaxLength(2)
                .IsFixedLength();
            entity.Property(e => e.DateInserted)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Lat).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.Long).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.StateProvince)
                .HasMaxLength(3)
                .IsFixedLength();
            entity.Property(e => e.ZipPostalCode)
                .HasMaxLength(16)
                .IsFixedLength();

            entity.HasOne(d => d.AddressType).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.AddressTypeId)
                .HasConstraintName("FK_Addresses_AddressType");
        });

        modelBuilder.Entity<AddressType>(entity =>
        {
            entity.ToTable("AddressType");

            entity.Property(e => e.AddressTypeId).HasColumnName("AddressTypeID");
            entity.Property(e => e.AddressDescription).HasMaxLength(200);
            entity.Property(e => e.AddressTypeName).HasMaxLength(50);
            entity.Property(e => e.DateInserted)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Album>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("trgAfterUpdateAlbum"));

            entity.Property(e => e.AlbumId).HasColumnName("AlbumID");
            entity.Property(e => e.AlbumConditionTypeId).HasColumnName("AlbumConditionTypeID");
            entity.Property(e => e.AlbumName).HasMaxLength(50);
            entity.Property(e => e.AlbumStatusId).HasColumnName("AlbumStatusID");
            entity.Property(e => e.ArtistId).HasColumnName("ArtistID");
            entity.Property(e => e.CoverImageUrl)
                .HasMaxLength(230)
                .HasColumnName("CoverImageURL");
            entity.Property(e => e.DateInserted)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DiscogResourceUrl).HasMaxLength(230);
            entity.Property(e => e.Label).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("smallmoney");
            entity.Property(e => e.ReleaseDate)
                .HasMaxLength(4)
                .IsUnicode(false);
            entity.Property(e => e.ThumbnailUrl)
                .HasMaxLength(230)
                .HasColumnName("ThumbnailURL");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.AlbumConditionType).WithMany(p => p.Albums)
                .HasForeignKey(d => d.AlbumConditionTypeId)
                .HasConstraintName("FK_Albums_AlbumConditionType");

            entity.HasOne(d => d.AlbumStatus).WithMany(p => p.Albums)
                .HasForeignKey(d => d.AlbumStatusId)
                .HasConstraintName("FK_Albums_AlbumStatus");

            entity.HasOne(d => d.Artist).WithMany(p => p.Albums)
                .HasForeignKey(d => d.ArtistId)
                .HasConstraintName("FK_Albums_Artists");

            entity.HasOne(d => d.User).WithMany(p => p.Albums)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Albums_Users");
        });

        modelBuilder.Entity<AlbumConditionType>(entity =>
        {
            entity.ToTable("AlbumConditionType");

            entity.Property(e => e.AlbumConditionTypeId).HasColumnName("AlbumConditionTypeID");
            entity.Property(e => e.ConditionDescription).HasMaxLength(200);
            entity.Property(e => e.ConditionName).HasMaxLength(20);
            entity.Property(e => e.DateInserted)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<AlbumGenre>(entity =>
        {
            entity.ToTable("AlbumGenre");

            entity.Property(e => e.AlbumGenreId).HasColumnName("AlbumGenreID");
            entity.Property(e => e.AlbumId).HasColumnName("AlbumID");
            entity.Property(e => e.DateInserted)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
            entity.Property(e => e.GenreId).HasColumnName("GenreID");

            entity.HasOne(d => d.Album).WithMany(p => p.AlbumGenres)
                .HasForeignKey(d => d.AlbumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlbumGenre_Albums");

            entity.HasOne(d => d.Genre).WithMany(p => p.AlbumGenres)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlbumGenre_Genre");
        });

        modelBuilder.Entity<AlbumMediaType>(entity =>
        {
            entity.ToTable("AlbumMediaType");

            entity.Property(e => e.AlbumMediaTypeId).HasColumnName("AlbumMediaTypeID");
            entity.Property(e => e.AlbumId).HasColumnName("AlbumID");
            entity.Property(e => e.DatInserted)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
            entity.Property(e => e.MediaTypeId).HasColumnName("MediaTypeID");

            entity.HasOne(d => d.Album).WithMany(p => p.AlbumMediaTypes)
                .HasForeignKey(d => d.AlbumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlbumMediaType_Albums");

            entity.HasOne(d => d.MediaType).WithMany(p => p.AlbumMediaTypes)
                .HasForeignKey(d => d.MediaTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlbumMediaType_MediaType");
        });

        modelBuilder.Entity<AlbumMusician>(entity =>
        {
            entity.HasKey(e => e.AlbumMusiciansId);

            entity.ToTable("AlbumMusician");

            entity.Property(e => e.AlbumMusiciansId).HasColumnName("AlbumMusiciansID");
            entity.Property(e => e.AlbumId).HasColumnName("AlbumID");
            entity.Property(e => e.DateInserted)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
            entity.Property(e => e.MusicianId).HasColumnName("MusicianID");

            entity.HasOne(d => d.Album).WithMany(p => p.AlbumMusicians)
                .HasForeignKey(d => d.AlbumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlbumMusician_Albums");

            entity.HasOne(d => d.Musician).WithMany(p => p.AlbumMusicians)
                .HasForeignKey(d => d.MusicianId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlbumMusician_Musicians");
        });

        modelBuilder.Entity<AlbumStatus>(entity =>
        {
            entity.ToTable("AlbumStatus");

            entity.Property(e => e.AlbumStatusId).HasColumnName("AlbumStatusID");
            entity.Property(e => e.DateInserted)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
            entity.Property(e => e.StatusName).HasMaxLength(50);
        });

        modelBuilder.Entity<AlbumStyle>(entity =>
        {
            entity.ToTable("AlbumStyle");

            entity.Property(e => e.AlbumStyleId).HasColumnName("AlbumStyleID");
            entity.Property(e => e.AlbumId).HasColumnName("AlbumID");
            entity.Property(e => e.DateInserted)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
            entity.Property(e => e.StyleId).HasColumnName("StyleID");

            entity.HasOne(d => d.Album).WithMany(p => p.AlbumStyles)
                .HasForeignKey(d => d.AlbumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlbumStyle_Album");

            entity.HasOne(d => d.Style).WithMany(p => p.AlbumStyles)
                .HasForeignKey(d => d.StyleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlbumStyle_Style");
        });

        modelBuilder.Entity<Artist>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("trgAfterUpdateArtists"));

            entity.Property(e => e.ArtistId).HasColumnName("ArtistID");
            entity.Property(e => e.CoverImageUrl).HasMaxLength(250);
            entity.Property(e => e.DateInserted)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(4000);
            entity.Property(e => e.DiscogResourceUrl).HasMaxLength(250);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.ThumbnailUrl).HasMaxLength(250);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Artists)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Users_Artists");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("Genre");

            entity.Property(e => e.GenreId).HasColumnName("GenreID");
            entity.Property(e => e.DateInserted)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
            entity.Property(e => e.GenreName).HasMaxLength(20);
        });

        modelBuilder.Entity<MediaType>(entity =>
        {
            entity.ToTable("MediaType", tb => tb.HasTrigger("trgAfterUpdateMedia"));

            entity.Property(e => e.MediaTypeId).HasColumnName("MediaTypeID");
            entity.Property(e => e.DateInserted)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MediaName).HasMaxLength(20);
        });

        modelBuilder.Entity<Musician>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("trgAfterUpdate"));

            entity.Property(e => e.MusicianId).HasColumnName("MusicianID");
            entity.Property(e => e.ArtistId).HasColumnName("ArtistID");
            entity.Property(e => e.DateInserted)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(4000);
            entity.Property(e => e.DiscogResourceUrl).HasMaxLength(250);
            entity.Property(e => e.MusicianName).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Artist).WithMany(p => p.Musicians)
                .HasForeignKey(d => d.ArtistId)
                .HasConstraintName("FK_Musicians_Artists");

            entity.HasOne(d => d.User).WithMany(p => p.Musicians)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Musicians_User");
        });

        modelBuilder.Entity<SalesOffer>(entity =>
        {
            entity.HasKey(e => e.SalesOffersId);

            entity.ToTable("Sales_Offers", tb => tb.HasTrigger("trgAfterUpdateOffers"));

            entity.Property(e => e.SalesOffersId).HasColumnName("Sales_OffersID");
            entity.Property(e => e.BuyerId).HasColumnName("BuyerID");
            entity.Property(e => e.DateInserted)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.InstigatedByUserId).HasColumnName("InstigatedByUserID");
            entity.Property(e => e.OfferAmount).HasColumnType("smallmoney");
            entity.Property(e => e.SalesOfferStatusId).HasColumnName("Sales_OfferStatusID");
            entity.Property(e => e.SalesShoppingCartId).HasColumnName("Sales_ShoppingCartID");
            entity.Property(e => e.SellerId).HasColumnName("SellerID");

            entity.HasOne(d => d.Buyer).WithMany(p => p.SalesOfferBuyers)
                .HasForeignKey(d => d.BuyerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Offers_Buyer");

            entity.HasOne(d => d.SalesOfferStatus).WithMany(p => p.SalesOffers)
                .HasForeignKey(d => d.SalesOfferStatusId)
                .HasConstraintName("FK_Sales_Offers_Sales_OfferStatus");

            entity.HasOne(d => d.SalesShoppingCart).WithMany(p => p.SalesOffers)
                .HasForeignKey(d => d.SalesShoppingCartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Offers_Sales_ShoppingCart");

            entity.HasOne(d => d.Seller).WithMany(p => p.SalesOfferSellers)
                .HasForeignKey(d => d.SellerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Offers_Seller");
        });

        modelBuilder.Entity<SalesOfferStatus>(entity =>
        {
            entity.ToTable("Sales_OfferStatus");

            entity.Property(e => e.SalesOfferStatusId).HasColumnName("Sales_OfferStatusID");
            entity.Property(e => e.DateInserted)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.OfferStatusDescription).HasMaxLength(200);
            entity.Property(e => e.OfferStatusName).HasMaxLength(50);
        });

        modelBuilder.Entity<SalesOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId);

            entity.ToTable("Sales_Orders", tb => tb.HasTrigger("trgAfterUpdateSalesOrders"));

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.BuyerId).HasColumnName("BuyerID");
            entity.Property(e => e.DateInserted)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
            entity.Property(e => e.DeliverDate).HasColumnType("smalldatetime");
            entity.Property(e => e.PayPalOrderId).HasColumnName("PayPalOrderID");

            entity.HasOne(d => d.Buyer).WithMany(p => p.SalesOrders)
                .HasForeignKey(d => d.BuyerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Orders_Users");

            entity.HasOne(d => d.DeliverAddressNavigation).WithMany(p => p.SalesOrders)
                .HasForeignKey(d => d.DeliverAddress)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Orders_Addresses");
        });

        modelBuilder.Entity<SalesOrdersItem>(entity =>
        {
            entity.HasKey(e => e.SalesOrdersItemsId);

            entity.ToTable("Sales_OrdersItems");

            entity.Property(e => e.SalesOrdersItemsId).HasColumnName("SalesOrdersItemsID");
            entity.Property(e => e.DateInserted)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.SalesOrderId).HasColumnName("SalesOrderID");

            entity.HasOne(d => d.Item).WithMany(p => p.SalesOrdersItems)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_OrdersItems_Albums");

            entity.HasOne(d => d.SalesOrder).WithMany(p => p.SalesOrdersItems)
                .HasForeignKey(d => d.SalesOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_OrdersItems_Sales_Orders");
        });

        modelBuilder.Entity<SalesPayment>(entity =>
        {
            entity.ToTable("Sales_Payment");

            entity.Property(e => e.SalesPaymentId).HasColumnName("Sales_PaymentID");
            entity.Property(e => e.BuyerId).HasColumnName("BuyerID");
            entity.Property(e => e.DateInserted).HasColumnType("datetime");
            entity.Property(e => e.DeliveryCost).HasColumnType("smallmoney");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.PaymentAmount).HasColumnType("smallmoney");
            entity.Property(e => e.SubtotalAmount).HasColumnType("smallmoney");
            entity.Property(e => e.Surcharge).HasColumnType("smallmoney");
            entity.Property(e => e.Tax1).HasColumnType("smallmoney");
            entity.Property(e => e.Tax2).HasColumnType("smallmoney");

            entity.HasOne(d => d.Buyer).WithMany(p => p.SalesPayments)
                .HasForeignKey(d => d.BuyerId)
                .HasConstraintName("FK_Sales_Payment_Users");

            entity.HasOne(d => d.Order).WithMany(p => p.SalesPayments)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Payment_Sales_Orders");
        });

        modelBuilder.Entity<SalesShoppingCart>(entity =>
        {
            entity.ToTable("Sales_ShoppingCart", tb => tb.HasTrigger("trgAfterUpdateShoppingCart"));

            entity.Property(e => e.SalesShoppingCartId).HasColumnName("Sales_ShoppingCartID");
            entity.Property(e => e.DateInserted)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.SalesStatusId).HasColumnName("Sales_StatusID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Item).WithMany(p => p.SalesShoppingCarts)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_ShoppingCart_Albums");

            entity.HasOne(d => d.SalesStatus).WithMany(p => p.SalesShoppingCarts)
                .HasForeignKey(d => d.SalesStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_ShoppingCart_Sales_Status");

            entity.HasOne(d => d.User).WithMany(p => p.SalesShoppingCarts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_ShoppingCart_Users");
        });

        modelBuilder.Entity<SalesStatus>(entity =>
        {
            entity.ToTable("Sales_Status");

            entity.Property(e => e.SalesStatusId).HasColumnName("Sales_StatusID");
            entity.Property(e => e.DateInserted)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.StatusDescription).HasMaxLength(200);
            entity.Property(e => e.StatusName).HasMaxLength(50);
        });

        modelBuilder.Entity<Song>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("trgAfterUpdateSongs"));

            entity.Property(e => e.SongId).HasColumnName("SongID");
            entity.Property(e => e.AlbumId).HasColumnName("AlbumID");
            entity.Property(e => e.DateInserted)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(4000);
            entity.Property(e => e.DiscogResourceUrl).HasMaxLength(250);
            entity.Property(e => e.SongName).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Album).WithMany(p => p.Songs)
                .HasForeignKey(d => d.AlbumId)
                .HasConstraintName("FK_Songs_Album");

            entity.HasOne(d => d.User).WithMany(p => p.Songs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Songs_User");
        });

        modelBuilder.Entity<Style>(entity =>
        {
            entity.ToTable("Style");

            entity.Property(e => e.StyleId).HasColumnName("StyleID");
            entity.Property(e => e.DateInserted)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
            entity.Property(e => e.StyleName).HasMaxLength(20);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("trgLastLogin"));

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .IsFixedLength();
            entity.Property(e => e.DateRegistered)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(20);
            entity.Property(e => e.LastLogin).HasColumnType("smalldatetime");
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.Password).HasMaxLength(20);
            entity.Property(e => e.PayPalEmail).HasMaxLength(100);
            entity.Property(e => e.PayPalMerchantId)
                .HasMaxLength(20)
                .HasColumnName("PayPalMerchantID");
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.TaxRate).HasColumnType("smallmoney");
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<UserAddress>(entity =>
        {
            entity.HasKey(e => e.UserAddressesId);

            entity.ToTable("User_Addresses");

            entity.Property(e => e.UserAddressesId).HasColumnName("User_AddressesID");
            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Address).WithMany(p => p.UserAddresses)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Addresses_Addresses");

            entity.HasOne(d => d.User).WithMany(p => p.UserAddresses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Addresses_Users");
        });

        modelBuilder.Entity<UserAudit>(entity =>
        {
            entity.HasKey(e => e.LogId);

            entity.ToTable("UserAudit");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.ActivityType).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.TimeStamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.UserAudits)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserAudit_Users");
        });

        modelBuilder.Entity<UserPasswordReset>(entity =>
        {
            entity.HasKey(e => e.TokenId);

            entity.ToTable("UserPasswordReset");

            entity.Property(e => e.TokenId).HasColumnName("TokenID");
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.TokenValue).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.UserPasswordResets)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserPasswordReset_User");
        });

        modelBuilder.Entity<UserPermission>(entity =>
        {
            entity.HasKey(e => e.PermissionsId);

            entity.Property(e => e.PermissionsId).HasColumnName("PermissionsID");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.PermissionsName).HasMaxLength(50);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId);

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<UserRolePermissionAssociation>(entity =>
        {
            entity.HasKey(e => e.RolePermissionId);

            entity.ToTable("UserRolePermissionAssociation");

            entity.Property(e => e.RolePermissionId).HasColumnName("RolePermissionID");
            entity.Property(e => e.PermissionId).HasColumnName("PermissionID");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.Permission).WithMany(p => p.UserRolePermissionAssociations)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRolePermissionAssociation_UserPermissions");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRolePermissionAssociations)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRolePermissionAssociation_UserRole");
        });

        modelBuilder.Entity<UserRolesAssociation>(entity =>
        {
            entity.HasKey(e => e.UserRoleId);

            entity.ToTable("UserRolesAssociation");

            entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRolesAssociations)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRolesAssociation_UserRoles");

            entity.HasOne(d => d.User).WithMany(p => p.UserRolesAssociations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRolesAssociation_User");
        });

        modelBuilder.Entity<UserTwoFactor>(entity =>
        {
            entity.HasKey(e => e.TwoFactorId);

            entity.ToTable("UserTwoFactor");

            entity.Property(e => e.TwoFactorId).HasColumnName("TwoFactorID");
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.Method).HasMaxLength(10);
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.VerificationCode).HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany(p => p.UserTwoFactors)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserTwoFactor_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
