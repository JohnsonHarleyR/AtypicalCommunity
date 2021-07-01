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
        /// Get an account based on a user's Id number. If it doesn't exist, create a new account to return.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public BankAccountDto GetAccountByUserId(int userId)
        {
            // grab the account
            BankAccountDto newAccountDto = bankRepository.GetAccountByUserId(userId);

            // if the account is null, create a new account for that user
            if (newAccountDto == null)
            {
                // attempt to create an account for that user
                bool success = CreateBankAccount(userId);

                // if successful, grab that new bank account and return it
                if (success)
                {
                    BankAccountDto accountDto = bankRepository.GetAccountByUserId(userId);
                    if (accountDto != null)
                    {
                        return accountDto;
                    }
                }

                // otherwise, return null
                return null;
            }

            // otherwise, return the accountDto
            return newAccountDto;

        }


        // create a new bank account based on a user's id. This will automatically start with a default amount.
        public bool CreateBankAccount(int userId)
        {
            // create account
            BankAccountDto newAccount = new BankAccountDto()
            {
                UserId = userId,
                Checking = 0,
                Savings = 100
            };

            // try to add it to the database - if it doesn't work, that means there is probably already an account for that id
            try
            {
                bankRepository.AddAccount(newAccount);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public bool TransferBetweenAccounts(double amount, 
            BankAccountDto sendingAccount, BankAccountDto receivingAccount)
        {
            // if eith dto is null, return false
            if (sendingAccount == null || receivingAccount == null)
            {
                return false;
            }

            // subtract the amount from the dto account
            sendingAccount.Checking -= amount;

            // if the amount is now less than 0, return false - as it could not be done
            if (sendingAccount.Checking < 0)
            {
                return false;
            }

            // otherwise, add the amount to Savings
            receivingAccount.Savings += amount;

            // update the accounts
            bankRepository.UpdateAccount(sendingAccount);
            bankRepository.UpdateAccount(receivingAccount);

            // return true
            return true;
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

        /// <summary>
        /// Take money from checking account and put it in savings.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="accountDto"></param>
        /// <returns></returns>
        public bool TransferCheckingToSavings(double amount, BankAccountDto accountDto)
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

            // otherwise, add the amount to Savings
            accountDto.Savings += amount;

            // update the account
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

            // if the amount is now less than 5, return false - as it could not be done
            if (accountDto.Savings < 5)
            {
                return false;
            }

            // otherwise, update the account
            bankRepository.UpdateAccount(accountDto);

            // return true
            return true;
        }

        /// <summary>
        /// Take money from savings account and put it in checking.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="accountDto"></param>
        /// <returns></returns>
        public bool TransferSavingsToChecking(double amount, BankAccountDto accountDto)
        {
            // if the dto is null, return false
            if (accountDto == null)
            {
                return false;
            }

            // subtract the amount from the dto account
            accountDto.Savings -= amount;

            // if the amount is now less than 5, return false - as it could not be done
            if (accountDto.Savings < 5)
            {
                return false;
            }

            // otherwise, add the amount to Savings
            accountDto.Checking += amount;

            // update the account
            bankRepository.UpdateAccount(accountDto);

            // return true
            return true;
        }


    }
}
