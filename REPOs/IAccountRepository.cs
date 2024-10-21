using BOs.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOs
{
    public interface IAccountRepository
    {
        Guest GetAccount(string accountName);
    }
}
