using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexoMarket.Entity
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsBlocked { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public RolEntity Rol { get; set; }

    }
}
