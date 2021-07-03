using Atypical.Crosscutting.Dtos.Bank;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atypical.Data.Repositories.Bank
{
    public class BankRepository
    {
        private string ConnectionString;

        public BankRepository()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["SqliteConnection"].ConnectionString;
        }

        // check if the table exists
        public bool TableExists()
        {

            using (var connection = new SqliteConnection(ConnectionString))
            {

                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_winsqlite3());

                string sql = $@"SELECT * FROM Bank";

                SqliteCommand command = new SqliteCommand(sql, connection);


                connection.Open();

                try
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            // success

                        }

                        connection.Close();
                        return true;
                    }

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
            using (var connection = new SqliteConnection(ConnectionString))
            {
                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_winsqlite3());

                string sql = $@"CREATE TABLE Bank (
    UserId   INTEGER PRIMARY KEY,
    Checking INT     DEFAULT (0) 
                     NOT NULL,
    Savings  INT     NOT NULL
                     DEFAULT (100) 
);
";

                SqliteCommand command = new SqliteCommand(sql, connection);

                connection.Open();

                command.ExecuteNonQuery();

                connection.Close();

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

            BankAccountDto account = null;

            using (var connection = new SqliteConnection(ConnectionString))
            {

                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_winsqlite3());

                string sql = $@"SELECT * FROM Bank WHERE UserId = @UserId;";

                SqliteCommand command = new SqliteCommand(sql, connection);

                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        account = new BankAccountDto()
                        {
                            UserId = Int32.Parse(reader["UserId"].ToString()),
                            Checking = Int32.Parse(reader["Checking"].ToString()),
                            Savings = Int32.Parse(reader["Savings"].ToString())
                        };
                    }
                    connection.Close();
                }

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

            using (var connection = new SqliteConnection(ConnectionString))
            {
                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_winsqlite3());

                string sql = $@"INSERT INTO Bank " +
                    $@"(UserId, Checking, Savings)" +
                    $@"VALUES (@UserId, @Checking, @Savings)";

                SqliteCommand command = new SqliteCommand(sql, connection);

                // TODO Decide if there should be anything that can't be null - there probably should be lol

                command.Parameters.AddWithValue("@UserId", accountDto.UserId);
                command.Parameters.AddWithValue("@Checking", accountDto.Checking);
                command.Parameters.AddWithValue("@Savings", accountDto.Savings);

                connection.Open();

                command.ExecuteNonQuery();

                connection.Close();

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

            using (var connection = new SqliteConnection(ConnectionString))
            {
                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_winsqlite3());

                string sql = $@"UPDATE Bank " +
                    $@"SET Checking = @Checking, Savings = @Savings " +
                    $@"WHERE UserId = @UserId;";

                SqliteCommand command = new SqliteCommand(sql, connection);

                command.Parameters.AddWithValue("@UserId", accountDto.UserId);

                command.Parameters.AddWithValue("@Checking", accountDto.Checking);

                command.Parameters.AddWithValue("@Savings", accountDto.Savings);

                connection.Open();

                command.ExecuteNonQuery();

                connection.Close();

            }

        }




    }
}
