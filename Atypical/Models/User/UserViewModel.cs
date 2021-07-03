using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;
using Atypical.Crosscutting.Dtos.User;

namespace Atypical.Web.Models.User
{
    public class UserViewModel
    {
        [Required]
        [Remote("UsernameExists", "User", HttpMethod = "POST", ErrorMessage = "Username already exists.")]
        public string Username { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string ProfileImageUrl { get; set; }
        // TODO add validation that user is at least 13
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [EmailAddress]
        [Remote("EmailExists", "User", HttpMethod = "POST", ErrorMessage = "Email already exists.")]
        public string Email { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        [MaxLength(20, ErrorMessage = "Password cannot be more than 20 characters.")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        public bool IsEmailConfirmed { get; set; }
        public bool IsAdmin { get; set; }


        public UserViewModel() { }

        public UserViewModel(UserDto userDto)
        {
            Username = userDto.Username;
            FirstName = userDto.FirstName;
            ProfileImageUrl = userDto.ProfileImageUrl;
            DateOfBirth = userDto.DateOfBirth;
            Email = userDto.Email;
            Password = userDto.Password;
            IsEmailConfirmed = userDto.IsEmailConfirmed;
            IsAdmin = userDto.IsAdmin;
        }
    }
}