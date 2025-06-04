using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexoMarket.Entity.Dtos
{
    public class PermisoEntity
    {
        public string Nombre { get; set; }
        public bool Is_Familia { get; set; }
        public List<PermisoEntity> Permisos { get; set; }
    }
}
