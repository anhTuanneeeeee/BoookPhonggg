using BOs.DTO;
using BOs.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REPOs;

namespace BookingController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchRepository _branchRepository;

        public BranchController(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        [HttpPost("Create_Branch")]
        public async Task<IActionResult> CreateBranch([FromBody] BranchCreateDTO branchDto)
        {
            if (string.IsNullOrEmpty(branchDto.BranchName) ||
                string.IsNullOrEmpty(branchDto.Location) ||
                string.IsNullOrEmpty(branchDto.PhoneNumber))
            {
                return BadRequest("All fields are required.");
            }

            var branch = new Branch
            {
                BranchName = branchDto.BranchName,
                Location = branchDto.Location,
                PhoneNumber = branchDto.PhoneNumber
            };

            var createdBranch = await _branchRepository.CreateBranch(branch);
            return Ok(createdBranch);
        }
        [HttpGet("GetAllBranch")]
        public async Task<IActionResult> GetAllBranches()
        {
            var branches = await _branchRepository.GetAllBranches();
            return Ok(branches);
        }

        // Lấy chi nhánh theo ID
        [HttpGet("GetBranchByID")]
        public async Task<IActionResult> GetBranchById(int id)
        {
            var branch = await _branchRepository.GetBranchById(id);
            if (branch == null)
            {
                return NotFound("Branch not found.");
            }
            return Ok(branch);
        }

        // Tìm kiếm chi nhánh theo tên hoặc địa điểm
        [HttpGet("SearchBranch")]
        public async Task<IActionResult> SearchBranches(string keyword)
        {
            var branches = await _branchRepository.SearchBranches(keyword);
            if (!branches.Any())
            {
                return NotFound("No branches found matching the search criteria.");
            }
            return Ok(branches);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBranch(int id, [FromBody] UpdateBranchDTO updateBranchDto)
        {
            if (updateBranchDto == null ||
                string.IsNullOrEmpty(updateBranchDto.BranchName) ||
                string.IsNullOrEmpty(updateBranchDto.Location) ||
                string.IsNullOrEmpty(updateBranchDto.PhoneNumber))
            {
                return BadRequest("All fields are required.");
            }

            var branchToUpdate = new Branch
            {
                BranchName = updateBranchDto.BranchName,
                Location = updateBranchDto.Location,
                PhoneNumber = updateBranchDto.PhoneNumber
            };

            var updatedBranch = await _branchRepository.UpdateBranch(id, branchToUpdate);
            if (updatedBranch == null)
            {
                return NotFound($"Branch with ID {id} not found.");
            }

            return Ok(updatedBranch);
        }


        [HttpDelete("DeleteBranchByID")]
        public async Task<IActionResult> DeleteBranch(int id)
        {
            var result = await _branchRepository.DeleteBranch(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
