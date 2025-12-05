using AutoMapper;
using InventarioApi.Models;
using InventarioApi.DTOs.ProductoDto;
using InventarioApi.DTOs.CategoriaDto;
using InventarioApi.DTOs.ProvedorDto;
using InventarioApi.DTOs.Provedor;
using InventarioApi.DTOs.MovimientoDto;

namespace InventarioApi.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            //Mapeo Producto --> ProductoDTO
            //llenamos los atributos de nombre producto y provedor desde las relaciones
            CreateMap<Producto, ProductoDTO>()
                .ForMember(dest => dest.UsuarioNombre, opt => opt.MapFrom(src => src.Usuario.UserName))
                .ForMember(dest => dest.CategoriaNombre, opt => opt.MapFrom(src => src.Categoria.Nombre))
                .ForMember(dest => dest.ProvedorNombre, opt => opt.MapFrom(src => src.Provedor.Nombre));

            //Mapeo Producto --> ProductoCreateDTO
            CreateMap<Producto, ProductoCreateDTO>().ReverseMap(); // se agrega reverse map para que se pueda mapear bidireccionalmente

            //Mapeo Producto --> ProductoUpdateDTO
            CreateMap<Producto, ProductoUpdateDTO>().ReverseMap();

            //Mapeo Categoria --> CategoriaDTO
            CreateMap<Categoria, CategoriaDTO>()
                .ForMember(dest => dest.usuarioNombre, opt => opt.MapFrom(src => src.Usuario.UserName));

            //Mapeo Categoria -- CategoriaDTO
            CreateMap<Categoria, CategoriaCreateUpdateDTO>().ReverseMap();

            //Mapeo de Provedor --> ProvedorDTO
            CreateMap<Provedor, ProvedorDTO>()
                .ForMember(dest => dest.NombreUsuario, opt => opt.MapFrom(src => src.Usuario.UserName));

            CreateMap<Provedor, ProvedorCreateUpdateDTO>().ReverseMap();

            //Mapeo de Movimiento --> MovimientoInventarioDTO
            CreateMap<MovimientoInventario, MovimientoInventarioDTO>()
                .ForMember(dest => dest.UsuarioNombre, opt => opt.MapFrom(src => src.Usuario.UserName))
                .ForMember(dest => dest.ProductoNombre, opt => opt.MapFrom(src => src.Producto.Nombre));

            CreateMap<MovimientoInventario, MovimientoDTO>().ReverseMap();
        }

    }
}
