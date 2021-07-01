using System.Collections.Generic;
using Atypical.Crosscutting.Enums;

namespace Atypical.Web.Models.Diary
{
    public class ChartViewModel
    {
        public EChart Type { get; set; }
        public string Title { get; set; }
        public List<ChartNodeViewModel> Nodes { get; set; }
    }
}