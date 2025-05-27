using NexoMarket.Data.Mapper;
using NexoMarket.Entity.Dtos;
using NexoMarket.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace NexoMarket.Data.Repository
{
    public class UserRepository
    {
        public async Task<UserDto> Login(string username, string password)
        {
            using (var context = new NexoMarketEntities())
            {
                var user = await context.Usuarios.FirstOrDefaultAsync(u => u.Username == username && u.Password == password );

                return MapperConfig.Mapper.Map<UserDto>(user);
            }
        }

        public async Task<bool> ExistByUsername(string username)
        {
            using (var context = new NexoMarketEntities())
            {
                return await context.Usuarios.AnyAsync(u => u.Username == username);
            }
        }

        public async Task BlockUser(string username)
        {
            using (var context = new NexoMarketEntities())
            {
                var user = await context.Usuarios.FirstAsync(u => u.Username == username);
                user.IsBlocked = true;
                await context.SaveChangesAsync();
            }
        }
    }
}
