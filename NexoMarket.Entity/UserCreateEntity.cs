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
        public bool IsBlocked { get; set; }

        public RolEntity Rol { get; set; }
    }
}
