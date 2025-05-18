using System;
using System.Collections.Generic;

namespace AlbumsGalore.Server.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime DateRegistered { get; set; }

    public DateTime? LastLogin { get; set; }

    public bool? IsActive { get; set; }

    public decimal? TaxRate { get; set; }

    public string? PayPalMerchantId { get; set; }

    public string? PayPalEmail { get; set; }

    public string? Currency { get; set; }

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();

    public virtual ICollection<Artist> Artists { get; set; } = new List<Artist>();

    public virtual ICollection<Musician> Musicians { get; set; } = new List<Musician>();

    public virtual ICollection<SalesOffer> SalesOfferBuyers { get; set; } = new List<SalesOffer>();

    public virtual ICollection<SalesOffer> SalesOfferSellers { get; set; } = new List<SalesOffer>();

    public virtual ICollection<SalesOrder> SalesOrders { get; set; } = new List<SalesOrder>();

    public virtual ICollection<SalesPayment> SalesPayments { get; set; } = new List<SalesPayment>();

    public virtual ICollection<SalesShoppingCart> SalesShoppingCarts { get; set; } = new List<SalesShoppingCart>();

    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();

    public virtual ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();

    public virtual ICollection<UserAudit> UserAudits { get; set; } = new List<UserAudit>();

    public virtual ICollection<UserPasswordReset> UserPasswordResets { get; set; } = new List<UserPasswordReset>();

    public virtual ICollection<UserRolesAssociation> UserRolesAssociations { get; set; } = new List<UserRolesAssociation>();

    public virtual ICollection<UserTwoFactor> UserTwoFactors { get; set; } = new List<UserTwoFactor>();
}
