using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexoMarket.Entity
{
    public class UserDvhEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Id_Rol { get; set; }
        public bool Is_Blocked { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DVH { get; set; }
    }
}
