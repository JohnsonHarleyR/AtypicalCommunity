using Atypical.Crosscutting.Dtos.User;
using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Data.Sqlite;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

namespace Atypical.Data.Repositories.User
{
    public class UserRepository
    {
        private string Schema = @"[db_owner]";
        private string ConnectionString;

        public UserRepository()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["Atypical"].ConnectionString;
        }

        // check if the table exists
        public bool TableExists()
        {

            IEnumerable<UserDto> users;

            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = $"{Schema}.GetAllUsers";

                users = connection.Query<UserDto>(sql,
                    commandType: System.Data.CommandType.StoredProcedure);

                try
                {
                    users = connection.Query<UserDto>(sql,
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
                string sql = $"{Schema}.CreateUserTable";

                connection.Execute(sql,
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        // Add a new user
        public void AddUser(UserDto userDto)
        {

            // first check that table exists - if not, create the table
            if (!TableExists())
            {
                CreateTable();
            }

            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = $"{Schema}.AddUser";

                connection.Execute(sql,
                    new
                    {
                        FirstName = userDto.FirstName,
                        ProfileImageUrl = userDto.ProfileImageUrl,
                        DateOfBirth = userDto.DateOfBirth,
                        Email = userDto.Email,
                        Password = userDto.Password,
                        IsEmailConfirmed = userDto.IsEmailConfirmed,
                        UserType = (int)userDto.UserType
                    },
                    commandType: System.Data.CommandType.StoredProcedure);
            }


        }

        // update a user
        public void UpdateUser(UserDto userDto)
        {

            // first check that table exists - if not, create the table
            if (!TableExists())
            {
                CreateTable();
            }

            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = $"{Schema}.UpdateUser";

                connection.Execute(sql,
                    new
                    {
                        Id = userDto.Id,
                        FirstName = userDto.FirstName,
                        ProfileImageUrl = userDto.ProfileImageUrl,
                        DateOfBirth = userDto.DateOfBirth,
                        Email = userDto.Email,
                        Password = userDto.Password,
                        IsEmailConfirmed = userDto.IsEmailConfirmed,
                        UserType = (int)userDto.UserType
                    },
                    commandType: System.Data.CommandType.StoredProcedure);
            }

        }

        public int GetBoolValue(bool value)
        {
            if (value == true)
            {
                return 1;
            }
            return 0;
        }


        /// <summary>
        /// Get all users in the database.
        /// </summary>
        /// <returns>List of userDto's from the database.</returns>
        public IEnumerable<UserDto> GetAllUsers()
        {
            // first check that table exists - if not, create the table
            if (!TableExists())
            {
                CreateTable();
            }

            IEnumerable<UserDto> users = new List<UserDto>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = $"{Schema}.GetAllUsers";

                users = connection.Query<UserDto>(sql,
                    commandType: System.Data.CommandType.StoredProcedure);

            }

            return users;
        }

        // Helper method
        public bool GetBoolFromBit(string bit)
        {
            if (bit == "1")
            {
                return true;
            }
            else if (bit == "0")
            {
                return false;
            }
            throw new Exception("Invalid bit string - not equal to needed values.");
        }

        // Methods to get a user by an id, email, or username

        public UserDto GetUserById(int id)
        {

            // first check that table exists - if not, create the table
            if (!TableExists())
            {
                CreateTable();
            }

            UserDto user = null;

            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = $"{Schema}.GetUserId";

                user = connection.Query<UserDto>(sql,
                    new { Id = id },
                    commandType: System.Data.CommandType.StoredProcedure)?.FirstOrDefault();

            }

            // TODO add all diary entries to user


            return user;
        }

        public UserDto GetUserByUsername(string username)
        {
            // first check that table exists - if not, create the table
            if (!TableExists())
            {
                CreateTable();
            }

            UserDto user = null;

            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = $"{Schema}.GetUserByUsername";

                user = connection.Query<UserDto>(sql,
                    new { Username = username },
                    commandType: System.Data.CommandType.StoredProcedure)?.FirstOrDefault();

            }

            // TODO add all diary entries to user


            return user;
        }


        public UserDto GetUserByEmail(string email)
        {
            // first check that table exists - if not, create the table
            if (!TableExists())
            {
                CreateTable();
            }

            UserDto user = null;

            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = $"{Schema}.GetUserByEmail";

                user = connection.Query<UserDto>(sql,
                    new { Email = email },
                    commandType: System.Data.CommandType.StoredProcedure)?.FirstOrDefault();

            }

            // TODO add all diary entries to user


            return user;

        }

    }
}