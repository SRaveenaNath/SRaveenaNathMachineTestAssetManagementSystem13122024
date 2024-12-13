using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SRaveenaNathMachineTestAssetManagementSystem.Model;

public partial class AssetDefinition
{
    public int AssetDefinitionId { get; set; }

    public int? AssetTypeId { get; set; }

    public string DefinitionName { get; set; } = null!;

    public virtual AssetType? AssetType { get; set; }
    [JsonIgnore]
    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
}
