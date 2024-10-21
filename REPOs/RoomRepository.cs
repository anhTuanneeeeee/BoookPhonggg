using BOs.DTO;
using BOs.Entity;
using DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOs
{
    public class RoomRepository : IRoomRepository
    {
        private readonly IRoomDAO _roomDAO;

        public RoomRepository(IRoomDAO roomDAO)
        {
            _roomDAO = roomDAO;
        }

        public async Task<Room> CreateRoom(CreateRoomDTO roomDto)
        {
            var room = new Room
            {
                RoomName = roomDto.RoomName,
                BranchId = roomDto.BranchId,
                IsAvailable = roomDto.IsAvailable
            };

            return await _roomDAO.CreateRoom(room);
        }
        public async Task<Room?> GetRoomById(int id)
        {
            return await _roomDAO.GetRoomById(id);
        }

        public async Task<List<Room>> GetAllRooms()
        {
            return await _roomDAO.GetAllRooms();
        }

        public async Task<List<Room>> GetRoomsByBranchId(int branchId)
        {
            return await _roomDAO.GetRoomsByBranchId(branchId);
        }
        public async Task<bool> DeleteRoom(int id) // Cài đặt phương thức xóa
        {
            return await _roomDAO.DeleteRoom(id);
        }
    }
}
