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
    public class RoleForUser
    {
        public class Request : IRequest<List<string>>
        {
            public string Username { get; set; }
        }

        public class Handler : IRequestHandler<Request, List<string>>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            public Handler(UserManager<Usuario> userManager,
                        RoleManager<IdentityRole> roleManager)
            {
                _userManager = userManager;
                _roleManager = roleManager;
            }

            public async Task<List<string>> Handle(Request request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(request.Username);
                if (user == null)
                {
                    throw new Exception("No existe el usuario") ;
                }

                var resultados = await _userManager.GetRolesAsync(user);
                return new List<string>(resultados);
            }
        }
    }
}
