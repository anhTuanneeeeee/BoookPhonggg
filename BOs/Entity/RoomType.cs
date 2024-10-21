using System;
using System.Collections.Generic;

namespace BOs.Entity;

public partial class RoomType
{
    public int RoomTypeId { get; set; }

    public string? TypeName { get; set; }

    public string? Description { get; set; }

    public string? Utilities { get; set; }

    public int? Price { get; set; }

    public virtual ICollection<Room> Rooms { get; } = new List<Room>();
}
