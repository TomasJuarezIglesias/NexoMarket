using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexoMarket.Entity.Dtos
{
    public class BitacoraEventoDTO
    {
        public DateTime Fecha { get; set; }

        public string Usuario { get; set; }

        public string Rol { get; set; }
        
        public string Evento { get; set; }
    }
}
