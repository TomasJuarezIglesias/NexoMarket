using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexoMarket.Entity.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public bool IsBlocked { get; set; }

        public RolDto Rol { get; set; }
    }
}
