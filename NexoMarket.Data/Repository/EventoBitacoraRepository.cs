using NexoMarket.Data.Mapper;
using NexoMarket.Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexoMarket.Data.Repository
{
    public class EventoBitacoraRepository
    {
        public async Task<List<BitacoraEventoEntity>> BuscarEventosBitacora()
        {
            using (var context = new NexoMarketEntities())
            {
                var query = from bi in context.BitacoraEvento
                            join u in context.Usuarios on bi.id_user equals u.Id
                            join r in context.RolSet on u.IdRol equals r.Id_Rol
                            select new BitacoraEventoEntity
                            {
                                Evento = bi.evento,
                                Fecha = bi.fecha,
                                Usuario = u.Username,
                                Rol = r.Nombre
                            };
                query = query.OrderByDescending(x => x.Fecha);

                return await query.ToListAsync();
            }
        }

        public async Task<bool> GuardarEventoBitacora(BitacoraEvento evento)
        {
            BitacoraEvento resultado;
            using (var context = new NexoMarketEntities())
            {
                resultado = context.BitacoraEvento.Add(evento);
                await context.SaveChangesAsync();
            }
            return resultado.id != 0;
        }
    }
}
