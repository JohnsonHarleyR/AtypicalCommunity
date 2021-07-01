using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atypical.Crosscutting.Enums;

namespace Atypical.Crosscutting.Dtos.Diary
{
    public class FullChartDto
    {
        public EChart Type { get; set; }
        public string Title { get; set; }
        public List<ChartNodeDto> Nodes { get; set; }

    }
}
