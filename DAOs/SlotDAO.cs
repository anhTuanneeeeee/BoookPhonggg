using BOs.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAOs
{
    public class SlotDAO : ISlotDAO
    {
        private readonly BookroomSwdContext _context;

        public SlotDAO(BookroomSwdContext context)
        {
            _context = context;
        }

        public async Task<Slot> CreateSlot(Slot slot)
        {
            await _context.Slots.AddAsync(slot);
            await _context.SaveChangesAsync();
            return slot;
        }

        public async Task<List<Slot>> GetSlotsByRoomId(int roomId)
        {
            return await _context.Slots
                .Where(s => s.RoomId == roomId)
                .ToListAsync();
        }

        public async Task<Slot?> GetSlotById(int id)
        {
            return await _context.Slots.FindAsync(id);
        }

        public async Task<bool> DeleteSlot(int id)
        {
            var slot = await GetSlotById(id);
            if (slot == null) return false;

            _context.Slots.Remove(slot);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
