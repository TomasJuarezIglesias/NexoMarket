using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexoMarket.Data.Dtos
{
    public class RolDto
    {
        public string Nombre { get; set; }
        public List<PermisoDTO> Permisos { get; set; }
    }
}
