using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PruebaPWC.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PruebaPWC.Application.Security
{
    public class RoleGetAll
    {
        public class Request : IRequest<List<IdentityRole>>
        {

        }

        public class Handler : IRequestHandler<Request, List<IdentityRole>>
        {
            private readonly PruebaBdContext _pruebaBdContext;

            public Handler(PruebaBdContext pruebaBdContext)
            {
                _pruebaBdContext = pruebaBdContext;
            }

            public async Task<List<IdentityRole>> Handle(Request request, CancellationToken cancellationToken)
            {
                var roles = await _pruebaBdContext.Roles.ToListAsync();
                return roles;
            }
        }

    }
}
