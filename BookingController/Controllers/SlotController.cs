using BOs.DTO;
using BOs.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REPOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotController : ControllerBase
    {
        private readonly ISlotRepository _slotRepository;

        public SlotController(ISlotRepository slotRepository)
        {
            _slotRepository = slotRepository;
        }

        [HttpPost("CreateSlot")]
        public async Task<IActionResult> CreateSlot([FromBody] CreateSlotDTO slotDto)
        {
            if (slotDto == null || string.IsNullOrEmpty(slotDto.StartTime) || string.IsNullOrEmpty(slotDto.EndTime))
            {
                return BadRequest("StartTime and EndTime are required.");
            }

            var slot = await _slotRepository.CreateSlot(slotDto);
            return CreatedAtAction(nameof(GetSlotById), new { id = slot.SlotId }, slot);
        }

        [HttpGet("GetSlotByRoomId")]
        public async Task<IActionResult> GetSlotsByRoomId(int roomId)
        {
            var slots = await _slotRepository.GetSlotsByRoomId(roomId);
            return Ok(slots);
        }

        [HttpGet("GetSlotBySlotId")]
        public async Task<IActionResult> GetSlotById(int id)
        {
            var slot = await _slotRepository.GetSlotById(id);
            if (slot == null)
            {
                return NotFound("Slot not found.");
            }
            return Ok(slot);
        }

        [HttpDelete("DeleteSlotById")]
        public async Task<IActionResult> DeleteSlot(int id)
        {
            var result = await _slotRepository.DeleteSlot(id);
            if (!result)
            {
                return NotFound("Slot not found.");
            }
            return NoContent(); // 204 No Content
        }
    }
}
