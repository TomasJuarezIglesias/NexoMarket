using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexoMarket.Entity
{
    public class UserCreateEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public bool IsBlocked { get; set; }

        public int Id_Rol { get; set; }
    }
}
