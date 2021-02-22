using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace PruebaPWC.Application
{
    /// <summary>
    /// Clase donde se genera la funcion MapTo
    /// </summary>
    public static class DtoMapperExtension
    {
        public static T MapTo<T>(this Object value)
        {
            return JsonSerializer.Deserialize<T>(
                JsonSerializer.Serialize(value));
        }
    }
}
