using BOs.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOs
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BookroomSwdContext _context;

        public AccountRepository(BookroomSwdContext context)
        {
            _context = context;
        }

        public Guest GetAccount(string email)
        {
            return _context.Guests.FirstOrDefault(g => g.Email == email);
        }
    }
}
