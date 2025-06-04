using System.Collections.Generic;

namespace NexoMarket.Entity 
{
    public class UserAuthEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Rol { get; set; }
        public List<MenuEntity> AllowedMenues { get; set; }
    }
}
