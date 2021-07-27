using Atypical.Crosscutting.Enums;
using System.Collections.Generic;

namespace Atypical.Models.Avatar
{
    public class AvatarItemViewModel
    {
        public int Id { get; set; }

        public string Category { get; set; }
        public string SubCategory { get; set; }

        public string Name { get; set; } // 50 char
        public string Description { get; set; } // 100 char

        public string Url { get; set; } // 100 char
        public string IconUrl { get; set; } // 100 char

        public ItemColor Color { get; set; }
        public AvatarGender Gender { get; set; }
        public List<string> Tags { get; set; } // max 10 tags
    }
}