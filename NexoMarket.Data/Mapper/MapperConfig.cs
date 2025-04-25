using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexoMarket.Data.Mapper
{
    public static class MapperConfig
    {
        public static IMapper Mapper { get; private set; }

        public static void Init()
        {
            if (Mapper is null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<MapperProfle>();
                });

                Mapper = config.CreateMapper();
            }
        }
    }
}
