using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Atypical.Web.Models.User
{
    public class CheckCodeViewModel
    {
        public string Email { get; set; }
        public string CodeForReset { get; set; } // the correct code
        [Required]
        [Remote("CodeMatches", "User", HttpMethod = "POST",
            ErrorMessage = "Verification code is not correct.")]
        public string CodeEntered { get; set; }
    }
}