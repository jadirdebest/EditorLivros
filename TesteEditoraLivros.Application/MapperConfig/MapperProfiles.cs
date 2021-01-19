using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TesteEditoraLivros.Application.DTO;
using TesteEditoraLivros.Domain.Models;

namespace TesteEditoraLivros.Application.MapperConfig
{
    //Costumo centralizar tudo que for de registro de serviços em apenas um lugar, 
    //nessa classe facilita na hora de criar mapeamentos para os objetos que possuo

    //Um detalhe interesante, eu não utilizei o mapeamento da view para meus objetos da aplicação, apenas por não ser necessário devido
    //a ter poucos objetos que iria ser utilizado e agilizar esse teste, porém as configurações não seriam nessa camada, eu iria configurar 
    //na camada de apresentação onde meus ModelViews iriam ser visíveis para tal uso.
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

            CreateMap<Register, RegisterDTO>();
            CreateMap<RegisterDTO, Register>();

            CreateMap<Book, BookDTO>();
            CreateMap<BookDTO, Book>();

            CreateMap<Role, RoleDTO>();
            CreateMap<RoleDTO, Role>();

        }
    }
}
