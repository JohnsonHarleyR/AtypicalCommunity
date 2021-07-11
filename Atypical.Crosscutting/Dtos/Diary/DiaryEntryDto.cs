using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atypical.Crosscutting.Dtos.Diary
{
    public class DiaryEntryDto
    {
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
