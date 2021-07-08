using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atypical.Data.Repositories.User;
using Atypical.Crosscutting.Dtos.User;
using System.Security.Cryptography;
using System.Net;
using System.Net.Mail;
using SQLitePCL;

namespace Atypical.Domain.Orchestrators.User
{
    public class UserOrchestrator
    {

        private UserRepository userRepository;

        public UserOrchestrator()
        {
            userRepository = new UserRepository();
        }

 

        public void SqliteHack()
        {
            NativeLibraryHack.DoHack();
        }


        // Create a new User
        public bool CreateUser(UserDto userDto)
        {
            // if the dto is null, return false
            if (userDto == null)
            {
                return false;
            }

            // Add the dto to the repo
            userRepository.AddUser(userDto);


            // return true that it was successful
            return true;

        }

        // update a user
        public bool UpdateUser(UserDto userDto)
        {
            // if the dto is null, return false
            if (userDto == null)
            {
                return false;
            }

            // Add the dto to the repo
            userRepository.UpdateUser(userDto);


            // return true that it was successful
            return true;

        }

        // Get all userDto's
        public List<UserDto> GetAllUsers()
        {
            List<UserDto> userDtos = userRepository.GetAllUsers();

            if (userDtos == null)
            {
                return null;
            }

            // TODO Grab all their diary entries and store in their list of entries (?)

            return userDtos;

        }


        // Get a userDto by the user's id
        public UserDto GetUserById(int id)
        {
            UserDto userDto = userRepository.GetUserById(id);

            if (userDto == null)
            {
                return null;
            }

            // TODO Grab all their diary entries and store in their list of entries (?)

            return userDto;

        }

        // Get a userDto by the user's email
        public UserDto GetUserByEmail(string email)
        {
            UserDto userDto = userRepository.GetUserByEmail(email);

            if (userDto == null)
            {
                return null;
            }

            // TODO Grab all their diary entries and store in their list of entries (?)

            return userDto;

        }


        // Methods to check if a user exists
        public bool DoesUserExistByUsername(string username)
        {
            if (username == null)
            {
                return false;
            }

            UserDto userDto = userRepository.GetUserByUsername(username);

            if (userDto == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public bool DoesUserExistByEmail(string email)
        {
            if (email == null)
            {
                return false;
            }

            UserDto userDto = userRepository.GetUserByEmail(email);

            if (userDto == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }


        // Methods to grab a user by username or email
        public int GetUserIdByUsername(string username)
        {
            if (username == null)
            {
                throw new ArgumentNullException();
            }

            UserDto userDto = userRepository.GetUserByUsername(username);

            return userDto.Id;

        }

        public int GetUserIdByEmail(string email)
        {
            if (email == null)
            {
                throw new ArgumentNullException();
            }

            UserDto userDto = userRepository.GetUserByEmail(email);

            return userDto.Id;

        }




        /// <summary>
        /// Scramble a password to make it more secure, or to match a password in the database.
        /// </summary>
        /// <param name="password"></param>
        /// <returns>A password string that has been hashed for security.</returns>
        public string SecurePassword(string password)
        {
            using (var sha256 = new SHA256Managed())
            {
                // hash the password
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // turn it into an appropriate string
                string securePassword = BitConverter.ToString(hashedBytes)
                    .Replace("-", "").ToLower();

                // TODO Salt the password for extra security

                // trim new password down to 100 characters if it is more than 100 - to fit in db
                if (securePassword.Length > 100)
                {
                    securePassword = securePassword.Substring(0, 100);
                }

                // return the password
                return securePassword;
            }
        }

    }
}
