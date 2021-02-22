using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PruebaPWC.Application.Contracts;
using PruebaPWC.Entities;
using PruebaPWC.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PruebaPWC.Application.Security
{
    public class RegisterUser
    {
        public class RequestRegisterUser : IRequest<UsuarioResponse>
        {
            [Required(ErrorMessage = "Campo Nombre Obligatorio")]
            public string NombreCompleto { get; set; }
            [Required(ErrorMessage = "Campo Email Obligatorio")]
            public string Email { get; set; }
            [Required(ErrorMessage = "Campo Password Obligatorio")]
            public string Password { get; set; }
            [Required(ErrorMessage = "Campo UserName Obligatorio")]
            public string UserName { get; set; }
            [Required(ErrorMessage = "Campo RoleUser Obligatorio")]
            public string RoleUser { get; set; }
        }

        public class Handler : IRequestHandler<RequestRegisterUser, UsuarioResponse>
        {
            private readonly IJWT _jWT;
            private readonly UserManager<Usuario> _userManager;
            private readonly PruebaBdContext _pruebaBdContext;
            private readonly RoleManager<IdentityRole> _roleManager;

            public Handler(IJWT jWT,
            UserManager<Usuario> userManager,
            PruebaBdContext pruebaBdContext,
            RoleManager<IdentityRole> roleManager
                )
            {
                _jWT = jWT;
                _userManager = userManager;
                _pruebaBdContext = pruebaBdContext;
                _roleManager = roleManager;
            }

            public async Task<UsuarioResponse> Handle(RequestRegisterUser request, CancellationToken cancellationToken)
            {
                var existe = await _pruebaBdContext.Users.Where(x => x.Email == request.Email).AnyAsync();

                if (existe)
                {
                    throw new Exception("Error existe el email");
                }

                var existeUserName = await _pruebaBdContext.Users.Where(x => x.UserName == request.UserName).AnyAsync();
                if (existeUserName)
                {
                    throw new Exception("Error existe el username");
                }

                var existRole = await _roleManager.FindByNameAsync(request.RoleUser);

                if (existRole == null)
                {
                    throw new Exception("No existe el role");
                }

                var usuario = new Usuario
                {
                    NombreCompleto = request.NombreCompleto,
                    Email = request.Email,
                    UserName = request.UserName,
                    RoleUser = request.RoleUser
                };

                var resultado = await _userManager.CreateAsync(usuario, request.Password);

                if (resultado.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(request.UserName);

                    var role = await _userManager.AddToRoleAsync(user, request.RoleUser);

                    return new UsuarioResponse
                    {
                        NombreCompleto = usuario.NombreCompleto,
                        Token = _jWT.CrearToken(usuario, null),
                        Email = usuario.Email,
                        Username = usuario.UserName,
                        RoleUser = usuario.RoleUser
                    };
                }

                throw new Exception("Error registrar");

            }
        }

    }
}
