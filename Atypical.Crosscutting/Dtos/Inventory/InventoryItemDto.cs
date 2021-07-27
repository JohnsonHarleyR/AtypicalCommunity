using Atypical.Crosscutting.Enums;
using Atypical.Crosscutting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atypical.Crosscutting.Dtos.Inventory
{
    public class InventoryItemDto : IItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ItemType Type { get; set; }
        public int ItemId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public ItemColor Color { get; set; }
    }
}
