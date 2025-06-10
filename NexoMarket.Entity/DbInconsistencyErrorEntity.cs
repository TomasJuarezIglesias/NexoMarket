using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexoMarket.Entity
{
    public class DbInconsistencyErrorEntity
    {
        public string Table { get; set; }
        public string Column { get; set; }
        public string Fact { get; set; }

        public DbInconsistencyErrorEntity(string table, string column, string fact)
        {
            Table = table;
            Column = column;
            Fact = fact;
        }
    }
}
