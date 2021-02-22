using MediatR;
using Microsoft.AspNetCore.Identity;
using PruebaPWC.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PruebaPWC.Application.Security
{
    public class AddRoleUser
    {
        public class RequestAddRoleUser : IRequest<bool>
        {
            public string Username { get; set; }
            public string RolName { get; set; }
        }

        public class Handler : IRequestHandler<RequestAddRoleUser, bool>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;

            public Handler(UserManager<Usuario> userManager,
                        RoleManager<IdentityRole> roleManager)
            {
                _userManager = userManager;
                _roleManager = roleManager;
            }

            public async Task<bool> Handle(RequestAddRoleUser request, CancellationToken cancellationToken)
            {
                var role = await _roleManager.FindByNameAsync(request.RolName);

                if (role == null)
                {
                    throw new Exception("No existe el role");
                }

                var user = await _userManager.FindByNameAsync(request.Username);
                if (user == null)
                {
                    throw new Exception("No existe el usuario");
                }

                var resultado = await _userManager.AddToRoleAsync(user, request.RolName);
                if (resultado.Succeeded)
                {
                    return true;
                }

                throw new Exception("No se pudo agregar el role al usuario");
            }
        }

    }
}
