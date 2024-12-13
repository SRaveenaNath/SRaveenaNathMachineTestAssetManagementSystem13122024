using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SRaveenaNathMachineTestAssetManagementSystem.Model;

public partial class Vendor
{
    public int VendorId { get; set; }

    public string VendorName { get; set; } = null!;

    public string? ContactInfo { get; set; }
    [JsonIgnore]
    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
}
