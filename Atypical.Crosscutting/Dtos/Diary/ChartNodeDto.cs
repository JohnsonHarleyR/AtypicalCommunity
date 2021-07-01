using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atypical.Crosscutting.Dtos.Diary
{
    public class ChartNodeDto
    {
        public string NodeName { get; set; }
        public double? Happy { get; set; }
        public double? Sad { get; set; }
        public double? Confident { get; set; }
        public double? Mad { get; set; }
        public double? Hopeful { get; set; }
        public double? Scared { get; set; }

    }
}
