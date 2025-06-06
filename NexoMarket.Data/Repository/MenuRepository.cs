﻿using NexoMarket.Data.Mapper;
using NexoMarket.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NexoMarket.Data.Repository
{
    public class MenuRepository
    {
        public List<MenuEntity> GetMenusByUser(int userId)
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
                    
                    return MapperConfig.Mapper.Map<List<MenuEntity>>(menus);
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