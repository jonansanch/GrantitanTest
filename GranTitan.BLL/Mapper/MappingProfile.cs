using AutoMapper;
using GranTitan.DAL.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GranTitan.BLL.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Book, BookCreateDto>().ReverseMap();
        }
    }
}
