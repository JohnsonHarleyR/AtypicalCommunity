using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atypical.Crosscutting.Enums
{
    // What type of account is it?
    public enum UserType
    {
        Citizen,
        Admin,
        NPC
    }

    // What is the state of their account?
    public enum AccountStatus
    {
        Active,
        Suspended,
        Frozen
    }

}
