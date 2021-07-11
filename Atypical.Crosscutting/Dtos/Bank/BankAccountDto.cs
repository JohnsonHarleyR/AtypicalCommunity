using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atypical.Crosscutting.Dtos.Bank
{
    public class BankAccountDto
    {
        public int UserId { get; set; }
        public int Checking { get; set; }
        public int Savings { get; set; }
    }
}
