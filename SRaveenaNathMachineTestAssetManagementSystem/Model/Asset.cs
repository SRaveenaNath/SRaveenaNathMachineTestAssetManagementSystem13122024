using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SRaveenaNathMachineTestAssetManagementSystem.Model;

public partial class Asset
{
    public int AssetId { get; set; }

    public int? AssetDefinitionId { get; set; }

    public string? SerialNumber { get; set; }

    public int? PurchaseOrderId { get; set; }

    public DateTime? CreatedAt { get; set; }
    [JsonIgnore]
    public virtual AssetDefinition? AssetDefinition { get; set; }
   
    public virtual PurchaseOrder? PurchaseOrder { get; set; }
}
