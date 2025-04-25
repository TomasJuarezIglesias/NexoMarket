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

        public List<MenuDto> GetMenusByUser(int userId)
        {
            using (var context = new NexoMarketEntities())
            {
                var menus = context.UsuarioMenu
                        .Include(m => m.Menu)
                        .Where(m => m.IdUsuario == userId)
                        .Select(m => m.Menu)
                        .ToList();

                return MapperConfig.Mapper.Map<List<MenuDto>>(menus);
            }

        }


    }
}
