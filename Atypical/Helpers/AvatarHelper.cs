using Atypical.Crosscutting.Dtos.Avatar;
using Atypical.Crosscutting.Dtos.Inventory;
using Atypical.Crosscutting.Enums;
using Atypical.Domain.Orchestrators.Avatar;
using Atypical.Models.Avatar;
using System;
using System.Collections.Generic;

namespace Atypical.Helpers
{
    public static class AvatarHelper
    {

        // Get all avatar item categories
        // TODO test this
        public static List<AvatarItemCategory> GetAllCategories()
        {
            int categoryCount = Enum.GetNames(typeof(AvatarItemCategory)).Length;
            List<AvatarItemCategory> categoryList = new List<AvatarItemCategory>();

            for (int i = 0; i < categoryCount; i++)
            {
                categoryList.Add((AvatarItemCategory)i);
            }

            return categoryList;
        }

        // Get avatar item sub categories based on a category
        // TODO test this
        public static List<AvatarItemSubCategory> GetSubCategories(AvatarItemCategory category)
        {
            List<AvatarItemSubCategory> subCategories = new List<AvatarItemSubCategory>();

            switch (category)
            {
                case AvatarItemCategory.Backdrop:
                    subCategories.Add(AvatarItemSubCategory.Background);
                    subCategories.Add(AvatarItemSubCategory.SecondaryBackground);
                    subCategories.Add(AvatarItemSubCategory.Foreground);
                    break;
                case AvatarItemCategory.Skin:
                    subCategories.Add(AvatarItemSubCategory.Base);
                    subCategories.Add(AvatarItemSubCategory.Tattoos);
                    subCategories.Add(AvatarItemSubCategory.Marks);
                    break;
                case AvatarItemCategory.Face:
                    subCategories.Add(AvatarItemSubCategory.Eyes);
                    subCategories.Add(AvatarItemSubCategory.Nose);
                    subCategories.Add(AvatarItemSubCategory.Mouth);
                    subCategories.Add(AvatarItemSubCategory.Makeup);
                    break;
                case AvatarItemCategory.Jewelry:
                    subCategories.Add(AvatarItemSubCategory.EarRings);
                    subCategories.Add(AvatarItemSubCategory.FacePiercing);
                    subCategories.Add(AvatarItemSubCategory.Necklace);
                    subCategories.Add(AvatarItemSubCategory.LeftArm);
                    subCategories.Add(AvatarItemSubCategory.RightArm);
                    break;
                case AvatarItemCategory.Head:
                    subCategories.Add(AvatarItemSubCategory.Hair);
                    subCategories.Add(AvatarItemSubCategory.HairAccessory);
                    subCategories.Add(AvatarItemSubCategory.Hat);
                    break;
                case AvatarItemCategory.Torso:
                    subCategories.Add(AvatarItemSubCategory.Top);
                    subCategories.Add(AvatarItemSubCategory.FullBody);
                    subCategories.Add(AvatarItemSubCategory.Neck);
                    break;
                case AvatarItemCategory.Accessories:
                    subCategories.Add(AvatarItemSubCategory.LeftAccessory);
                    subCategories.Add(AvatarItemSubCategory.RightAccessory);
                    break;
                case AvatarItemCategory.Hands:
                    subCategories.Add(AvatarItemSubCategory.LeftHand);
                    subCategories.Add(AvatarItemSubCategory.RightHand);
                    break;
                default:
                    subCategories = null;
                    break;
            }

            return subCategories;
        }

        // TODO create function to get category based on sub category



        // Avatar Items
        public static List<AvatarItemDto> GetAvatarItemsFromInventoryList(List<InventoryItemDto> items)
        {
            // Get orchestrator
            AvatarOrchestrator avatarOrchestrator = new AvatarOrchestrator();

            // create list to return
            List<AvatarItemDto> avatarItems = new List<AvatarItemDto>();

            // loop through items
            foreach (var item in items)
            {
                // make sure the item is an avatar item
                if (item.Type == ItemType.Avatar)
                {
                    AvatarItemDto avatarItem = 
                        avatarOrchestrator.GetAvatarItemById(item.ItemId);
                    avatarItems.Add(avatarItem);
                }
            }

            return avatarItems;
        }
        public static AvatarItemViewModel GetAvatarItemModel(AvatarItemDto itemDto)
        {
            AvatarItemViewModel itemModel = new AvatarItemViewModel()
            {
                Id = itemDto.Id,
                Category = itemDto.Category.ToString(),
                SubCategory = itemDto.SubCategory.ToString(),
                Name = itemDto.Name,
                Description = itemDto.Description,
                Url = itemDto.Url,
                IconUrl = itemDto.IconUrl,
                Color = itemDto.Color,
                Gender = itemDto.Gender,
                Tags = GetTagsFromString(itemDto.Tags)
            };
            return itemModel;
        }

