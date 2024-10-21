using BOs.Entity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DAOs
{
    public class RoomDAO : IRoomDAO
    {
        private readonly BookroomSwdContext _context;

        public RoomDAO(BookroomSwdContext context)
        {
            _context = context;
        }

        public async Task<Room> CreateRoom(Room room)
        {
            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();
            return room;
        }
        public async Task<Room?> GetRoomById(int id)
        {
            return await _context.Rooms.FindAsync(id);
        }

        public async Task<List<Room>> GetAllRooms()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<List<Room>> GetRoomsByBranchId(int branchId)
        {
            return await _context.Rooms
                .Where(r => r.BranchId == branchId)
                .ToListAsync();
        }
        public async Task<bool> DeleteRoom(int id) // Cài đặt phương thức xóa
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null) return false;

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
