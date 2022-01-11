using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi2.DBOperations;

namespace WebApi2.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateBookCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if (book != null)
            {
                throw new InvalidOperationException("Kitap zaten mevcut");
            }

            else
            {
                book = _mapper.Map<Book>(Model);
                    //new Book();
                //book.PublishDate = Model.PublishDate;
                //book.Title = Model.Title;
                //book.PageCount = Model.PageCount;
                //book.GenreId = Model.GenreId;

                _dbContext.Books.Add(book);
                _dbContext.SaveChanges();
              
            }

        }

    }
    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
