using System;
using System.Collections.Generic;

namespace BOs.Entity;

public partial class Slot
{
    public int SlotId { get; set; }

    public int RoomId { get; set; }

    public string? StartTime { get; set; }

    public string? EndTime { get; set; }

    public virtual Room Room { get; set; } = null!;

    public virtual ICollection<SlotBooking> SlotBookings { get; } = new List<SlotBooking>();
}
