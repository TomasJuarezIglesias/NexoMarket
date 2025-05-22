using NexoMarket.Data.Mapper;
using NexoMarket.Entity.Dtos;
using NexoMarket.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace NexoMarket.Data.Repository
{
    public class UserRepository
    {
        public async Task<List<UserDto>> GetUsuariosAsync()
        {
            using (var context = new NexoMarketEntities())
            {
                var users = await context.Usuarios.ToListAsync();
                return MapperConfig.Mapper.Map<List<UserDto>>(users);
            }
        }


        public async Task<UserDto> Login(string username, string password)
        {
            using (var context = new NexoMarketEntities())
            {
                var user = await context.Usuarios.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

                return MapperConfig.Mapper.Map<UserDto>(user);
            }
        }

    }
}
