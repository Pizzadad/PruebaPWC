using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaPWC.Application.Security;

namespace PruebaPWC.Controllers
{
    /// <summary>
    /// API Login
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    
    //[AllowAnonymous]
    public class UserController : ControllerBase
    {

        private readonly IMediator _mediator;
        /// <summary>
        /// Constructor default
        /// </summary>
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Metodo obtener el Loggin
        /// </summary>
        [HttpPost("login")]
        [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UsuarioResponse>> Login(Logger.RequestLogger parametros)
        {
            return await _mediator.Send(parametros);
        }

        /// <summary>
        /// Metodo obtener el registro
        /// </summary>
        [HttpPost("register")]
        [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UsuarioResponse>> Registrar(RegisterUser.RequestRegisterUser parametros)
        {
            return await _mediator.Send(parametros);
        }

    }
}
