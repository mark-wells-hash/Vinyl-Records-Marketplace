using System;
using System.Collections.Generic;

namespace AlbumsGalore.Server.Models;

public partial class SalesOrdersItem
{
    public int SalesOrdersItemsId { get; set; }

    public int SalesOrderId { get; set; }

    public int ItemId { get; set; }

    public DateTime? DateInserted { get; set; }

    public virtual Album Item { get; set; } = null!;

    public virtual SalesOrder SalesOrder { get; set; } = null!;
}
