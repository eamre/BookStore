using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi2.BookOperations.DeleteBook;
using WebApi2.BookOperations.GetBooks;
using WebApi2.BookOperations.UpdateBook;
using WebApi2.CreateBook;
using WebApi2.DBOperations;
using WebApi2.GetBookDetail;
using static WebApi2.BookOperations.UpdateBook.UpdateBookCommand;
using static WebApi2.CreateBook.CreateBookCommand;

namespace WebApi2.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController:ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context,_mapper);
            var result = query.Handles();
            return Ok(result);
            //var bookList = _context.Books.OrderBy(x => x.Id).ToList<Book>();
            //return bookList;
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
                query.BookId = id;
                result=query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);

            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] UpdateBookModel updatedBook)
        {
            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.Model = updatedBook;
                command.BookId = id;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok();

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok();
        }
    }
    
}

//private static List<Book> BookList = new List<Book>()
        //{
        //new Book
        //{
        //    Id=1,
        //    Title="Learn Startup",
        //    GenreId=1,
        //    PageCount=200,
        //    PublishDate=new DateTime(2001,06,12)
        //},
        //new Book
        //{
        //    Id=2,
        //    Title="Herland",
        //    GenreId=2,
        //    PageCount=250,
        //    PublishDate=new DateTime(2010,05,23)
        //},
        //new Book
        //{
        //    Id=3,
        //    Title="Dune",
        //    GenreId=2,
        //    PageCount=540,
        //    PublishDate=new DateTime(2001,12,21)
        //}
        



//var book = _context.Books.SingleOrDefault(x => x.Id == id);
            //if (book == null)
            //{
            //    return BadRequest();
            //}
            //book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            //book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            //book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            //book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
