using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Atypical.Web.Models.Diary
{
    public class NewEntryViewModel
    {
        // NOTE No DateAndTime is needed because the repo will add the current date and time
        // TODO Consider if we want to allow user to enter a different date and time to an entry?

        [Required]
        public int UserId { get; set; }
        [Required]
        public int Happy { get; set; }
        [Required]
        public int Sad { get; set; }
        [Required]
        public int Confident { get; set; }
        [Required]
        public int Mad { get; set; }
        [Required]
        public int Hopeful { get; set; }
        [Required]
        public int Scared { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [Remote("NoHtmlTags", "Entry", HttpMethod = "POST", ErrorMessage = "HTML tags are not allowed. Please use suggested BBCode.")]
        public string Text { get; set; }
    }
}