using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atypical.Crosscutting.Enums
{
    // user or admin activity category
    public enum UserActivityCategory
    {
        UserAccount,
        Friends,
        Forum,
        Shop,
        Messaging,
        Profile,
        Avatar,
        Bank,
        Diary,
        Games
    }

    public enum AdminActivityCategory
    {
        Executive, // these are things like adding entire forum categories, deleting users, all the most risky stuff
        UserAccount,
        Forum
    }

}
