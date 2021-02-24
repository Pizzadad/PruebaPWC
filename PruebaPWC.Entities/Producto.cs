using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PruebaPWC.Entities
{
    /// <summary>
    /// Clase de la entidad Producto
    /// </summary>
    public class Producto
    {
        [Key]
        public int idProducto { get; set; }
        public string nombreProducto { get; set; }
        public string tipoProducto { get; set; }
        public int cantidadProducto { get; set; }
        public byte[] imagenProducto { get; set; }
        public DateTime fechaRegistro { get; set; }
        public DateTime fechaModificacion { get; set; }
    }
}
