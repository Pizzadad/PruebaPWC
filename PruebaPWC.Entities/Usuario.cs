using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PruebaPWC.Entities
{
    public class Usuario : IdentityUser
    {
        public string NombreCompleto { get; set; }
        public string RoleUser { get; set; }
    }
}
