using NexoMarket.Data;
using NexoMarket.Data.Repository;
using NexoMarket.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NexoMarket.Business
{
    public class BitacoraBusiness
    {
        private readonly EventoBitacoraRepository _eventoBitacoraRepository;

        public BitacoraBusiness()
        {
            _eventoBitacoraRepository = new EventoBitacoraRepository();
        }

        public async Task GuardarEventoBitacora(string descripcion_evento, int id_user)
        {
            var evento = new BitacoraEventoCreateEntity()
            {
                Evento = descripcion_evento,
                Id_user = id_user
            };

            await _eventoBitacoraRepository.GuardarEventoBitacora(evento);
        }

        public async Task<List<BitacoraEventoEntity>> BuscarEventosBitacora()
        {
            return await _eventoBitacoraRepository.BuscarEventosBitacora();
        }
    }
}