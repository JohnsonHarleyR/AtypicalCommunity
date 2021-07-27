using Atypical.Crosscutting.Dtos.Inventory;
using Atypical.Crosscutting.Interfaces;
using Atypical.Domain.Orchestrators.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Atypical.Helpers
{
    public class InventoryHelper
    {

        public static void AddItemsToInventory(IEnumerable<IItem> items, int userId)
        {
            InventoryOrchestrator inventoryOrchestrator = new InventoryOrchestrator();

            foreach (var item in items)
            {
                if (item != null)
                {
                    InventoryItemDto inventoryItem = new InventoryItemDto()
                    {
                        UserId = userId,
                        Type = item.Type,
                        ItemId = item.Id,
                        Name = item.Name,
                        Description = item.Description,
                        IconUrl = item.IconUrl,
                        Color = item.Color
                    };

                    inventoryOrchestrator.AddInventoryItem(inventoryItem);
                }
                
            }

        }

    }
}