using NexoMarket.Data.Mapper;
using NexoMarket.Entity;
using NexoMarket.Service;
using System.Collections.Generic;
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
                var user = await context.Usuarios.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

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

        public async Task<bool> CreateUser(UserCreateEntity user)
        {
            using (var context = new NexoMarketEntities())
            {
                var usuario = MapperConfig.Mapper.Map<Usuarios>(user);
                var userDvhEntity = MapperConfig.Mapper.Map<UserDvhEntity>(usuario);
                var userDb = MapperConfig.Mapper.Map<Usuarios>(DigitoVerificadorService<UserDvhEntity>.CalcularDVH(userDvhEntity));

                context.Usuarios.Add(userDb); 
                await context.SaveChangesAsync(); 
                return true;
            }
        }

        public async Task<List<UserDvhEntity>> GetAll()
        {
            using (var context = new NexoMarketEntities())
            {
                var users = await context.Usuarios.ToListAsync();

                return MapperConfig.Mapper.Map<List<UserDvhEntity>>(users);
            }
        }

        public async Task SaveRange(List<UserDvhEntity> userList)
        {
            using (var context = new NexoMarketEntities())
            {
                var userDbList = MapperConfig.Mapper.Map<List<Usuarios>>(userList);

                foreach (var userDb in userDbList)
                {
                    context.Entry(userDb).State = EntityState.Modified;
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
