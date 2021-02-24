using System;
using System.Collections.Generic;
using System.Text;

namespace PruebaPWC.Application.ModelDto
{
    /// <summary>
    /// Clase de la entidad ProductoDTO
    /// </summary>
    public class ProductoDto
    {
        public int idProducto { get; set; }
        public string nombreProducto { get; set; }
        public string tipoProducto { get; set; }
        public int cantidadProducto { get; set; }
        public byte[] imagenProducto { get; set; }
        public DateTime fechaRegistro { get; set; }
        public DateTime fechaModificacion { get; set; }
    }
}
