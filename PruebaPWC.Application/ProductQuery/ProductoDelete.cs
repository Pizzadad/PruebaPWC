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
    public class ProductoDelete
    {
        public class RequestProductoDelete : IRequest<int>
        {
            [Required(ErrorMessage = "ID Obligatorio")]
            public int idProducto { get; set; }
        }

        public class Handler : IRequestHandler<RequestProductoDelete, int>
        { 
            private readonly PruebaBdContext _pruebaBdContext;
            public Handler(PruebaBdContext pruebaBdContext)
            {
                _pruebaBdContext = pruebaBdContext;
            }
            public async Task<int> Handle(RequestProductoDelete request, CancellationToken cancellationToken)
            {
                var delete = await _pruebaBdContext.Producto.FindAsync(request.idProducto);

                if (delete == null)
                {
                    throw new Exception("No se encuentra el producot");
                }

                _pruebaBdContext.Remove(delete);

                var resultado = await _pruebaBdContext.SaveChangesAsync(cancellationToken);

                if (resultado > 0)
                {
                    return 1;
                }

                throw new Exception("No se pudo eliminar el producto");
            }
        }
    }
}
