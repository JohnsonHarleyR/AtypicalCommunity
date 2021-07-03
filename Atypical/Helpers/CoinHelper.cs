using Atypical.Crosscutting.Dtos.Bank;
using Atypical.Crosscutting.Dtos.User;
using Atypical.Domain.Orchestrators.Bank;
using Atypical.Domain.Orchestrators.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Atypical.Helpers
{
    public static class CoinHelper
    {
        private static Random random = new Random();
        private static BankOrchestrator bankOrchestrator = new BankOrchestrator();


        // generate a random amount of coin based on a min and max for the user.
        public static void GenerateCoin(int userId, int min, int max)
        {
            // grab the bank account
            BankAccountDto account = bankOrchestrator.GetAccountByUserId(userId);

            // generate a random amount based on the min and max
            int amount = random.Next(min, max);

            // add that amount to the account
            bankOrchestrator.AddToChecking(amount, account);

        }

    }
}