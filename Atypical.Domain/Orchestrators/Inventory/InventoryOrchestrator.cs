using Atypical.Crosscutting.Dtos.Inventory;
using Atypical.Crosscutting.Enums;
using Atypical.Data.Repositories.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atypical.Domain.Orchestrators.Inventory
{
    public class InventoryOrchestrator
    {

        private InventoryRepository inventoryRepository = new InventoryRepository();


        public bool AddInventoryItem(InventoryItemDto itemDto)
        {
            if (itemDto == null)
            {
                return false;
            }

            inventoryRepository.AddInventoryItem(itemDto);

            return true;
        }


        public IEnumerable<InventoryItemDto> GetInventoryItems(int userId)
        {
            IEnumerable<InventoryItemDto> items = inventoryRepository.GetInventoryItems(userId);

            if (items == null)
            {
                return null;
            }

            return items;

        }


        public IEnumerable<InventoryItemDto> GetInventoryItemsByType(int userId,
            ItemType type)
        {
            IEnumerable<InventoryItemDto> items = inventoryRepository
                .GetInventoryItemsByType(userId, type);

            if (items == null)
            {
                return null;
            }

            return items;

        }


    }
}
