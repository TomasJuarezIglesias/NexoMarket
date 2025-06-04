using NexoMarket.Data.Mapper;
using NexoMarket.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NexoMarket.Data.Repository
{
    public class EventoBitacoraRepository
    {
        public async Task<List<BitacoraEventoEntity>> BuscarEventosBitacora()
        {
            using (var context = new NexoMarketEntities())
            {
                var eventos = await context.BitacoraEvento
                    .Include(b => b.Usuarios.Rol)
                    .OrderByDescending(b => b.fecha)
                    .ToListAsync();

                return MapperConfig.Mapper.Map<List<BitacoraEventoEntity>>(eventos);
            }
        }

        public async Task GuardarEventoBitacora(BitacoraEventoCreateEntity evento)
        {
            try
            {
                var entity = MapperConfig.Mapper.Map<BitacoraEvento>(evento);
                using (var context = new NexoMarketEntities())
                {
                    context.BitacoraEvento.Add(entity);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
