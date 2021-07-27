using Atypical.Crosscutting.Dtos.Inventory;
using Atypical.Crosscutting.Enums;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atypical.Data.Repositories.Inventory
{
    public class InventoryRepository
    {

        private string Schema = @"[db_owner]";
        private string ConnectionString;

        public InventoryRepository()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["Atypical"].ConnectionString;
        }

        public void AddInventoryItem(InventoryItemDto itemDto)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = $"{Schema}.AddInventoryItem";

                connection.Execute(sql,
                                    new
                                    {
                                        UserId = itemDto.UserId,
                                        Type = (int)itemDto.Type,
                                        ItemId = itemDto.ItemId,
                                        Name = itemDto.Name,
                                        Description = itemDto.Description,
                                        IconUrl = itemDto.IconUrl,
                                        Color = (int)itemDto.Color,
                                    },
                                    commandType: System.Data.CommandType.StoredProcedure);

            }
        }


        public IEnumerable<InventoryItemDto> GetInventoryItems(int userId)
        {
            IEnumerable<InventoryItemDto> items;

            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = $"{Schema}.GetInventoryItems";

                items = connection.Query<InventoryItemDto>(sql,
                    new { UserId = userId },
                    commandType: System.Data.CommandType.StoredProcedure);

            }
            return items;
        }

        public IEnumerable<InventoryItemDto> GetInventoryItemsByType(int userId, ItemType type)
        {
            IEnumerable<InventoryItemDto> items;

            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = $"{Schema}.GetInventoryItemsByType";

                items = connection.Query<InventoryItemDto>(sql,
                    new { 
                        UserId = userId,
                        ItemType = (int)type
                    },
                    commandType: System.Data.CommandType.StoredProcedure);

            }
            return items;
        }


    }
}
