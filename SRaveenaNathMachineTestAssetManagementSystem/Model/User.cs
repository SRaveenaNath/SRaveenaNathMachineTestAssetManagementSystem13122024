using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SRaveenaNathMachineTestAssetManagementSystem.Model;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? RoleId { get; set; }

    public DateTime? CreatedAt { get; set; }
    [JsonIgnore]
    public virtual Role? Role { get; set; }
}
