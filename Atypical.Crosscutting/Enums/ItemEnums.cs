using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atypical.Crosscutting.Enums
{
    public enum ItemType
    {
        Avatar,
        Food
    }

    public enum AvatarItemCategory
    {
        Backdrop,
        Skin,
        Face,
        Jewelry,
        Head,
        Torso,
        Lower,
        Accessories,
        Hands
    }

    public enum AvatarItemSubCategory
    {
        Background, // Backdrop
        [Display(Name = "Secondary Background")]
        SecondaryBackground, // Backdrop
        Foreground, // Backdrop

        Base, // Skin
        Tattoos, // Skin
        Marks, // Skin

        Eyes, // Face
        Nose, // Face
        Mouth, // Face
        Makeup, // Face

        [Display(Name = "Ear Rings")]
        EarRings, // Jewelry
        [Display(Name = "Face Piercings")]
        FacePiercing, // Jewelry
        Necklace, // Jewelry
        [Display(Name = "Left Arm")]
        LeftArm, // Jewelry
        [Display(Name = "Right Arm")]
        RightArm, // Jewelry

        Hair, // Head
        [Display(Name = "Hair Accessory")]
        HairAccessory, // Head
        Hat, // Head

        Top, // Torso
        [Display(Name = "Full Body")]
        FullBody, // Torso
        Neck, // Torso

        Bottom, // Lower
        Shoes, // Lower

        [Display(Name = "Left Accessory")]
        LeftAccessory, // Accessories
        [Display(Name = "Right Accessory")]
        RightAccessory, // Accessories

        [Display(Name = "Left Hand")]
        LeftHand, // Hands
        [Display(Name = "Right Hand")]
        RightHand // Hands

    }

    public enum ItemColor
    {
        Black,
        White,
        Grey,
        Brown,
        Red,
        Orange,
        Yellow,
        Green,
        Blue,
        Purple,
        None
    }

    public enum AvatarGender
    {
        Female,
        Male,
        Unisex
    }
}
