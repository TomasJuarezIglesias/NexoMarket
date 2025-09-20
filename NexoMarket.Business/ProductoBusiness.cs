using NexoMarket.Data;
using NexoMarket.Data.Repository;
using NexoMarket.Entity;
using NexoMarket.Service;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NexoMarket.Business
{
    public class ProductoBusiness
    {
        private readonly ProductoRepository _productoRepository;
        private readonly XmlService _xmlService;
        private const string _fileName = "Carrito.xml";

        public ProductoBusiness()
        {
            _productoRepository = new ProductoRepository();
            _xmlService = new XmlService();
        }

        public async Task<List<ProductoEntity>> BuscarProductos()
        {
            return await _productoRepository.GetAll();
        }

        public async Task<ProductoEntity> GetById(int productId)
        {
            return await _productoRepository.GetById(productId);
        }

        public async Task<bool> HasStock(int productId, int quantityRequired)
        {
            var products = GetProductsFromCart().FirstOrDefault(p => p.Product.Id == productId);

            quantityRequired += products is null ? 0 : products.Cantidad;

            return await _productoRepository.HasEnoughStockAsync(productId, quantityRequired);
        }

        public void AddToCart(ProductoEntity product, int quantity)
        {
            var products = GetProductsFromCart();

            var productFound = products.FirstOrDefault(p => p.Product.Id == product.Id);

            if (productFound is null)
                products.Add(new ProductoCarritoEntity { Product = product, Cantidad = quantity });
            else
                productFound.Cantidad += quantity;

            _xmlService.SaveXml(_fileName, products);
        }

        public void RemoveFromCart(int productId)
        {
            var products = GetProductsFromCart();

            var productFound = products.FirstOrDefault(p => p.Product.Id == productId);
            if (productFound != null)
            {
                products.Remove(productFound);
                _xmlService.SaveXml(_fileName, products);
            }
        }

        public List<ProductoCarritoEntity> GetProductsFromCart()
        {
            return _xmlService.LoadXml<List<ProductoCarritoEntity>>(_fileName) ?? new List<ProductoCarritoEntity>();
        }

        public void EmptyCart()
        {
            _xmlService.DeleteXml(_fileName);
        }
    }
}