        public static List<AvatarItemViewModel> GetAvatarItemModelList(List<AvatarItemDto> itemDtos)
        {
            List<AvatarItemViewModel> itemModels = new List<AvatarItemViewModel>();

            foreach (var itemDto in itemDtos)
            {
                AvatarItemViewModel itemModel = new AvatarItemViewModel()
                {
                    Id = itemDto.Id,
                    Category = itemDto.Category.ToString(),
                    SubCategory = itemDto.SubCategory.ToString(),
                    Name = itemDto.Name,
                    Description = itemDto.Description,
                    Url = itemDto.Url,
                    IconUrl = itemDto.IconUrl,
                    Color = itemDto.Color,
                    Gender = itemDto.Gender,
                    Tags = GetTagsFromString(itemDto.Tags)
                };
                itemModels.Add(itemModel);
            }


            return itemModels;
        }

        public static AvatarItemDto GetAvatarItemDto(AvatarItemViewModel itemModel)
        {
            AvatarItemDto itemDto = new AvatarItemDto()
            {
                Id = itemModel.Id,
                Category = (AvatarItemCategory)Enum.Parse(typeof(AvatarItemCategory), itemModel.Category),
                SubCategory = (AvatarItemSubCategory)Enum.Parse(typeof(AvatarItemSubCategory), itemModel.SubCategory),
                Name = itemModel.Name,
                Description = itemModel.Description,
                Url = itemModel.Url,
                IconUrl = itemModel.IconUrl,
                Color = itemModel.Color,
                Gender = itemModel.Gender,
                Tags = GetStringFromTagsList(itemModel.Tags)
            };
            return itemDto;
        }

        public static List<string> GetTagsFromString(string tagsString)
        {
            List<string> tagsList = new List<string>();
            string[] tagsArray = tagsString.Split(',');

            for (int i = 0; i < tagsArray.Length; i++)
            {
                tagsList.Add(tagsArray[i]);
            }

            return tagsList;
        }

        public static string GetStringFromTagsList(List<string> tagsList)
        {
            string tagsString = "";
            for (int i = 0; i < tagsList.Count; i++)
            {
                tagsString += tagsList[i];
                if (i != tagsList.Count - 1)
                {
                    tagsString += ",";
                }
            }
            return tagsString;
        }


        // Avatar

        public static AvatarViewModel GetAvatarModel(AvatarDto avatarDto)
        {
            AvatarOrchestrator avatarOrchestrator = new AvatarOrchestrator();

            AvatarViewModel avatarModel = new AvatarViewModel()
            {
                UserId = avatarDto.UserId,
                IsCreated = avatarDto.IsCreated,
                Background = avatarOrchestrator.GetAvatarItemById(avatarDto.Background),
                SecondaryBackground = avatarOrchestrator.GetAvatarItemById(avatarDto.SecondaryBackground),
                Foreground = avatarOrchestrator.GetAvatarItemById(avatarDto.Foreground),
                Base = avatarOrchestrator.GetAvatarItemById(avatarDto.Base),
                Tattoos = avatarOrchestrator.GetAvatarItemById(avatarDto.Tattoos),
                Marks = avatarOrchestrator.GetAvatarItemById(avatarDto.Marks),
                Eyes = avatarOrchestrator.GetAvatarItemById(avatarDto.Eyes),
                Nose = avatarOrchestrator.GetAvatarItemById(avatarDto.Nose),
                Mouth = avatarOrchestrator.GetAvatarItemById(avatarDto.Mouth),
                Makeup = avatarOrchestrator.GetAvatarItemById(avatarDto.Makeup),
                FacialHair = avatarOrchestrator.GetAvatarItemById(avatarDto.FacialHair),
                EarRings = avatarOrchestrator.GetAvatarItemById(avatarDto.EarRings),
                FacePiercings = avatarOrchestrator.GetAvatarItemById(avatarDto.FacePiercings),
                Necklace = avatarOrchestrator.GetAvatarItemById(avatarDto.Necklace),
                LeftArm = avatarOrchestrator.GetAvatarItemById(avatarDto.LeftArm),
                RightArm = avatarOrchestrator.GetAvatarItemById(avatarDto.RightArm),
                Hair = avatarOrchestrator.GetAvatarItemById(avatarDto.Hair),
                HairAccessory = avatarOrchestrator.GetAvatarItemById(avatarDto.HairAccessory),
                Hat = avatarOrchestrator.GetAvatarItemById(avatarDto.Hat),
                Top = avatarOrchestrator.GetAvatarItemById(avatarDto.Top),
                FullBody = avatarOrchestrator.GetAvatarItemById(avatarDto.FullBody),
                Neck = avatarOrchestrator.GetAvatarItemById(avatarDto.Neck),
                Bottom = avatarOrchestrator.GetAvatarItemById(avatarDto.Bottom),
                Shoes = avatarOrchestrator.GetAvatarItemById(avatarDto.Shoes),
                LeftAccessory = avatarOrchestrator.GetAvatarItemById(avatarDto.LeftAccessory),
                RightAccessory = avatarOrchestrator.GetAvatarItemById(avatarDto.RightAccessory),
                LeftHand = avatarOrchestrator.GetAvatarItemById(avatarDto.LeftHand),
                RightHand = avatarOrchestrator.GetAvatarItemById(avatarDto.RightHand)
            };

            return avatarModel;
        }


    }
}