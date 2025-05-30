﻿using NexoMarket.Data;
using NexoMarket.Data.Repository;
using NexoMarket.Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexoMarket.Business
{
    public class BusinessBitacora
    {
        private readonly EventoBitacoraRepository _eventoBitacoraRepository;

        public BusinessBitacora()
        {
            _eventoBitacoraRepository = new EventoBitacoraRepository();
        }

        public async Task<bool> GuardarEventoBitacora(string descripcion_evento, int id_user)
        {
            var evento = new BitacoraEvento()
            {
                evento = descripcion_evento,
                id_user = id_user,
                fecha = DateTime.Now
            };
            return await _eventoBitacoraRepository.GuardarEventoBitacora(evento);
        }

        public async Task<List<BitacoraEventoDTO>> BuscarEventosBitacora()
        {
            return await _eventoBitacoraRepository.BuscarEventosBitacora();

        }
    }
}