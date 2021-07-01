using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Atypical.Web.Models.Bank
{
    public class BankAccountViewModel
    {
        public int UserId { get; set; }
        public double Checking { get; set; }
        public double Savings { get; set; }
    }
}