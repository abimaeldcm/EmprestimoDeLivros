using AutoMapper;
using EmprestimoLivros.API.DTOs;
using EmprestimoLivros.API.Models;

namespace EmprestimoLivros.API.Mappings
{
    public class EntityToDTOMpappingProfile : Profile
    {
        //olhar o sturtup
        public EntityToDTOMpappingProfile()
        {
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<Livro, LivroDTO>().ReverseMap();
        }
    }
}
