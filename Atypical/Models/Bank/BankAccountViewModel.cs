using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Atypical.Web.Models.Bank
{
    public class BankAccountViewModel
    {
        public int UserId { get; set; }
        public int Checking { get; set; }
        public int Savings { get; set; }
    }
}