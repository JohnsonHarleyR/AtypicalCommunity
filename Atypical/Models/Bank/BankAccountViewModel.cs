using Atypical.Crosscutting.Dtos.Bank;
using Atypical.Crosscutting.Dtos.User;
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

        public BankAccountViewModel() { }

        public BankAccountViewModel(BankAccountDto bankAccountDto)
        {

            UserId = bankAccountDto.UserId;
            Checking = bankAccountDto.Checking;
            Savings = bankAccountDto.Savings;

        }

    }
}