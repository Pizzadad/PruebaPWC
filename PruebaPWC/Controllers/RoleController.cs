using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PruebaPWC.Application.Security;

namespace PruebaPWC.Controllers
{
    /// <summary>
    /// API Role
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor default
        /// </summary>
        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Metodo crear role
        /// </summary>
        [Authorize(Roles = "admin")]
        [HttpPost("create")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> Create(RoleCreate.RequestRoleCreate parametros)
        {
            return await _mediator.Send(parametros);
        }

        /// <summary>
        /// Metodo crear ver los roles
        /// </summary>
        [Authorize(Roles = "admin")]
        [HttpGet("getall")]
        [ProducesResponseType(typeof(List<IdentityRole>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<IdentityRole>>> GetAll()
        {
            return await _mediator.Send(new RoleGetAll.Request());
        }

        /// <summary>
        /// Metodo agregar rol a un usuario
        /// </summary>
        [Authorize(Roles = "admin")]
        [HttpPost("addroluser")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> AddRoleUser(AddRoleUser.RequestAddRoleUser parametros)
        {
            return await _mediator.Send(parametros);
        }

        /// <summary>
        /// Metodo ver rol de un usuario
        /// </summary>
        [Authorize(Roles = "admin")]
        [HttpPost("roluser/{username}")]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<string>>> RoleUsuario(string username)
        {
            return await _mediator.Send(new RoleForUser.Request { Username = username });
        }

    }
}
