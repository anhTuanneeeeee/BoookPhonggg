using System;
using System.Collections.Generic;

namespace BOs.Entity;

public partial class Status
{
    public int Status1 { get; set; }

    public string? StatusName { get; set; }

    public virtual ICollection<Payment> Payments { get; } = new List<Payment>();

    public virtual ICollection<Room> Rooms { get; } = new List<Room>();

    public virtual ICollection<SlotBooking> SlotBookings { get; } = new List<SlotBooking>();
}
