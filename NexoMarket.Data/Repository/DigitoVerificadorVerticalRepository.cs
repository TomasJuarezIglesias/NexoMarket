using AutoMapper;
using NexoMarket.Data.Mapper;
using NexoMarket.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexoMarket.Data.Repository
{
    public class DigitoVerificadorVerticalRepository
    {

        public async Task SaveRange(List<DigitoVerificadorVerticalEntity> digitoVerificadorList)
        {
            using (var context = new NexoMarketEntities())
            {
                var digitoVerificadorDbList = MapperConfig.Mapper.Map<List<DigitoVerificadorVertical>>(digitoVerificadorList);

                var nuevos = digitoVerificadorDbList.Where(x => x.Id == 0).ToList();
                var existentes = digitoVerificadorDbList.Where(x => x.Id != 0).ToList();

                if (nuevos.Any())
                    context.DigitoVerificadorVertical.AddRange(nuevos);

                foreach (var item in existentes)
                    context.Entry(item).State = EntityState.Modified;

                await context.SaveChangesAsync();
            }
        }

        public async Task<List<DigitoVerificadorVerticalEntity>> GetAll()
        {
            using (var context = new NexoMarketEntities())
            {
                var digitosList = await context.DigitoVerificadorVertical.ToListAsync();

                return MapperConfig.Mapper.Map<List<DigitoVerificadorVerticalEntity>>(digitosList);
            }
        }


    }
}
