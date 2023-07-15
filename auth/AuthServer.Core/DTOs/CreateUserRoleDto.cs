using System;
using System.Collections.Generic;

namespace AuthServer.Core.DTOs
{
    public class CreateUserRoleDto
    {
        public List<Guid> RoleIds { get; set; }
    }
}
