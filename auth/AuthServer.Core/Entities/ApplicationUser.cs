using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace AuthServer.Core.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
    }
}
