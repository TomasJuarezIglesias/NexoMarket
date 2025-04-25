using NexoMarket.Data.Dtos;
using NexoMarket.Data.Mapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexoMarket.Data.Repository
{
    public class MenuRepository
    {

        public async Task<List<MenuDto>> GetMenusByUser(int userId)
        {
            using (var context = new NexoMarketEntities())
            {
                var menus = await context.UsuarioMenu
                        .Include(m => m.Menu)
                        .Where(m => m.IdUsuario == userId)
                        .Select(m => m.Menu)
                        .ToListAsync();

                return MapperConfig.Mapper.Map<List<MenuDto>>(menus);
            }

        }


    }
}
