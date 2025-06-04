using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NexoMarket.Entity.Entities
{   
    public class BusinessResponse<T> where T :class
    {
        public BusinessResponse(T data = null, string mensaje = "", bool ok = false)
        {
            Data = data;
            Mensaje = mensaje;
            Ok  = ok;
        }
        public T Data { get; set; }
        public string Mensaje { get; set; }
        public bool Ok { get; set; } 
    }
}
