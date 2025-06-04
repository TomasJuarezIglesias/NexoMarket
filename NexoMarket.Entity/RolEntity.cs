using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexoMarket.Entity
{
    public class RolEntity
    {
        public string Nombre { get; set; }
        public List<PermisoEntity> Permisos { get; set; }
    }
}
