using Atypical.Crosscutting.Dtos.Avatar;
using Atypical.Data.Repositories.Avatar;
using System.Collections.Generic;

namespace Atypical.Domain.Orchestrators.Avatar
{
    public class AvatarOrchestrator
    {
        private AvatarRepository avatarRepository = new AvatarRepository();

        // grab avatar items

        public AvatarItemDto GetAvatarItemById(int? id)
        {
            if (id == null)
            {
                return null;
            }

            AvatarItemDto itemDto = avatarRepository.GetAvatarItemById((int)id);

            if (itemDto == null)
            {
                return null;
            }

            return itemDto;

        }

        public bool AddAvatarItem(AvatarItemDto itemDto)
        {
            if (itemDto == null)
            {
                return false;
            }

            avatarRepository.AddAvatarItem(itemDto);

            return true;
        }

        public IEnumerable<AvatarItemDto> GetAllAvatarItems()
        {
            IEnumerable<AvatarItemDto> items = avatarRepository.GetAllAvatarItems();

            if (items == null)
            {
                return null;
            }

            return items;

        }


        // Avatars

        public AvatarDto GetAvatarById(int userId)
        {
            AvatarDto userDto = avatarRepository.GetAvatarById(userId);

            if (userDto == null)
            {
                return null;
            }

            return userDto;

        }

        public bool AddAvatar(AvatarDto avatarDto)
        {
            if (avatarDto == null)
            {
                return false;
            }

            avatarRepository.AddAvatar(avatarDto);

            return true;
        }


    }
}
