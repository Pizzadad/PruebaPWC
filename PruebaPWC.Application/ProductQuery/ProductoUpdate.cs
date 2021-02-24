using MediatR;
using PruebaPWC.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PruebaPWC.Application.ProductQuery
{
    public class ProductoUpdate
    {
        public class RequestProductoUpdate : IRequest<int>
        {
            [Required(ErrorMessage = "ID Obligatorio")]
            public int idProducto { get; set; }
            [Required(ErrorMessage = "Campo Nombre Obligatorio")]
            public string nombreProducto { get; set; }
            [Required(ErrorMessage = "Campo Nombre Obligatorio")]
            public string tipoProducto { get; set; }
            [Required(ErrorMessage = "Campo Nombre Obligatorio")]
            public int? cantidadProducto { get; set; }
            public byte[] imagenProducto { get; set; }
        }

        public class Handler : IRequestHandler<RequestProductoUpdate, int>
        {
            private readonly PruebaBdContext _pruebaBdContext;
            public Handler(PruebaBdContext pruebaBdContext)
            {
                _pruebaBdContext = pruebaBdContext;
            }
            public async Task<int> Handle(RequestProductoUpdate request, CancellationToken cancellationToken)
            {
                var productoID = await _pruebaBdContext.Producto.FindAsync(request.idProducto);

                if (productoID == null)
                {
                    throw new Exception("No existe el curso");
                }

                productoID.nombreProducto = request.nombreProducto ?? productoID.nombreProducto;
                productoID.tipoProducto = request.tipoProducto ?? productoID.tipoProducto;
                productoID.cantidadProducto = request.cantidadProducto ?? productoID.cantidadProducto;
                productoID.imagenProducto = request.imagenProducto ?? productoID.imagenProducto;
                productoID.fechaRegistro = productoID.fechaRegistro;
                productoID.fechaModificacion = DateTime.Now;

                var save = await _pruebaBdContext.SaveChangesAsync(cancellationToken);

                if (save > 0)
                {
                    return productoID.idProducto;
                }

                throw new Exception("No se pudo actualizar el producto");

            }
        }

    }
}
