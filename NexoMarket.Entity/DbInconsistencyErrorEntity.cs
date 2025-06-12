using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexoMarket.Entity
{
    public class DbInconsistencyErrorEntity
    {
        public string Tabla { get; set; }
        public string Columna { get; set; }
        public string Dato { get; set; }

        public DbInconsistencyErrorEntity(string table, string column, string fact)
        {
            Tabla = table;
            Columna = column;
            Dato = fact;
        }
    }
}
