using BOs.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REPOs;

namespace BookingController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IGuestRepository _guestRepository;

        public StaffController(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateStaff([FromBody] StaffCreateDTO staffDto)
        {
            if (string.IsNullOrEmpty(staffDto.UserName) ||
                string.IsNullOrEmpty(staffDto.Email) ||
                string.IsNullOrEmpty(staffDto.Password) ||
                string.IsNullOrEmpty(staffDto.PhoneNumber))
            {
                return BadRequest("All fields are required.");
            }

            var staff = await _guestRepository.CreateStaff(
                staffDto.UserName, staffDto.Email, staffDto.Password, staffDto.PhoneNumber
            );

            if (staff == null)
            {
                return Conflict("Email already exists.");
            }

            return Ok(staff);
        }
    }
}
