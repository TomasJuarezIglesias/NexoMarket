﻿using NexoMarket.Data.Mapper;
using NexoMarket.Entity;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace NexoMarket.Data.Repository
{
    public class ProductoRepository
    {
        public async Task<List<ProductoEntity>> BuscarEventosBitacora()
        {
            using (var context = new NexoMarketEntities())
            {
                var productos = await context.Producto.OrderBy(x => x.Id_Categoria).ToListAsync();
                return MapperConfig.Mapper.Map<List<ProductoEntity>>(productos);
            }
        }

        public async Task<List<ProductDvhEntity>> GetAll()
        {
            using (var context = new NexoMarketEntities())
            {
                var productos = await context.Producto.ToListAsync();

                return MapperConfig.Mapper.Map<List<ProductDvhEntity>>(productos);
            }
        }

        public async Task SaveRange(List<ProductDvhEntity> productList)
        {
            using (var context = new NexoMarketEntities())
            {
                var productDbList = MapperConfig.Mapper.Map<List<Producto>>(productList);

                foreach (var product in productDbList)
                {
                    context.Entry(product).State = EntityState.Modified;
                }

                await context.SaveChangesAsync();
            }
        }


    }
}
