using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexoMarket.Data.Repository
{
    public class UserRepository
    {

        public async Task<List<Usuarios>> GetUsuariosAsync()
        {
            using (var context = new NexoMarketEntities())
            {
                var users = await context.Usuarios.ToListAsync();
                return users;
            }
        }


    }
}
