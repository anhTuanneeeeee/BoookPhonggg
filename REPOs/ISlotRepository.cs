using BOs.DTO;
using BOs.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace REPOs
{
    public interface ISlotRepository
    {
        Task<Slot> CreateSlot(CreateSlotDTO slotDto);
        Task<List<Slot>> GetSlotsByRoomId(int roomId);
        Task<Slot?> GetSlotById(int id);
        Task<bool> DeleteSlot(int id);
    }
}
