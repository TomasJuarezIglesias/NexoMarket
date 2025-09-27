using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexoMarket.Data;
using NexoMarket.Data.Repository;
using NexoMarket.Entity;

namespace NexoMarket.Business
{
    public class VentaBusiness
    {
        ProductoBusiness _productoBusiness = new ProductoBusiness();
        VentaRepository _ventaRepository = new VentaRepository();
        BitacoraBusiness _bitacoraBusiness = new BitacoraBusiness();
        DigitoVerificadorBusiness _dvBusiness = new DigitoVerificadorBusiness();

        public async Task<bool> AgregarVenta(VentaEntity venta)
        {
            var Products = _productoBusiness.GetProductsFromCart();

            var VentaEntity = _ventaRepository.AgregarVenta(venta);
            if(VentaEntity == null)
                return false;

            foreach (var p in Products)
            {
                var DetalleVenta = new DetalleVentaEntity();
                DetalleVenta.Id_Venta = VentaEntity.Id;
                DetalleVenta.Id_Producto = p.Product.Id;
                DetalleVenta.Cantidad = p.Cantidad;
                DetalleVenta.Precio_Unitario = p.Product.Precio;
                DetalleVenta.Sub_Total = p.Cantidad * p.Product.Precio;
                _ventaRepository.AgregarDetalleVenta(DetalleVenta);
                _productoBusiness.DiscountStock(p.Product.Id, p.Cantidad);
            }

            await _dvBusiness.Recomponer();
            await _bitacoraBusiness.GuardarEventoBitacora("Venta guardada", VentaEntity.Id_Usuario);
            return true;
        }
        public decimal CalcularTotal()
        {
            var Products = _productoBusiness.GetProductsFromCart();
            decimal total = 0;

            foreach (var p in Products)
            {
                total += p.Product.Precio * p.Cantidad;
            }
            return total;
        }
    }
}
