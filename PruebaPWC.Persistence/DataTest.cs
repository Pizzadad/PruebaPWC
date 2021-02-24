using Microsoft.AspNetCore.Identity;
using PruebaPWC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaPWC.Persistence
{
    public class DataTest
    {
        public static async Task Insertar(PruebaBdContext context, UserManager<Usuario> userManager)
        {
            if (!userManager.Users.Any())
            {
                var usuario = new Usuario
                {
                    NombreCompleto = "Christian Romleo",
                    UserName = "ChristianRomleo",
                    Email = "christian_romleo@hotmail.com",
                    RoleUser = "admin"
                   
                };
                await userManager.CreateAsync(usuario, "christian123");
            }
        }
    }
}
