using Atypical.Crosscutting.Dtos.Bank;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using System.Text;
using System.Threading.Tasks;

namespace Atypical.Data.Repositories.Bank
{
    public class BankRepository
    {
        private string Schema = @"[db_owner]";
        private string ConnectionString;

        public BankRepository()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["Atypical"].ConnectionString;
        }

        // check if the table exists
        public bool TableExists()
        {

            IEnumerable<BankAccountDto> accounts;

            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = $"{Schema}.GetAllBankAccounts";

                accounts = connection.Query<BankAccountDto>(sql,
                    commandType: System.Data.CommandType.StoredProcedure);

                try
                {
                    accounts = connection.Query<BankAccountDto>(sql,
                    commandType: System.Data.CommandType.StoredProcedure);

                    return true;

                }
                catch (Exception)
                {
                    return false;
                }

            }
        }

        // create table if it doesn't exist
        public void CreateTable()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = $"{Schema}.CreateBankTable";

                connection.Execute(sql,
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }


        /// <summary>
        /// Grab an entry based on the entry's Id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public BankAccountDto GetAccountByUserId(int userId)
        {

            // first check that table exists - if not, create the table
            if (!TableExists())
            {
                CreateTable();
            }

            BankAccountDto account;

            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = $"{Schema}.GetBankAccountByUserId";

                account = connection.Query<BankAccountDto>(sql,
                    new { UserId = userId },
                    commandType: System.Data.CommandType.StoredProcedure)?.FirstOrDefault();

            }

            return account;
        }


        /// <summary>
        /// Add a new bank account for a user.
        /// </summary>
        /// <param name="accountDto"></param>
        public void AddAccount(BankAccountDto accountDto)
        {

            // first check that table exists - if not, create the table
            if (!TableExists())
            {
                CreateTable();
            }

            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = $"{Schema}.AddBankAccount";

                connection.Execute(sql,
                    new
                    {
                        UserId = accountDto.UserId,
                        Checking = accountDto.Checking,
                        Savings = accountDto.Savings
                    },
                    commandType: System.Data.CommandType.StoredProcedure);
            }

        }

        /// <summary>
        /// Update a bank account.
        /// </summary>
        /// <param name="accountDto"></param>
        public void UpdateAccount(BankAccountDto accountDto)
        {

            // first check that table exists - if not, create the table
            if (!TableExists())
            {
                CreateTable();
            }

            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = $"{Schema}.UpdateBankAccount";

                connection.Execute(sql,
                    new
                    {
                        UserId = accountDto.UserId,
                        Checking = accountDto.Checking,
                        Savings = accountDto.Savings
                    },
                    commandType: System.Data.CommandType.StoredProcedure);
            }

        }




    }
}
