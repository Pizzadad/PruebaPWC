using System;
using System.Collections.Generic;
using System.Text;

namespace PruebaPWC.Application.Security
{
    public class UsuarioResponse
    {
        public string NombreCompleto { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string RoleUser { get; set; }
    }
}
