using NexoMarket.Data.Repository;
using NexoMarket.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexoMarket.Business
{
    public class BusinessProducto
    {
        public ProductoRepository _productoRepository = new ProductoRepository();

        public async Task<List<ProductoEntity>> BuscarProductos()
        {
            return await _productoRepository.BuscarEventosBitacora();
        }
    }
}
