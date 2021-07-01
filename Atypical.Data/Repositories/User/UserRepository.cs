using Atypical.Crosscutting.Dtos.User;
using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Data.Sqlite;

namespace Atypical.Data.Repositories.User
{
    public class UserRepository
    {
        //private string schema = @"mood_diary";
        private string ConnectionString;

        public UserRepository()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["SqliteConnection"].ConnectionString;
        }

        // Add a new user
        public void AddUser(UserDto userDto)
        {

            using (var connection = new SqliteConnection(ConnectionString))
            {
                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_winsqlite3());

                string sql = $@"INSERT INTO User " +
                    $@"(Username, FirstName, ProfileImageUrl, DateOfBirth, Email, Password, IsEmailConfirmed, IsAdmin)" +
                    $@"VALUES (@Username, @FirstName, @ProfileImageUrl, @DateOfBirth, @Email, @Password, @IsEmailConfirmed, @IsAdmin)";

                SqliteCommand command = new SqliteCommand(sql, connection);

                command.Parameters.AddWithValue("@Username", userDto.Username);
                command.Parameters.AddWithValue("@FirstName", userDto.FirstName);
                command.Parameters.AddWithValue("@ProfileImageUrl", userDto.ProfileImageUrl);
                command.Parameters.AddWithValue("@DateOfBirth", userDto.DateOfBirth);
                command.Parameters.AddWithValue("@Email", userDto.Email);
                command.Parameters.AddWithValue("@Password", userDto.Password);
                command.Parameters.AddWithValue("@IsEmailConfirmed", userDto.IsEmailConfirmed);
                command.Parameters.AddWithValue("@IsAdmin", userDto.IsAdmin);

                connection.Open();

                command.ExecuteNonQuery();

                connection.Close();

            }

        }

        // update a user
        public void UpdateUser(UserDto userDto)
        {

            using (var connection = new SqliteConnection(ConnectionString))
            {
                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_winsqlite3());

                string sql = $@"UPDATE User " +
                    "SET Username = @Username, FirstName = @FirstName, ProfileImageUrl = ProfileImageUrl, " +
                    $@"DateOfBirth = @DateOfBirth, Email = @Email, Password = @Password, " + 
                    $@"IsEmailConfirmed = @IsEmailConfirmed, IsAdmin = @IsAdmin " + 
                    $@"WHERE Id = @Id";

                SqliteCommand command = new SqliteCommand(sql, connection);

                command.Parameters.AddWithValue("@Id", userDto.Id);
                command.Parameters.AddWithValue("@Username", userDto.Username);
                command.Parameters.AddWithValue("@FirstName", userDto.FirstName);
                command.Parameters.AddWithValue("@ProfileImageUrl", userDto.ProfileImageUrl);
                command.Parameters.AddWithValue("@DateOfBirth", userDto.DateOfBirth);
                command.Parameters.AddWithValue("@Email", userDto.Email);
                command.Parameters.AddWithValue("@Password", userDto.Password);
                command.Parameters.AddWithValue("@IsEmailConfirmed", userDto.IsEmailConfirmed);
                command.Parameters.AddWithValue("@IsAdmin", userDto.IsAdmin);

                connection.Open();

                command.ExecuteNonQuery();

                connection.Close();

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
        public List<UserDto> GetAllUsers()
        {
            List<UserDto> users = new List<UserDto>();

            using (var connection = new SqliteConnection(ConnectionString))
            {

                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_winsqlite3());

                string sql = $@"SELECT * FROM User";

                SqliteCommand command = new SqliteCommand(sql, connection);


                connection.Open();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(
                            new UserDto()
                            {
                                Id = Int32.Parse(reader["Id"].ToString()),
                                Username = reader["Username"].ToString(),
                                FirstName = reader["FirstName"].ToString(),
                                ProfileImageUrl = reader["ProfileImageUrl"].ToString(),
                                DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                                Email = reader["Email"].ToString(),
                                Password = reader["Password"].ToString(),
                                IsEmailConfirmed = GetBoolFromBit(reader["IsEmailConfirmed"].ToString()),
                                IsAdmin = GetBoolFromBit(reader["IsAdmin"].ToString())
                            });

                        // TODO add all diary entries to user

                    }

                    connection.Close();
                }

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
            UserDto user = null;

            using (var connection = new SqliteConnection(ConnectionString))
            {

                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_winsqlite3());

                string sql = $@"SELECT * FROM User WHERE Id = @Id;";

                SqliteCommand command = new SqliteCommand(sql, connection);

                command.Parameters.AddWithValue("@Id", id);

                connection.Open();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        user = new UserDto()
                        {
                            Id = Int32.Parse(reader["Id"].ToString()),
                            Username = reader["Username"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            ProfileImageUrl = reader["ProfileImageUrl"].ToString(),
                            DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                            Email = reader["Email"].ToString(),
                            Password = reader["Password"].ToString(),
                            IsEmailConfirmed = GetBoolFromBit(reader["IsEmailConfirmed"].ToString()),
                            IsAdmin = GetBoolFromBit(reader["IsAdmin"].ToString())
                            //Entries = new List<EntryDto>() { }
                        };
                    }
                    connection.Close();
                }

                // TODO add all diary entries to user

            }
            return user;
        }

        public UserDto GetUserByUsername(string username)
        {
            UserDto user = null;

            using (var connection = new SqliteConnection(ConnectionString))
            {

                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_winsqlite3());

                string sql = $@"SELECT * FROM User WHERE Username = @Username;";

                SqliteCommand command = new SqliteCommand(sql, connection);

                command.Parameters.AddWithValue("@Username", username);

                connection.Open();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        user = new UserDto()
                        {
                            Id = Int32.Parse(reader["Id"].ToString()),
                            Username = reader["Username"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            ProfileImageUrl = reader["ProfileImageUrl"].ToString(),
                            DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                            Email = reader["Email"].ToString(),
                            Password = reader["Password"].ToString(),
                            IsEmailConfirmed = GetBoolFromBit(reader["IsEmailConfirmed"].ToString()),
                            IsAdmin = GetBoolFromBit(reader["IsAdmin"].ToString())
                            //Entries = new List<EntryDto>() { }
                        };
                    }
                    connection.Close();
                }

                // TODO add all diary entries to user (?)

            }
            return user;
        }


        public UserDto GetUserByEmail(string email)
        {
            UserDto user = null;

            using (var connection = new SqliteConnection(ConnectionString))
            {

                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_winsqlite3());

                string sql = $@"SELECT * FROM User WHERE Email = @Email;";

                SqliteCommand command = new SqliteCommand(sql, connection);

                command.Parameters.AddWithValue("@Email", email);

                connection.Open();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        user = new UserDto()
                        {
                            Id = Int32.Parse(reader["Id"].ToString()),
                            Username = reader["Username"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            ProfileImageUrl = reader["ProfileImageUrl"].ToString(),
                            DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                            Email = reader["Email"].ToString(),
                            Password = reader["Password"].ToString(),
                            IsEmailConfirmed = GetBoolFromBit(reader["IsEmailConfirmed"].ToString()),
                            IsAdmin = GetBoolFromBit(reader["IsAdmin"].ToString())
                            //Entries = new List<EntryDto>() { }
                        };
                    }
                    connection.Close();
                }

                // TODO add all diary entries to user (?)

            }
            return user;
        }

    }


}