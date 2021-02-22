using PruebaPWC.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PruebaPWC.Application.Contracts
{
    public interface IJWT
    {
        string CrearToken(Usuario usuario, List<string> roles);
    }
}
