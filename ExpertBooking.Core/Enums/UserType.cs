using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertBooking.Core.Enums
{
    public enum UserType
    {
        ApplicationUser = 0, // Default until profile completed
        Client = 1,
        Expert = 2
    }
}
