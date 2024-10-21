using BOs.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public interface IBranchDAO
    {
        Task<Branch> CreateBranch(Branch branch);
        Task<IEnumerable<Branch>> GetAllBranches();
        Task<Branch?> GetBranchById(int id);
        Task<IEnumerable<Branch>> SearchBranches(string keyword);

        Task<Branch?> UpdateBranch(int id, Branch branch);
        Task<bool> DeleteBranch(int id);

    }
}
