using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SRaveenaNathMachineTestAssetManagementSystem.Model;

public partial class PurchaseOrder
{
    public int PurchaseOrderId { get; set; }

    public int? VendorId { get; set; }

    public int? AssetDefinitionId { get; set; }

    public int Quantity { get; set; }

    public string? Status { get; set; }
    [JsonIgnore]
    public virtual AssetDefinition? AssetDefinition { get; set; }

    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();

    public virtual Vendor? Vendor { get; set; }
}
