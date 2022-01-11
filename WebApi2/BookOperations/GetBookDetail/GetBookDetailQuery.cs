using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi2.Common;
using WebApi2.DBOperations;

namespace WebApi2.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int BookId { get; set; }
        public GetBookDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Where(b => b.Id == BookId).SingleOrDefault();
            if(book == null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı!");
            }
            BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book);
                //new BookDetailViewModel();
            //vm.Title = book.Title;
            //vm.PageCount = book.PageCount;
            //vm.Genre = ((GenreEnum)book.GenreId).ToString();
            //vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
            return vm;
        }
    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}
