using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexoMarket.Data.Mapper;
using NexoMarket.Entity;
using NexoMarket.Service;

namespace NexoMarket.Data.Repository
{
    public class VentaRepository
    {
        public bool AgregarDetalleVenta(DetalleVentaEntity detalleVenta)
        {
            try
            {
                var entity = MapperConfig.Mapper.Map<DetalleVenta>(detalleVenta);
                using (var context = new NexoMarketEntities())
                {
                    context.DetalleVenta.Add(entity);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public VentaEntity AgregarVenta(VentaEntity venta)
        {

            try
            {
                var entity = MapperConfig.Mapper.Map<Venta>(venta);
                using (var context = new NexoMarketEntities())
                {
                    context.Venta.Add(entity);
                    context.SaveChanges();
                }
                return MapperConfig.Mapper.Map<VentaEntity>(entity);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public VentaEntity SelectUltVenta()
        {
            using (var context = new NexoMarketEntities())
            {
                var lastVenta = context.Venta.OrderByDescending(v => v.Id).FirstOrDefault();
                return MapperConfig.Mapper.Map<VentaEntity>(lastVenta);
            }
        }
    }
}
