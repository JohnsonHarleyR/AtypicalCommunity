using Atypical.Crosscutting.Enums;
using Atypical.Crosscutting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atypical.Crosscutting.Dtos.Avatar
{
    public class AvatarItemDto : IItem
    {
        public int Id { get; set; }

        public ItemType Type { get; set; } = ItemType.Avatar;

        public AvatarItemCategory Category { get; set; }
        public AvatarItemSubCategory SubCategory { get; set; }

        public string Name { get; set; } // 50 char
        public string Description { get; set; } // 100 char

        public string Url { get; set; } // 100 char
        public string IconUrl { get; set; } // 100 char

        public ItemColor Color { get; set; }
        public AvatarGender Gender { get; set; }
        public string Tags { get; set; } // 100 char
    }
}
