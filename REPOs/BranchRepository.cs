using BOs.Entity;
using DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOs
{
    public class BranchRepository : IBranchRepository
    {
        private readonly IBranchDAO _branchDAO;

        public BranchRepository(IBranchDAO branchDAO)
        {
            _branchDAO = branchDAO;
        }

        public async Task<Branch> CreateBranch(Branch branch)
        {
            return await _branchDAO.CreateBranch(branch);
        }
        public async Task<IEnumerable<Branch>> GetAllBranches()
        {
            return await _branchDAO.GetAllBranches();
        }

        public async Task<Branch?> GetBranchById(int id)
        {
            return await _branchDAO.GetBranchById(id);
        }

        public async Task<IEnumerable<Branch>> SearchBranches(string keyword)
        {
            return await _branchDAO.SearchBranches(keyword);
        }
        public async Task<Branch?> UpdateBranch(int id, Branch branch)
        {
            return await _branchDAO.UpdateBranch(id, branch);
        }

        public async Task<bool> DeleteBranch(int id)
        {
            return await _branchDAO.DeleteBranch(id);
        }
    }
}
