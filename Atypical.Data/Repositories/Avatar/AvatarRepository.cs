using Atypical.Crosscutting.Dtos.Avatar;
using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace Atypical.Data.Repositories.Avatar
{
    public class AvatarRepository
    {

        private string Schema = @"[db_owner]";
        private string ConnectionString;

        public AvatarRepository()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["Atypical"].ConnectionString;
        }

        // Avatar Items

        // Get list of all avatar items available
        public IEnumerable<AvatarItemDto> GetAllAvatarItems()
        {
            IEnumerable<AvatarItemDto> items;

            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = $"{Schema}.GetAllAvatarItems";

                items = connection.Query<AvatarItemDto>(sql, commandType: System.Data.CommandType.StoredProcedure);

            }
            return items;
        }


        public AvatarItemDto GetAvatarItemById(int id)
        {
            AvatarItemDto item;

            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = $"{Schema}.GetAvatarItemById";

                item = connection.Query<AvatarItemDto>(sql,
                    new { Id = id },
                    commandType: System.Data.CommandType.StoredProcedure)?.FirstOrDefault();

            }
            return item;
        }


        public void AddAvatarItem(AvatarItemDto itemDto)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = $"{Schema}.AddAvatarItem";

                connection.Execute(sql,
                                    new
                                    {
                                        Category = (int)itemDto.Category,
                                        SubCategory = (int)itemDto.SubCategory,
                                        Name = itemDto.Name,
                                        Description = itemDto.Description,
                                        Url = itemDto.Url,
                                        IconUrl = itemDto.IconUrl,
                                        Color = (int)itemDto.Color,
                                        Gender = (int)itemDto.Gender,
                                        Tags = itemDto.Tags
                                    },
                                    commandType: System.Data.CommandType.StoredProcedure);

            }
        }




        // User Avatars

        public AvatarDto GetAvatarById(int userId)
        {
            AvatarDto avatar;

            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = $"{Schema}.GetUserAvatarById";

                avatar = connection.Query<AvatarDto>(sql,
                    new { UserId = userId },
                    commandType: System.Data.CommandType.StoredProcedure)?.FirstOrDefault();

            }
            return avatar;
        }

        public void AddAvatar(AvatarDto avatarDto)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = $"{Schema}.AddUserAvatar";

                connection.Execute(sql,
                                    new
                                    {
                                        UserId = avatarDto.UserId,
                                        IsCreated = avatarDto.IsCreated,
                                        Background = avatarDto.Background,
                                        SecondaryBackground = avatarDto.SecondaryBackground,
                                        Foreground = avatarDto.Foreground,
                                        Base = avatarDto.Base,
                                        Tattoos = avatarDto.Tattoos,
                                        Marks = avatarDto.Marks,
                                        Eyes = avatarDto.Eyes,
                                        Nose = avatarDto.Nose,
                                        Mouth = avatarDto.Mouth,
                                        Makeup = avatarDto.Makeup,
                                        FacialHair = avatarDto.FacialHair,
                                        EarRings = avatarDto.EarRings,
                                        FacePiercings = avatarDto.FacePiercings,
                                        Necklace = avatarDto.Necklace,
                                        LeftArm = avatarDto.LeftArm,
                                        RightArm = avatarDto.RightArm,
                                        Hair = avatarDto.Hair,
                                        HairAccessory = avatarDto.HairAccessory,
                                        Hat = avatarDto.Hat,
                                        Top = avatarDto.Top,
                                        FullBody = avatarDto.FullBody,
                                        Neck = avatarDto.Neck,
                                        Bottom = avatarDto.Bottom,
                                        Shoes = avatarDto.Shoes,
                                        LeftAccessory = avatarDto.LeftAccessory,
                                        RightAccessory = avatarDto.RightAccessory,
                                        LeftHand = avatarDto.LeftHand,
                                        RightHand = avatarDto.RightHand
                                    },
                                    commandType: System.Data.CommandType.StoredProcedure);

            }
        }

        public void UpdateAvatar(AvatarDto avatarDto)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                string sql = $"{Schema}.UpdateUserAvatar";

                connection.Execute(sql,
                                    new
                                    {
                                        UserId = avatarDto.UserId,
                                        IsCreated = avatarDto.IsCreated,
                                        Background = avatarDto.Background,
                                        SecondaryBackground = avatarDto.SecondaryBackground,
                                        Foreground = avatarDto.Foreground,
                                        Base = avatarDto.Base,
                                        Tattoos = avatarDto.Tattoos,
                                        Marks = avatarDto.Marks,
                                        Eyes = avatarDto.Eyes,
                                        Nose = avatarDto.Nose,
                                        Mouth = avatarDto.Mouth,
                                        Makeup = avatarDto.Makeup,
                                        FacialHair = avatarDto.FacialHair,
                                        EarRings = avatarDto.EarRings,
                                        FacePiercings = avatarDto.FacePiercings,
                                        Necklace = avatarDto.Necklace,
                                        LeftArm = avatarDto.LeftArm,
                                        RightArm = avatarDto.RightArm,
                                        Hair = avatarDto.Hair,
                                        HairAccessory = avatarDto.HairAccessory,
                                        Hat = avatarDto.Hat,
                                        Top = avatarDto.Top,
                                        FullBody = avatarDto.FullBody,
                                        Neck = avatarDto.Neck,
                                        Bottom = avatarDto.Bottom,
                                        Shoes = avatarDto.Shoes,
                                        LeftAccessory = avatarDto.LeftAccessory,
                                        RightAccessory = avatarDto.RightAccessory,
                                        LeftHand = avatarDto.LeftHand,
                                        RightHand = avatarDto.RightHand
                                    },
                                    commandType: System.Data.CommandType.StoredProcedure);

            }
        }


    }
}
