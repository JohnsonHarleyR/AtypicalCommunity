using Atypical.Crosscutting.Dtos.Avatar;
using Atypical.Crosscutting.Dtos.Inventory;
using Atypical.Crosscutting.Dtos.User;
using Atypical.Crosscutting.Enums;
using Atypical.Crosscutting.Interfaces;
using Atypical.Domain.Orchestrators.Avatar;
using Atypical.Domain.Orchestrators.Inventory;
using Atypical.Domain.Orchestrators.User;
using Atypical.Helpers;
using Atypical.Models.Avatar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Atypical.Controllers
{
    public class AvatarController : Controller
    {
        // GET: Avatar
        private AvatarOrchestrator avatarOrchestrator = new AvatarOrchestrator();
        private UserOrchestrator userOrchestrator = new UserOrchestrator();
        private InventoryOrchestrator inventoryOrchestrator = new InventoryOrchestrator();

        // Give the user all the default items, then redirect to the Create Page
        public ActionResult CreateFirstAvatar()
        {
            //first check if the session has a user - if it does not, go to home page
            if (Session["username"] == null || Session["userId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // Get all avatar items that have the word default (or test - for now)
            List<AvatarItemDto> itemDtos =
                avatarOrchestrator.GetAllAvatarItems()
                .Where(a => a.Tags.ToLower().Contains("test") ||
                a.Tags.ToLower().Contains("default")).ToList();

            // Grab the user
            UserDto user = userOrchestrator
                .GetUserById(Int32.Parse(Session["userId"].ToString()));

            // try to grab the avatar - if there's no avatar, create one
            AvatarDto avatar = avatarOrchestrator.GetAvatarById(user.Id);

            if (avatar != null && avatar.IsCreated == true)
            {
                return RedirectToAction("Create", "Avatar");
            } else if (avatar == null) // if it's not null but attributes have not been filled
            {
                // create an empty avatar for the user
                avatar = new AvatarDto()
                {
                    UserId = user.Id,
                    IsCreated = false
                };

                // Give the avatar base, eyes, nose, and mouth default values if they are null
                if (avatar.Base == null)
                {
                    var tempBase = itemDtos.Where(i => i.SubCategory.Equals(AvatarItemSubCategory.Base))
                        .ToList().FirstOrDefault();
                    if (tempBase != null)
                    {
                        avatar.Base = tempBase.Id;
                    } 
                }
                if (avatar.Eyes == null)
                {
                    var tempEyes = itemDtos.Where(i => i.SubCategory.Equals(AvatarItemSubCategory.Eyes))
                        .ToList().FirstOrDefault();
                    if (tempEyes != null)
                    {
                        avatar.Eyes = tempEyes.Id;
                    }
                    
                }
                if (avatar.Nose == null)
                {
                    var tempNose = itemDtos.Where(i => i.SubCategory.Equals(AvatarItemSubCategory.Nose))
                        .ToList().FirstOrDefault();
                    if (tempNose != null)
                    {
                        avatar.Nose = tempNose.Id;
                    }
                    
                }
                if (avatar.Mouth == null)
                {
                    var tempMouth = itemDtos.Where(i => i.SubCategory.Equals(AvatarItemSubCategory.Mouth))
                        .ToList().FirstOrDefault();
                    if (tempMouth != null)
                    {
                        avatar.Mouth = tempMouth.Id;
                    }
                    
                }
                if (avatar.Hair == null)
                {
                    var tempHair = itemDtos.Where(i => i.SubCategory.Equals(AvatarItemSubCategory.Hair))
                        .ToList().FirstOrDefault();
                    if (tempHair != null)
                    {
                        avatar.Hair = tempHair.Id;
                    }
                }

                // add avatar to the repo
                avatarOrchestrator.AddAvatar(avatar);
            }

            // Go to Create and add the itemDtos
            return RedirectToAction("Create", "Avatar", itemDtos);

        }



        // Takes in a list of avatar items but does not require one.
        public ActionResult Create(List<AvatarItemDto> itemDtos)
        {

            //first check if the session has a user - if it does not, go to home page
            if (Session["username"] == null || Session["userId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // Grab the user
            UserDto user = userOrchestrator
                .GetUserById(Int32.Parse(Session["userId"].ToString()));

            // Get avatar dto for creator - turn it into a model
            AvatarDto avatar = avatarOrchestrator.GetAvatarById(user.Id);

            // if the avatar dto is null, redirect so that one can be created
            if (avatar == null)
            {
                return RedirectToAction("CreateFirstAvatar", "Avatar");
            }

            // Create list of items
            List<AvatarItemViewModel> items;

            // If itemDtos isn't null, use that - otherwise grab user's items
            if (itemDtos != null)
            {
                items = AvatarHelper.GetAvatarItemModelList(itemDtos);
                // TODO see if user avatar has default base assigned - if not, assign one
            }
            else // otherwise grab the user's avatars
            { // TODO add inventory ability
                List<InventoryItemDto> inventoryItems =
                    inventoryOrchestrator.GetInventoryItemsByType(user.Id, ItemType.Avatar).ToList();
                items = AvatarHelper
                    .GetAvatarItemModelList(AvatarHelper.GetAvatarItemsFromInventoryList(inventoryItems));
            }

            //// TEST - add item to avatar - TODO change avatar items to models instead of dtos?
            //avatar.Background = AvatarHelper.GetAvatarItemDto(items[1]);

            // create model for the maker
            AvatarMakerViewModel model = new AvatarMakerViewModel()
            {
                Avatar = AvatarHelper.GetAvatarModel(avatar), // TODO add method to change avatar into model
                AvatarItems = items
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(AvatarMakerViewModel model)
        {
            if (ModelState.IsValid)
            {
                // turn the avatar model into a dto
                AvatarDto avatar = AvatarHelper.GetAvatarDto(model.Avatar);

                // if this is the first time creating, add the avatar items to the user inventory
                if (avatar.IsCreated == false)
                {
                    // set it to true
                    avatar.IsCreated = true;

                    // add items to inventory
                    List<IItem> parts = new List<IItem>();
                    parts.Add(model.Avatar.Background);
                    parts.Add(model.Avatar.SecondaryBackground);
                    parts.Add(model.Avatar.Foreground);
                    parts.Add(model.Avatar.Base);
                    parts.Add(model.Avatar.Tattoos);
                    parts.Add(model.Avatar.Marks);
                    parts.Add(model.Avatar.Eyes);
                    parts.Add(model.Avatar.Nose);
                    parts.Add(model.Avatar.Mouth);
                    parts.Add(model.Avatar.Makeup);
                    parts.Add(model.Avatar.FacialHair);
                    parts.Add(model.Avatar.EarRings);
                    parts.Add(model.Avatar.FacePiercings);
                    parts.Add(model.Avatar.Necklace);
                    parts.Add(model.Avatar.LeftArm);
                    parts.Add(model.Avatar.RightArm);
                    parts.Add(model.Avatar.Hair);
                    parts.Add(model.Avatar.HairAccessory);
                    parts.Add(model.Avatar.Hat);
                    parts.Add(model.Avatar.Top);
                    parts.Add(model.Avatar.FullBody);
                    parts.Add(model.Avatar.Neck);
                    parts.Add(model.Avatar.Bottom);
                    parts.Add(model.Avatar.Shoes);
                    parts.Add(model.Avatar.LeftAccessory);
                    parts.Add(model.Avatar.RightAccessory);
                    parts.Add(model.Avatar.LeftHand);
                    parts.Add(model.Avatar.RightHand);

                    InventoryHelper.AddItemsToInventory(parts, avatar.UserId);

                }

                // update the avatar
                avatarOrchestrator.UpdateAvatar(avatar);

                // TODO update the user profile pic

            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult AddDefaults()
        {
            List<AvatarItemDto> newItems = new List<AvatarItemDto>();

            // Backgrounds
            newItems.Add(new AvatarItemDto()
            {
                Category = AvatarItemCategory.Backdrop,
                SubCategory = AvatarItemSubCategory.Background,
                Name = "Green Test Background",
                Description = "You gotta start somewhere, right?",
                Url = "/Images/Avatar/Items/Backdrop/Background/background_test1.png",
                IconUrl = "/Images/Avatar/Items/Backdrop/Background/Icons/icon_background_test1.png",
                Color = ItemColor.Green,
                Gender = AvatarGender.Unisex,
                Tags = "Test,Starter,Plain"
            });
            newItems.Add(new AvatarItemDto()
            {
                Category = AvatarItemCategory.Backdrop,
                SubCategory = AvatarItemSubCategory.Background,
                Name = "Pink Test Background",
                Description = "You gotta start somewhere, right?",
                Url = "/Images/Avatar/Items/Backdrop/Background/background_test2.png",
                IconUrl = "/Images/Avatar/Items/Backdrop/Background/Icons/icon_background_test2.png",
                Color = ItemColor.Red,
                Gender = AvatarGender.Unisex,
                Tags = "Test,Starter,Plain"
            });
            newItems.Add(new AvatarItemDto()
            {
                Category = AvatarItemCategory.Backdrop,
                SubCategory = AvatarItemSubCategory.Background,
                Name = "Yellow Test Background",
                Description = "You gotta start somewhere, right?",
                Url = "/Images/Avatar/Items/Backdrop/Background/background_test3.png",
                IconUrl = "/Images/Avatar/Items/Backdrop/Background/Icons/icon_background_test3.png",
                Color = ItemColor.Red,
                Gender = AvatarGender.Unisex,
                Tags = "Test,Starter,Plain"
            });

            // Secondary Background
            newItems.Add(new AvatarItemDto()
            {
                Category = AvatarItemCategory.Backdrop,
                SubCategory = AvatarItemSubCategory.SecondaryBackground,
                Name = "Polka Dot Test Secondary BG",
                Description = "You gotta start somewhere, right?",
                Url = "/Images/Avatar/Items/Backdrop/SecondaryBackground/secondarybackground_test1.png",
                IconUrl = "/Images/Avatar/Items/Backdrop/SecondaryBackground/Icons/icon_secondarybackground_test1.png",
                Color = ItemColor.Orange,
                Gender = AvatarGender.Unisex,
                Tags = "Test,Starter,Polka Dots"
            });
            newItems.Add(new AvatarItemDto()
            {
                Category = AvatarItemCategory.Backdrop,
                SubCategory = AvatarItemSubCategory.SecondaryBackground,
                Name = "Stars Test Secondary BG",
                Description = "You gotta start somewhere, right?",
                Url = "/Images/Avatar/Items/Backdrop/SecondaryBackground/secondarybackground_test2.png",
                IconUrl = "/Images/Avatar/Items/Backdrop/SecondaryBackground/Icons/icon_secondarybackground_test2.png",
                Color = ItemColor.Green,
                Gender = AvatarGender.Unisex,
                Tags = "Test,Starter,Stars"
            });

            // Base
            newItems.Add(new AvatarItemDto()
            {
                Category = AvatarItemCategory.Skin,
                SubCategory = AvatarItemSubCategory.Base,
                Name = "Light Skin Test",
                Description = "You gotta start somewhere, right?",
                Url = "/Images/Avatar/Items/Skin/Base/base_test1.png",
                IconUrl = "/Images/Avatar/Items/Skin/Base/Icons/icon_base_test1.png",
                Color = ItemColor.None,
                Gender = AvatarGender.Unisex,
                Tags = "Test,Starter,Skin,Base"
            });

            newItems.Add(new AvatarItemDto()
            {
                Category = AvatarItemCategory.Skin,
                SubCategory = AvatarItemSubCategory.Base,
                Name = "Medium Skin Test",
                Description = "You gotta start somewhere, right?",
                Url = "/Images/Avatar/Items/Skin/Base/base_test2.png",
                IconUrl = "/Images/Avatar/Items/Skin/Base/Icons/icon_base_test2.png",
                Color = ItemColor.None,
                Gender = AvatarGender.Unisex,
                Tags = "Test,Starter,Skin,Base"
            });
            newItems.Add(new AvatarItemDto()
            {
                Category = AvatarItemCategory.Skin,
                SubCategory = AvatarItemSubCategory.Base,
                Name = "Dark Skin Test",
                Description = "You gotta start somewhere, right?",
                Url = "/Images/Avatar/Items/Skin/Base/base_test3.png",
                IconUrl = "/Images/Avatar/Items/Skin/Base/Icons/icon_base_test3.png",
                Color = ItemColor.None,
                Gender = AvatarGender.Unisex,
                Tags = "Test,Starter,Skin,Base"
            });

            // Eyes
            newItems.Add(new AvatarItemDto()
            {
                Category = AvatarItemCategory.Face,
                SubCategory = AvatarItemSubCategory.Eyes,
                Name = "Blue Test Eyes",
                Description = "You gotta start somewhere, right?",
                Url = "/Images/Avatar/Items/Face/Eyes/eyes_test1.png",
                IconUrl = "/Images/Avatar/Items/Face/Eyes/Icons/icon_eyes_test1.png",
                Color = ItemColor.Blue,
                Gender = AvatarGender.Unisex,
                Tags = "Test,Starter,Eyes,Dreamy"
            });
            newItems.Add(new AvatarItemDto()
            {
                Category = AvatarItemCategory.Face,
                SubCategory = AvatarItemSubCategory.Eyes,
                Name = "Brown Test Eyes",
                Description = "You gotta start somewhere, right?",
                Url = "/Images/Avatar/Items/Face/Eyes/eyes_test2.png",
                IconUrl = "/Images/Avatar/Items/Face/Eyes/Icons/icon_eyes_test2.png",
                Color = ItemColor.Brown,
                Gender = AvatarGender.Unisex,
                Tags = "Test,Starter,Eyes,Dreamy"
            });

            // Nose
            newItems.Add(new AvatarItemDto()
            {
                Category = AvatarItemCategory.Face,
                SubCategory = AvatarItemSubCategory.Nose,
                Name = "Cute Test Nose",
                Description = "You gotta start somewhere, right?",
                Url = "/Images/Avatar/Items/Face/Nose/nose_test1.png",
                IconUrl = "/Images/Avatar/Items/Face/Nose/Icons/icon_nose_test1.png",
                Color = ItemColor.None,
                Gender = AvatarGender.Unisex,
                Tags = "Test,Starter,Nose,Cute"
            });
            newItems.Add(new AvatarItemDto()
            {
                Category = AvatarItemCategory.Face,
                SubCategory = AvatarItemSubCategory.Nose,
                Name = "Large Test Nose",
                Description = "You gotta start somewhere, right?",
                Url = "/Images/Avatar/Items/Face/Nose/nose_test2.png",
                IconUrl = "/Images/Avatar/Items/Face/Nose/Icons/icon_nose_test2.png",
                Color = ItemColor.None,
                Gender = AvatarGender.Unisex,
                Tags = "Test,Starter,Nose,Large"
            });
            newItems.Add(new AvatarItemDto()
            {
                Category = AvatarItemCategory.Face,
                SubCategory = AvatarItemSubCategory.Nose,
                Name = "Pig Test Nose",
                Description = "You gotta start somewhere, right?",
                Url = "/Images/Avatar/Items/Face/Nose/nose_test3.png",
                IconUrl = "/Images/Avatar/Items/Face/Nose/Icons/icon_nose_test3.png",
                Color = ItemColor.None,
                Gender = AvatarGender.Unisex,
                Tags = "Test,Starter,Nose,Pig"
            });

            // Mouth
            newItems.Add(new AvatarItemDto()
            {
                Category = AvatarItemCategory.Face,
                SubCategory = AvatarItemSubCategory.Mouth,
                Name = "Cute Test Mouth",
                Description = "You gotta start somewhere, right?",
                Url = "/Images/Avatar/Items/Face/Mouth/mouth_test1.png",
                IconUrl = "/Images/Avatar/Items/Face/Mouth/Icons/icon_mouth_test1.png",
                Color = ItemColor.None,
                Gender = AvatarGender.Unisex,
                Tags = "Test,Starter,Mouth,Cute"
            });
            newItems.Add(new AvatarItemDto()
            {
                Category = AvatarItemCategory.Face,
                SubCategory = AvatarItemSubCategory.Mouth,
                Name = "Lipstick Test",
                Description = "You gotta start somewhere, right?",
                Url = "/Images/Avatar/Items/Face/Mouth/mouth_test2.png",
                IconUrl = "/Images/Avatar/Items/Face/Mouth/Icons/icon_mouth_test2.png",
                Color = ItemColor.Red,
                Gender = AvatarGender.Female, // I might make lipstick genderless - but this is for testing
                Tags = "Test,Starter,Mouth,Lipstick"
            });
            newItems.Add(new AvatarItemDto()
            {
                Category = AvatarItemCategory.Face,
                SubCategory = AvatarItemSubCategory.Mouth,
                Name = "Silly Test Mouth",
                Description = "You gotta start somewhere, right?",
                Url = "/Images/Avatar/Items/Face/Mouth/mouth_test3.png",
                IconUrl = "/Images/Avatar/Items/Face/Mouth/Icons/icon_mouth_test3.png",
                Color = ItemColor.None,
                Gender = AvatarGender.Unisex,
                Tags = "Test,Starter,Mouth,Silly"
            });

            // loop through items to add to database - don't add any repeats (check URL)
            IEnumerable<string> existingUrls = avatarOrchestrator.GetAllAvatarItems()
                .Select(i => i.Url).ToList();
            foreach (var newItem in newItems)
            {
                if (existingUrls == null || existingUrls.Count() == 0 ||
                    !existingUrls.Contains(newItem.Url))
                {
                    avatarOrchestrator.AddAvatarItem(newItem);
                }
            }

            return RedirectToAction("Index", "Home");

        }


    }
}