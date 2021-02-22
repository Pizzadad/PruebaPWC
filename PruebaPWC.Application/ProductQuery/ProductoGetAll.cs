using MediatR;
using Microsoft.EntityFrameworkCore;
using PruebaPWC.Application.ModelDto;
using PruebaPWC.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PruebaPWC.Application.ProductQuery
{
    /// <summary>
    /// Clase de la entidad ProductoGetAll
    /// </summary>
    public class ProductoGetAll
    {
        public class RequestProductoGetAll : IRequest<List<ProductoDto>>
        {

        }

        public class Handler : IRequestHandler<RequestProductoGetAll, List<ProductoDto>>
        {
            private readonly PruebaBdContext _pruebaBdContext;
            public Handler(PruebaBdContext pruebaBdContext)
            {
                _pruebaBdContext = pruebaBdContext;
            }

            public async Task<List<ProductoDto>> Handle(RequestProductoGetAll request, CancellationToken cancellationToken)
            {
                var resultado = await _pruebaBdContext.Producto.ToListAsync();

                var resultadoDto = resultado.MapTo<List<ProductoDto>>();

                return resultadoDto;
            }
        }
    }
}
