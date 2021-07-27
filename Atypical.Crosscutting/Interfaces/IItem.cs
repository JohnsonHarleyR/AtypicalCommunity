using Atypical.Crosscutting.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atypical.Crosscutting.Interfaces
{
    public interface IItem
    {
        int Id { get; set; }
        ItemType Type { get; set; }

        string Name { get; set; }
        string Description { get; set; }
        string IconUrl { get; set; }

        ItemColor Color { get; set; }

    }
}
