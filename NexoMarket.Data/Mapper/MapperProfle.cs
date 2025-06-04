using AutoMapper;
using NexoMarket.Entity.Dtos;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexoMarket.Data.Mapper
{
    public class MapperProfle : Profile
    {
        public MapperProfle()
        {
            CreateMap<Usuarios, UserEntity>().ReverseMap();
            CreateMap<Menu, MenuEntity>();
            CreateMap<Rol, RolEntity>();
            CreateMap<Permiso, PermisoEntity>();
        }



    }
}
