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
            try
            {
                using (var context = new NexoMarketEntities())
                {
                    var menus = context.Usuarios
                            .Where(u => u.Id == userId)
                            .SelectMany(u => u.Rol.Permiso)
                            .SelectMany(m => m.Menu)
                            .ToList();
                    
                    return MapperConfig.Mapper.Map<List<MenuDto>>(menus);
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception("Error al obtener los menús del usuario", ex);
            }

        }
    }
}