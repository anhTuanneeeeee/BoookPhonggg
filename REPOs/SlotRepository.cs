using BOs.DTO;
using BOs.Entity;
using DAOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace REPOs
{
    public class SlotRepository : ISlotRepository
    {
        private readonly ISlotDAO _slotDAO;

        public SlotRepository(ISlotDAO slotDAO)
        {
            _slotDAO = slotDAO;
        }

        public async Task<Slot> CreateSlot(CreateSlotDTO slotDto)
        {
            var slot = new Slot
            {
                RoomId = slotDto.RoomId,
                StartTime = slotDto.StartTime,
                EndTime = slotDto.EndTime
            };

            return await _slotDAO.CreateSlot(slot);
        }

        public async Task<List<Slot>> GetSlotsByRoomId(int roomId)
        {
            return await _slotDAO.GetSlotsByRoomId(roomId);
        }

        public async Task<Slot?> GetSlotById(int id)
        {
            return await _slotDAO.GetSlotById(id);
        }

        public async Task<bool> DeleteSlot(int id)
        {
            return await _slotDAO.DeleteSlot(id);
        }
    }
}
