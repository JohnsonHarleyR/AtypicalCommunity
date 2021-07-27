using System.Collections.Generic;

namespace Atypical.Models.Avatar
{
    public class AvatarMakerViewModel
    {
        public AvatarViewModel Avatar { get; set; }
        public List<AvatarItemViewModel> AvatarItems { get; set; }
    }
}