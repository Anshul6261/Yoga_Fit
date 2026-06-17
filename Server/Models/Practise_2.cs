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


dotnetapp/Models/AppDbContext.cs : 
 
using System;
using Microsoft.EntityFrameworkCore;
 
namespace dotnetapp.Models
{
    public class AppDbContext : DbContext
    {
        // Constructor that accepts DbContextOptions and passes it to the base class constructor
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
 
        // Define DbSet properties to represent the collections of Book and LibraryCard entities in the database
        public DbSet<Book> Books{get;set;} = null;
        public DbSet<LibraryCard> LibraryCards {get;set;} = null;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring the relationship between Book and LibraryCard
            // A Book has one LibraryCard
            // A LibraryCard can have many Books
            // The foreign key in the Book entity is LibraryCardId
 
            // Seeding initial data into the LibraryCard table
            // Unique identifier for the library card
            // Card number
            // Name of the cardholder
            // Expiry date of the library card
 
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LibraryCard>()
                .HasMany(l=>l.Books)
                .WithOne(b=>b.LibraryCard)
                .HasForeignKey(b=>b.LibraryCardId);
            modelBuilder.Entity<LibraryCard>().HasData(
                new LibraryCard
                {
                    Id = 1,
                    CardNumber = "LC-12345",
                    MemberName = "John Doe",
                    ExpiryDate = new DateTime(2025,12,31)
                },
                new LibraryCard
                {
                    Id = 2,
                    CardNumber = "LC-54321",
                    MemberName = "Jane Smith",
                    ExpiryDate = new DateTime(2024,10,15)
                }
            );
        }
    }
}


dotnetapp/Models/Book.cs :
 
 
using System.ComponentModel.DataAnnotations;
namespace dotnetapp.Models
{
    public class Book
    {
        [Key]
        public int Id{get;set;} // Auto Increment by EF
 
        [Required]
        [MaxLength(100)]
        public string Title{get;set;} = null;
 
        [Required]
        [MaxLength(50)]
        public string Author{get;set;} = null;
 
        // Should be between 1000 and 2024
        [Range(1000, 2024, ErrorMessage = "PublishedYear must be between 1000 and 2024")]
        public int PublishedYear{get;set;}
        // nullable FK - a book may or may not be borrowed
        public int? LibraryCardId{get;set;}
        // navigation
        public LibraryCard? LibraryCard{get;set;}
    }
}
 
 
