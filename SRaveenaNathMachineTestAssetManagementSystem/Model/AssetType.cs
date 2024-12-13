using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SRaveenaNathMachineTestAssetManagementSystem.Model;

public partial class AssetType
{
    public int AssetTypeId { get; set; }

    public string TypeName { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<AssetDefinition> AssetDefinitions { get; set; } = new List<AssetDefinition>();
}
