using BOs.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REPOs;

namespace BookingController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;

        public RoomController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        [HttpPost("CreateRoom")]
        public async Task<IActionResult> CreateRoom([FromBody] CreateRoomDTO roomDto)
        {
            if (roomDto == null ||
                string.IsNullOrEmpty(roomDto.RoomName) ||
                roomDto.BranchId <= 0)
            {
                return BadRequest("All fields are required and must be valid.");
            }

            var createdRoom = await _roomRepository.CreateRoom(roomDto);
            return CreatedAtAction(nameof(CreateRoom), new { id = createdRoom.RoomId }, createdRoom);
        }

        [HttpGet("GetAllRoom")]
        public async Task<IActionResult> GetAllRooms()
        {
            var rooms = await _roomRepository.GetAllRooms();
            return Ok(rooms);
        }

        [HttpGet("GetRoomByIdRoom")]
        public async Task<IActionResult> GetRoomById(int id)
        {
            var room = await _roomRepository.GetRoomById(id);
            if (room == null)
            {
                return NotFound("Room not found.");
            }
            return Ok(room);
        }

        [HttpGet("GetRoomByBranchId")]
        public async Task<IActionResult> GetRoomsByBranchId(int branchId)
        {
            var rooms = await _roomRepository.GetRoomsByBranchId(branchId);
            return Ok(rooms);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id) // Thêm phương thức xóa
        {
            var isDeleted = await _roomRepository.DeleteRoom(id);
            if (!isDeleted)
            {
                return NotFound("Room not found.");
            }

            return NoContent(); // Trả về 204 No Content nếu xóa thành công
        }
    }
}
