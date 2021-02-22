using MediatR;
using Microsoft.AspNetCore.Identity;
using PruebaPWC.Application.Contracts;
using PruebaPWC.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PruebaPWC.Application.Security
{
    public class Logger
    {
        public class RequestLogger : IRequest<UsuarioResponse>
        {
            [Required(ErrorMessage = "Ingrese Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Ingrese Password")]
            public string Password { get; set; }
        }

        public class Handler : IRequestHandler<RequestLogger, UsuarioResponse>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly SignInManager<Usuario> _signInManager;
            private readonly IJWT _jWT;

            public Handler(
                UserManager<Usuario> userManager,
                SignInManager<Usuario> signInManager,
                IJWT jWT)
            {
                _jWT = jWT;
                _signInManager = signInManager;
                _userManager = userManager;
            }

            
            public async Task<UsuarioResponse> Handle(RequestLogger request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    throw new Exception("No permisos");
                }

                var resultado = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                var responseRoles = await _userManager.GetRolesAsync(user);

                var lstRoles = responseRoles.MapTo<List<string>>();

                if (resultado.Succeeded)
                {
                    return new UsuarioResponse
                    {
                        NombreCompleto = user.NombreCompleto,
                        Token = _jWT.CrearToken(user, lstRoles),
                        Username = user.UserName,
                        Email = user.Email,
                        RoleUser = user.RoleUser
                    };
                }

                throw new Exception("No se logro el loggin");
            }
        }
    }
}
