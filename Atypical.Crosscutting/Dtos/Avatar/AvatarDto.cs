using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atypical.Crosscutting.Dtos.Avatar
{
    public class AvatarDto
    {

        // This will store item ids that refer to the database

        public int UserId { get; set; } = 1; // HACK change once connected to users - make foreign key

        // Is default avatar created?
        public bool IsCreated { get; set; }

        // Backdrop
        public int? Background { get; set; } // 0
        public int? SecondaryBackground { get; set; }
        public int? Foreground { get; set; }

        // Skin
        public int? Base { get; set; } // 1
        public int? Tattoos { get; set; }
        public int? Marks { get; set; }

        // Face
        public int? Eyes { get; set; }
        public int? Nose { get; set; }
        public int? Mouth { get; set; }
        public int? Makeup { get; set; }
        public int? FacialHair { get; set; }

        // Jewelry
        public int? EarRings { get; set; }
        public int? FacePiercings { get; set; }
        public int? Necklace { get; set; }
        public int? LeftArm { get; set; }
        public int? RightArm { get; set; }

        // Head
        public int? Hair { get; set; }
        public int? HairAccessory { get; set; }
        public int? Hat { get; set; }

        // Torso
        public int? Top { get; set; }
        public int? FullBody { get; set; }
        public int? Neck { get; set; }

        // Lower
        public int? Bottom { get; set; }
        public int? Shoes { get; set; }

        // Accessories
        public int? LeftAccessory { get; set; }
        public int? RightAccessory { get; set; }

        // Hands
        public int? LeftHand { get; set; }
        public int? RightHand { get; set; }

    }
}
