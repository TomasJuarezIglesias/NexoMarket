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
            CreateMap<BitacoraEvento, BitacoraEventoEntity>()
                .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.Usuarios.Username))
                .ForMember(dest => dest.Rol, opt => opt.MapFrom(src => src.Usuarios.Rol.Nombre));

            CreateMap<BitacoraEventoCreateEntity, BitacoraEvento>();
            CreateMap<UserCreateEntity, Usuarios>();
        }

    }
}
