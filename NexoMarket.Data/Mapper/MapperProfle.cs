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
            CreateMap<Usuarios, UserDto>().ReverseMap();
            CreateMap<Menu, MenuDto>();
            CreateMap<Rol, RolDto>();
            CreateMap<Permiso, PermisoDTO>();
        }



    }
}
