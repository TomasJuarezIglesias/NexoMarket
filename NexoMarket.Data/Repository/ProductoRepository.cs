using NexoMarket.Data.Mapper;
using NexoMarket.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexoMarket.Data.Repository
{
    public class ProductoRepository
    {
        public async Task<List<ProductoEntity>> BuscarEventosBitacora()
        {
            using (var context = new NexoMarketEntities())
            {
                var productos = await context.Producto.ToListAsync();

                return MapperConfig.Mapper.Map<List<ProductoEntity>>(productos);
            }
        }
    }
}
