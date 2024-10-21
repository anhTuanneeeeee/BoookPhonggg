using BOs.DTO;
using BOs.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOs
{
    public interface IRoomRepository
    {
        Task<Room> CreateRoom(CreateRoomDTO roomDto);
        Task<Room?> GetRoomById(int id);
        Task<List<Room>> GetAllRooms();
        Task<List<Room>> GetRoomsByBranchId(int branchId);
        Task<bool> DeleteRoom(int id);
    }
}
