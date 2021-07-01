using System;

namespace Atypical.Web.Models.Diary
{
    public class EntryViewModel
    {
        // NOTE No DateAndTime is needed because the repo will add the current date and time
        // TODO Consider if we want to allow user to enter a different date and time to an entry?

        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime DateAndTime { get; set; }
        public int Happy { get; set; }
        public int Sad { get; set; }
        public int Confident { get; set; }
        public int Mad { get; set; }
        public int Hopeful { get; set; }
        public int Scared { get; set; }

        public string Title { get; set; }
        public string Text { get; set; }
    }
}