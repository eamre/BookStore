using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi2.BookOperations.GetBooks;
using WebApi2.CreateBook;
using WebApi2.GetBookDetail;

namespace WebApi2.Common
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>().ForMember(d=>d.Genre, opt=>opt.MapFrom(src=>((GenreEnum)src.GenreId).ToString()));
            CreateMap<Book, BooksViewModel>().ForMember(d => d.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString())); ;
        }
    }
}
