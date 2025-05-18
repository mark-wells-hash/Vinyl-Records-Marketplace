using System;
using System.Collections.Generic;

namespace AlbumsGalore.Server.Models;

public partial class SalesPayment
{
    public int SalesPaymentId { get; set; }

    public int OrderId { get; set; }

    public int? BuyerId { get; set; }

    public decimal? SubtotalAmount { get; set; }

    public decimal? Tax1 { get; set; }

    public decimal? Tax2 { get; set; }

    public decimal? Surcharge { get; set; }

    public decimal? DeliveryCost { get; set; }

    public decimal? PaymentAmount { get; set; }

    public bool? Success { get; set; }

    public DateTime? DateInserted { get; set; }

    public virtual User? Buyer { get; set; }

    public virtual SalesOrder Order { get; set; } = null!;
}
