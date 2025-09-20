using NexoMarket.Data.Mapper;
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
        public async Task<List<ProductoEntity>> GetAll()
        {
            using (var context = new NexoMarketEntities())
            {
                var productos = await context.Producto.OrderBy(x => x.Id_Categoria).ToListAsync();
                return MapperConfig.Mapper.Map<List<ProductoEntity>>(productos);
            }
        }

        public async Task<ProductoEntity> GetById(int productId)
        {
            using (var context = new NexoMarketEntities())
            {
                var producto = await context.Producto.FirstOrDefaultAsync(p => p.Id == productId);

                if (producto is null) return null;

                return MapperConfig.Mapper.Map<ProductoEntity>(producto);
            }
        }

        public async Task<bool> HasEnoughStockAsync(int productId, int quantityRequired)
        {
            using (var context = new NexoMarketEntities())
            {
                var product = await context.Producto.FirstOrDefaultAsync(p => p.Id == productId);

                if (product == null) return false;

                return product.Stock >= quantityRequired;
            }
        }
        

        public async Task<List<ProductDvhEntity>> GetAllWithDvh()
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
