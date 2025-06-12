using NexoMarket.Data.Repository;
using NexoMarket.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NexoMarket.Business
{
    public class BusinessProducto
    {
        private readonly ProductoRepository _productoRepository;

        public BusinessProducto()
        {
            _productoRepository = new ProductoRepository();
            
        }

        public async Task<List<ProductoEntity>> BuscarProductos()
        {
            return await _productoRepository.BuscarEventosBitacora();
        }
    }
}
