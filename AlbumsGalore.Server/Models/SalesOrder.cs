using System;
using System.Collections.Generic;

namespace AlbumsGalore.Server.Models;

public partial class SalesOrder
{
    public int OrderId { get; set; }

    public int BuyerId { get; set; }

    public int PayPalOrderId { get; set; }

    public int Quantity { get; set; }

    public bool? Delivered { get; set; }

    public DateTime? DeliverDate { get; set; }

    public int DeliverAddress { get; set; }

    public DateTime? DateInserted { get; set; }

    public DateTime? DateUpdated { get; set; }

    public virtual User Buyer { get; set; } = null!;

    public virtual Address DeliverAddressNavigation { get; set; } = null!;

    public virtual ICollection<SalesOrdersItem> SalesOrdersItems { get; set; } = new List<SalesOrdersItem>();

    public virtual ICollection<SalesPayment> SalesPayments { get; set; } = new List<SalesPayment>();
}
