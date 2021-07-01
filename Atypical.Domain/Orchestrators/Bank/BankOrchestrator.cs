using Atypical.Crosscutting.Dtos.Bank;
using Atypical.Data.Repositories.Bank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atypical.Domain.Orchestrators.Bank
{

    class BankOrchestrator
    {

        private BankRepository bankRepository;

        public BankOrchestrator()
        {
            bankRepository = new BankRepository();
        }


        /// <summary>
        /// Add money to checking account. This is the default for gaining money.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="accountDto"></param>
        /// <returns></returns>
        public bool AddToChecking(double amount, BankAccountDto accountDto)
        {
            // if the dto is null, return false
            if (accountDto == null)
            {
                return false;
            }

            // add the amount to the dto account
            accountDto.Checking += amount;

            // update the account
            bankRepository.UpdateAccount(accountDto);

            // return true
            return true;
        }

        /// <summary>
        /// Take money from checking account. This is the default for spending money.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="accountDto"></param>
        /// <returns></returns>
        public bool RemoveFromChecking(double amount, BankAccountDto accountDto)
        {
            // if the dto is null, return false
            if (accountDto == null)
            {
                return false;
            }

            // subtract the amount from the dto account
            accountDto.Checking -= amount;

            // if the amount is now less than 0, return false - as it could not be done
            if (accountDto.Checking < 0)
            {
                return false;
            }

            // otherwise, update the account
            bankRepository.UpdateAccount(accountDto);

            // return true
            return true;
        }




        // Savings


        /// <summary>
        /// Add money to savings account. This is the default for gaining money.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="accountDto"></param>
        /// <returns></returns>
        public bool AddToSavings(double amount, BankAccountDto accountDto)
        {
            // if the dto is null, return false
            if (accountDto == null)
            {
                return false;
            }

            // add the amount to the dto account
            accountDto.Savings += amount;

            // update the account
            bankRepository.UpdateAccount(accountDto);

            // return true
            return true;
        }

        /// <summary>
        /// Take money from savings account. This is the default for spending money.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="accountDto"></param>
        /// <returns></returns>
        public bool RemoveFromSavings(double amount, BankAccountDto accountDto)
        {
            // if the dto is null, return false
            if (accountDto == null)
            {
                return false;
            }

            // subtract the amount from the dto account
            accountDto.Savings -= amount;

            // if the amount is now less than 0, return false - as it could not be done
            if (accountDto.Savings < 0)
            {
                return false;
            }

            // otherwise, update the account
            bankRepository.UpdateAccount(accountDto);

            // return true
            return true;
        }


    }
}
