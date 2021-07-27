using Atypical.Crosscutting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atypical.Crosscutting.Dtos.Inventory
{
    class InventoryDto
    {
        public int UserId { get; set; }
        public List<IItem> InventoryList { get; set; }
    }
}
