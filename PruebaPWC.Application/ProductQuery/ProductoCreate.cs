using MediatR;
using PruebaPWC.Application.ModelDto;
using PruebaPWC.Entities;
using PruebaPWC.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PruebaPWC.Application.ProductQuery
{
    /// <summary>
    /// Clase de la entidad ProductoCreate
    /// </summary>
    public class ProductoCreate
    {
        public class RequestProductoCreate : IRequest<int>
        {
            [Required(ErrorMessage = "Campo Nombre Obligatorio")]
            public string nombreProducto { get; set; }
            [Required(ErrorMessage = "Campo Nombre Obligatorio")]
            public string tipoProducto { get; set; }
            [Required(ErrorMessage = "Campo Nombre Obligatorio")]
            public int cantidadProducto { get; set; }
            
            public byte[] imagenProducto { get; set; }

        }

        public class Handler : IRequestHandler<RequestProductoCreate, int>
        {
            private readonly PruebaBdContext _pruebaBdContext;

            public Handler(PruebaBdContext pruebaBdContext)
            {
                _pruebaBdContext = pruebaBdContext;
            }

            public async Task<int> Handle(RequestProductoCreate request, CancellationToken cancellationToken)
            {
                var producto = new Producto
                {
                    nombreProducto = request.nombreProducto,
                    tipoProducto = request.tipoProducto,
                    cantidadProducto = request.cantidadProducto,
                    imagenProducto = request.imagenProducto,
                    fechaRegistro = DateTime.Now,
                    fechaModificacion = DateTime.Now
                };

                await _pruebaBdContext.Producto.AddAsync(producto, cancellationToken);

                var valor = await _pruebaBdContext.SaveChangesAsync(cancellationToken);

                if (valor > 0)
                {
                    return producto.idProducto;
                }

                throw new Exception("Error en agregar el producto");

            }

        }
    }
}
