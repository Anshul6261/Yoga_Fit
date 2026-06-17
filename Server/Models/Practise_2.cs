dotnetapp/Controllers/LibraryController.cs :
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Models;
using FluentValidation;
using System.Runtime.InteropServices;
 
namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibraryController : Controller
    {
        private readonly AppDbContext context;
        public LibraryController(AppDbContext _context)
        {
            context = _context;  
        }
        [HttpGet]
        public IActionResult DisplayBooksForLibraryCard(int libraryCardId)
        {
            var books = context.Books
                                .Where(b=>b.LibraryCardId==libraryCardId)
                                .ToList();
            ViewBag.LibraryCardId = libraryCardId;
            return View(books);
        }
        [HttpGet]
        public IActionResult DisplayAllBooks()
        {
            var books = context.Books
                                .Include(b=>b.LibraryCard)
                                .ToList();
            return View(books);
        }
        [HttpGet]
        public IActionResult SearchBooksByTitle(string query)
        {
            var bookQuery = context.Books.AsQueryable();
            if(!string.IsNullOrWhiteSpace(query))
            {
                var lower = query.ToLower();
                bookQuery = bookQuery.Where(b=>b.Title.ToLower().Contains(lower));
            }
            var result = bookQuery.ToList();
            ViewBag.SearchQuery = query;
            return View(result);
        }
        [HttpGet]
        public IActionResult AddBook()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBook(Book book)
        {
            if(!ModelState.IsValid)
            {
                return View(book);
            }
            var validator = new BookValidator();
            var validationResult = validator.Validate(book);
            if(!validationResult.IsValid)
            {
                foreach(var error in validationResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);
                }
                return View(book);
            }
            context.Books.Add(book);
            context.SaveChanges();
           
            return RedirectToAction(nameof(DisplayAllBooks));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteBook(int id)
        {
            var book = context.Books.Find(id);
            if(book==null)
            {
                return NotFound();
            }
            int? libraryCardId = book.LibraryCardId;
            context.Books.Remove(book);
            context.SaveChanges();
            if(libraryCardId.HasValue)
            {
                return RedirectToAction(nameof(DisplayBooksForLibraryCard),
                    new {libraryCardId = libraryCardId.Value});
            }
            return RedirectToAction(nameof(DisplayAllBooks));
        }
    }
}
