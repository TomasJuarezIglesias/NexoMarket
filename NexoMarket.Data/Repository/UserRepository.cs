using NexoMarket.Data.Mapper;
using NexoMarket.Entity;
using System.Data.Entity;
using System.Threading.Tasks;

namespace NexoMarket.Data.Repository
{
    public class UserRepository
    {
        public async Task<UserEntity> Login(string username, string password)
        {
            using (var context = new NexoMarketEntities())
            {
                var user = await context.Usuarios.FirstOrDefaultAsync(u => u.Username == username && u.Password == password );

                return MapperConfig.Mapper.Map<UserEntity>(user);
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
                user.Is_Blocked = true;
                await context.SaveChangesAsync();
            }
        }
    }
}
