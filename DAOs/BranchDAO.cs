using BOs.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public class BranchDAO : IBranchDAO
    {
        private readonly BookroomSwdContext _context;

        public BranchDAO(BookroomSwdContext context)
        {
            _context = context;
        }

        public async Task<Branch> CreateBranch(Branch branch)
        {
            _context.Branches.Add(branch);
            await _context.SaveChangesAsync();
            return branch;
        }
        public async Task<IEnumerable<Branch>> GetAllBranches()
        {
            return await _context.Branches.ToListAsync();
        }

        public async Task<Branch?> GetBranchById(int id)
        {
            return await _context.Branches.FindAsync(id);
        }

        public async Task<IEnumerable<Branch>> SearchBranches(string keyword)
        {
            return await _context.Branches
                                 .Where(b => b.BranchName.Contains(keyword) || b.Location.Contains(keyword))
                                 .ToListAsync();
        }
        public async Task<Branch?> UpdateBranch(int id, Branch branch)
        {
            var existingBranch = await GetBranchById(id);
            if (existingBranch == null)
            {
                return null; // Chi nhánh không tồn tại
            }

            // Cập nhật các trường mà không cần cập nhật ID
            existingBranch.BranchName = branch.BranchName;
            existingBranch.Location = branch.Location;
            existingBranch.PhoneNumber = branch.PhoneNumber;

            await _context.SaveChangesAsync();
            return existingBranch;
        }

        public async Task<bool> DeleteBranch(int id)
        {
            var branch = await GetBranchById(id);
            if (branch == null) return false;

            _context.Branches.Remove(branch);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
