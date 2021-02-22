using MediatR;
using PruebaPWC.Application.ModelDto;
using PruebaPWC.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PruebaPWC.Application.ProductQuery
{
    public class ProductoGet
    {
        public class RequestProductoGet : IRequest<ProductoDto>
        {
            [Required(ErrorMessage = "ID Obligatorio")]
            public int idProducto { get; set; }
        }
        public class Handler : IRequestHandler<RequestProductoGet, ProductoDto>
        {
            private readonly PruebaBdContext _pruebaBdContext;
            public Handler(PruebaBdContext pruebaBdContext)
            {
                _pruebaBdContext = pruebaBdContext;
            }
            public async Task<ProductoDto> Handle(RequestProductoGet request, CancellationToken cancellationToken)
            {
                var resultado = await _pruebaBdContext.Producto.FindAsync(request.idProducto);
                var resultadoDto = resultado.MapTo<ProductoDto>();
                return resultadoDto;
            }
        }
    }
}
