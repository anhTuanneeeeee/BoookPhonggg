using BOs.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public interface ISlotDAO
    {
        Task<Slot> CreateSlot(Slot slot);
        Task<List<Slot>> GetSlotsByRoomId(int roomId);
        Task<Slot?> GetSlotById(int id);
        Task<bool> DeleteSlot(int id);
    }
}
