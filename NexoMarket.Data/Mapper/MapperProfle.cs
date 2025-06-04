using AutoMapper;
using NexoMarket.Entity;

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
