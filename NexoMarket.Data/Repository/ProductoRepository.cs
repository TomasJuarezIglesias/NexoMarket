using NexoMarket.Data.Mapper;
using NexoMarket.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
        public async Task<DataTable> GetTopProductos()
        {
            using (var context = new NexoMarketEntities())
            {
                // 1. Calcular total facturación
                var totalFacturacion = await context.DetalleVenta.SumAsync(v => v.Sub_Total);

                // 2. Armar lista con las mismas columnas que el SP
                var topProductos = await context.DetalleVenta
                    .GroupBy(v => new { v.Id_Producto, v.Producto.Nombre })
                    .Select(g => new
                    {
                        NombreProducto = g.Key.Nombre,
                        Monto = g.Sum(x => x.Sub_Total),
                        Cantidad = g.Sum(x => x.Cantidad),
                        TotalFacturacion = totalFacturacion  // mismo valor en todas las filas
                    })
                    .OrderByDescending(x => x.Monto)
                    .Take(5)
                    .ToListAsync();

                // 3. Armar DataTable con las columnas del SP
                var datosSP = new DataTable();
                datosSP.Columns.Add("NombreProducto", typeof(string));
                datosSP.Columns.Add("Monto", typeof(decimal));
                datosSP.Columns.Add("Cantidad", typeof(int));
                datosSP.Columns.Add("TotalFacturacion", typeof(decimal));

                foreach (var p in topProductos)
                {
                    datosSP.Rows.Add(p.NombreProducto, p.Monto, p.Cantidad, p.TotalFacturacion);
                }

                return datosSP;
            }
        }

        public void UpdateStock(int id, int cantidad)
        {
            using (var context = new NexoMarketEntities())
            {
                context.Database.ExecuteSqlCommand(
            "UPDATE Producto SET Stock = Stock - @p0 WHERE Id = @p1",
            cantidad, id);
            }
        }
    }
}
