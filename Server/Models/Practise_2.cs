namespace dotnetapp.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using log4net;

namespace dotnetapp.Controllers
{
    public class BookController : Controller
    {
        private static readonly ILog log =
            LogManager.GetLogger(typeof(BookController));

        private static List<Book> books = new List<Book>()
        {
            new Book{Id=1,Title="C# Basics",Author="John",Price=500},
            new Book{Id=2,Title="ASP.NET Core",Author="Smith",Price=800}
        };

        public IActionResult Index()
        {
            log.Info("Index method called");
            return Ok(books);
        }

        public IActionResult Search(string title)
        {
            log.Info($"Searching book : {title}");

            var result = books.FirstOrDefault(b =>
                b.Title.Contains(title ?? "", StringComparison.OrdinalIgnoreCase));

            if(result == null)
            {
                log.Warn("Book not found");
                return NotFound();
            }

            return Ok(result);
        }

        public IActionResult Details(int id)
        {
            log.Info($"Details requested for book id {id}");

            var book = books.FirstOrDefault(x => x.Id == id);

            if(book == null)
            {
                log.Error("Book not found");
                return NotFound();
            }

            return Ok(book);
        }

        public IActionResult Purchase(int id)
        {
            log.Info($"Purchase initiated for book id {id}");

            var book = books.FirstOrDefault(x => x.Id == id);

            if(book == null)
            {
                log.Error("Purchase failed");
                return NotFound();
            }

            log.Info("Purchase successful");
            return Ok("Purchase Successful");
        }
    }
}

<?xml version="1.0" encoding="utf-8" ?>
<log4net>
  <appender name="FileAppender"
            type="log4net.Appender.FileAppender">
    <file value="bookstore.log" />
    <appendToFile value="true" />
    <layout type="log4net.Layout.PatternLayout">
      <conversionPattern value="%date %-5level %message%newline" />
    </layout>
  </appender>

  <root>
    <level value="ALL" />
    <appender-ref ref="FileAppender" />
  </root>
</log4net>
