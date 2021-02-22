using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PruebaPWC.Application.Security
{
    public class RoleCreate
    {
        public class RequestRoleCreate : IRequest<bool>
        {
            [Required(ErrorMessage ="Nombre Obligatorio")]
            public string NameRole { get; set; }
        }

        public class Handler : IRequestHandler<RequestRoleCreate, bool>
        {
            private readonly RoleManager<IdentityRole> _roleManager;
            public Handler(RoleManager<IdentityRole> roleManager)
            {
                _roleManager = roleManager;
            }

            public async Task<bool> Handle(RequestRoleCreate request, CancellationToken cancellationToken)
            {
                var role = await _roleManager.FindByNameAsync(request.NameRole);

                if (role != null)
                {
                    throw new Exception("Ya se encuentra el role registrado");
                }

                var create = await _roleManager.CreateAsync(new IdentityRole(request.NameRole));

                if (create.Succeeded)
                {
                    return true;
                }

                throw new Exception("No se pudo crear el rol");
            }
        }
    }
}
