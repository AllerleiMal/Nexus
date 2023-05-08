using System;
using System.Collections.Generic;

namespace BlogApp.Models;

public partial class Notification
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public int UserId { get; set; }

    public int TypeId { get; set; }

    public virtual NotificationType Type { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}
