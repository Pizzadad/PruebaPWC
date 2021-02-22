using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaPWC.Application.ModelDto;
using PruebaPWC.Application.ProductQuery;

namespace PruebaPWC.Controllers.v1
{
    /// <summary>
    /// API Producto
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductoController : ControllerBase
    {
        private readonly IMediator _mediator;
        /// <summary>
        /// Constructor default
        /// </summary>
        public ProductoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Metodo obtener para Listar Productos
        /// </summary>
        [Authorize(Roles = "comprador, admin")]
        [HttpGet("getall")]
        [ProducesResponseType(typeof(List<ProductoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<ProductoDto>>> Get()
        {
            return await _mediator.Send(new ProductoGetAll.RequestProductoGetAll());
        }

        /// <summary>
        /// Metodo crear producto
        /// </summary>
        [Authorize(Roles = "admin")]
        [HttpPost("create")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create(ProductoCreate.RequestProductoCreate data)
        {
            try
            {
                var id = await _mediator.Send(data);
                return Ok(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo eliminar producto
        /// </summary>
        [Authorize(Roles = "admin")]
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var delete = await _mediator.Send(new ProductoDelete.RequestProductoDelete { idProducto = id });
                return Ok(delete);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo actualizar producto
        /// </summary>
        [Authorize(Roles = "admin")]
        [HttpPut("update/{id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(int id, ProductoUpdate.RequestProductoUpdate data)
        {
            try
            {
                data.idProducto = id;
                var update = await _mediator.Send(data);
                return Ok(update);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo buscar solo producto
        /// </summary>
        [Authorize(Roles = "comprador, admin")]
        [HttpGet("get/{id}")]
        [ProducesResponseType(typeof(ProductoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetId(int id)
        {
            try
            {
                
                var get = await _mediator.Send(new ProductoGet.RequestProductoGet {idProducto = id } );
                return Ok(get);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

    }
}
