using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexoMarket.Entity.Dtos
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public bool IsBlocked { get; set; }

        public RolEntity Rol { get; set; }
    }
}
