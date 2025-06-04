using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexoMarket.Entity
{
    public class BitacoraEventoCreateEntity
    {
        public int Id_user { get; set; }
        public string Evento { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
