using System;
using System.Collections.Generic;

namespace BlogApp.Models;

public class NotificationType
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
