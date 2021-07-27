using Atypical.Crosscutting.Dtos.Avatar;

namespace Atypical.Models.Avatar
{
    public class AvatarViewModel
    {

        public int UserId { get; set; }

        // Backdrop
        public AvatarItemDto Background { get; set; } // 0
        public AvatarItemDto SecondaryBackground { get; set; }
        public AvatarItemDto Foreground { get; set; }

        // Skin
        public AvatarItemDto Base { get; set; } // 1
        public AvatarItemDto Tattoos { get; set; }
        public AvatarItemDto Marks { get; set; }

        // Face
        public AvatarItemDto Eyes { get; set; }
        public AvatarItemDto Nose { get; set; }
        public AvatarItemDto Mouth { get; set; }
        public AvatarItemDto Makeup { get; set; }
        public AvatarItemDto FacialHair { get; set; }

        // Jewelry
        public AvatarItemDto EarRings { get; set; }
        public AvatarItemDto FacePiercings { get; set; }
        public AvatarItemDto Necklace { get; set; }
        public AvatarItemDto LeftArm { get; set; }
        public AvatarItemDto RightArm { get; set; }

        // Head
        public AvatarItemDto Hair { get; set; }
        public AvatarItemDto HairAccessory { get; set; }
        public AvatarItemDto Hat { get; set; }

        // Torso
        public AvatarItemDto Top { get; set; }
        public AvatarItemDto FullBody { get; set; }
        public AvatarItemDto Neck { get; set; }

        // Lower
        public AvatarItemDto Bottom { get; set; }
        public AvatarItemDto Shoes { get; set; }

        // Accessories
        public AvatarItemDto LeftAccessory { get; set; }
        public AvatarItemDto RightAccessory { get; set; }

        // Hands
        public AvatarItemDto LeftHand { get; set; }
        public AvatarItemDto RightHand { get; set; }
    }
}