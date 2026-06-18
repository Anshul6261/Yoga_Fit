//COD1

controller
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Services;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly IBookService service;

        public BookController(IBookService _service)
        {
            service = _service;
        }

        [HttpGet]
        public ActionResult<List<Book>> GetAllBooks()
        {
            var evt = service.GetBooks();

            if (evt == null)
                return NoContent();

            return Ok(evt);
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBookById(int id)
        {
            var evt = service.GetBook(id);

            if (evt == null)
                return NotFound();

            return Ok(evt);
        }

        [HttpPost]
        public ActionResult<Book> AddBook([FromBody] Book book)
        {
            if (book == null)
                return BadRequest();

            var evt = service.SaveBook(book);
            return Ok(evt);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book book)
        {
            if (book == null)
                return BadRequest();

            var upd = service.UpdateBook(id, book);

            if (upd == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            service.DeleteBook(id);
            return NoContent();
        }
    }
}

//order contoller

using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using dotnetapp.Services;

namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService service;

        public OrderController(IOrderService _service)
        {
            service = _service;
        }

        [HttpGet]
        public ActionResult<List<Order>> GetAllOrders()
        {
            return Ok(service.GetOrders());
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetOrderById(int id)
        {
            var evt = service.GetOrder(id);

            if (evt == null)
                return NotFound();

            return Ok(evt);
        }

        [HttpPost]
        public ActionResult<Order> AddOrder([FromBody] Order order)
        {
            if (order == null)
                return BadRequest();

            return Ok(service.SaveOrder(order));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] Order order)
        {
            if (order == null)
                return BadRequest();

            service.UpdateOrder(id, order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            service.DeleteOrder(id);
            return NoContent();
        }
    }
}

models
Book in data folder
using System.ComponentModel.DataAnnotations;
namespace dotnetapp.Models{
    
    public class Book{
        [Key]
        public int BookId{get;set;}
        public string BookName{get;set;}
        public string Category {get;set;}
        
        public decimal Price {get;set;}
        
    }
}
Order.cs in data folder
using System.ComponentModel.DataAnnotations;
namespace dotnetapp.Models{
    public class Order{
        [Key]
        public int OrderId{get;set;}
        public string CustomerName{get;set;}
        public decimal TotalAmount{get;set;}
    }
}


repositoryy
BookRepository.cs

using dotnetapp.Models;

namespace dotnetapp.Repository
{
    public class BookRepository
    {
        public List<Book> books = new();
        private int nid = 1;

        public List<Book> GetBooks() => books;

        public Book GetBook(int id)
        {
            return books.FirstOrDefault(x => x.BookId == id);
        }

        public Book SaveBook(Book book)
        {
            book.BookId = nid++;
            books.Add(book);
            return book;
        }

        public Book UpdateBook(int id, Book book)
        {
            Book b = books.FirstOrDefault(x => x.BookId == id);

            if (b != null)
            {
                b.BookName = book.BookName;
                b.Category = book.Category;
                b.Price = book.Price;
            }

            return b;
        }

        public bool DeleteBook(int id)
        {
            Book b = books.FirstOrDefault(x => x.BookId == id);

            if (b != null)
            {
                books.Remove(b);
                return true;
            }

            return false;
        }
    }
}

OrderRepository.cs
using dotnetapp.Models;

namespace dotnetapp.Repository
{
    public class OrderRepository
    {
        public List<Order> orders = new();
        private int nid = 1;

        public List<Order> GetOrders() => orders;

        public Order GetOrder(int id)
        {
            return orders.FirstOrDefault(x => x.OrderId == id);
        }

        public Order SaveOrder(Order order)
        {
            order.OrderId = nid++;
            orders.Add(order);
            return order;
        }

        public Order UpdateOrder(int id, Order order)
        {
            Order o = orders.FirstOrDefault(x => x.OrderId == id);

            if (o != null)
            {
                o.CustomerName = order.CustomerName;
                o.TotalAmount = order.TotalAmount;
            }

            return o;
        }

        public bool DeleteOrder(int id)
        {
            Order o = orders.FirstOrDefault(x => x.OrderId == id);

            if (o != null)
            {
                orders.Remove(o);
                return true;
            }

            return false;
        }
    }
}

services/
 BookService.cs
 using dotnetapp.Models;
 using dotnetapp.Repository;
 namespace dotnetapp.Services{
    public interface IBookService
    {
        List<Book> GetBooks();
        Book GetBook(int id);
        Book SaveBook(Book book);
        Book UpdateBook(int id, Book book);
        bool DeleteBook(int id);
    }
    public class BookService : IBookService
    {
        private readonly BookRepository brepo;

        public BookService(BookRepository _brepo)
        {
            brepo = _brepo;
        }

        public List<Book> GetBooks() => brepo.GetBooks();
        public Book GetBook(int id) => brepo.GetBook(id);
        public Book SaveBook(Book book) => brepo.SaveBook(book);
        public Book UpdateBook(int id, Book book) => brepo.UpdateBook(id, book);
        public bool DeleteBook(int id) => brepo.DeleteBook(id);
    }
    
}
OrderService.cs
 using dotnetapp.Models;
using dotnetapp.Repository;

namespace dotnetapp.Services{
    public interface IOrderService
    {
        List<Order> GetOrders();
        Order GetOrder(int id);
        Order SaveOrder(Order order);
        Order UpdateOrder(int id, Order order);
        bool DeleteOrder(int id);
    }
    public class OrderService : IOrderService
    {
        private readonly OrderRepository orepo;

        public OrderService(OrderRepository _orepo)
        {
            orepo = _orepo;
        }

        public List<Order> GetOrders() => orepo.GetOrders();
        public Order GetOrder(int id) => orepo.GetOrder(id);
        public Order SaveOrder(Order order) => orepo.SaveOrder(order);
        public Order UpdateOrder(int id, Order order) => orepo.UpdateOrder(id, order);
        public bool DeleteOrder(int id) => orepo.DeleteOrder(id);
    }
}

program.cs
using dotnetapp.Repository;
using dotnetapp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<BookRepository>();
builder.Services.AddSingleton<OrderRepository>();

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();

//swagger cod = 2

/home/coder/project/workspace/dotnetapp/Controllers/MobilePhoneController.cs
using dotnetapp.Services;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MobilePhoneController : ControllerBase
    {
        private IMobilePhoneService service;

        public MobilePhoneController(IMobilePhoneService _service)
        {
            service = _service;
        }

        [HttpGet]
        public IActionResult GetAllMobilePhones()
        {
            return Ok(service.GetMobilePhones());
        }

        [HttpGet("{id}")]
        public IActionResult GetMobilePhoneById(int id)
        {
            var evt = service.GetMobilePhone(id);

            if (evt == null)
                return NotFound();

            return Ok(evt);
        }

        [HttpPost]
        public IActionResult AddMobilePhone(MobilePhone mobilePhone)
        {
            service.SaveMobilePhone(mobilePhone);

            return CreatedAtAction(
                nameof(GetMobilePhoneById),
                new { id = mobilePhone.MobilePhoneId },
                mobilePhone
            );
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMobilePhone(int id, MobilePhone mobilePhone)
        {
            var ump = service.GetMobilePhone(id);

            if (ump == null)
                return NotFound();

            service.UpdateMobilePhone(id, mobilePhone);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMobilePhone(int id)
        {
            service.DeleteMobilePhone(id);
            return NoContent();
        }
    }
}
/home/coder/project/workspace/dotnetapp/Models/MobilePhone.cs
namespace dotnetapp.Models
{
    public class MobilePhone
    {
        public int MobilePhoneId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
dotnetapp/Repository/MobilePhoneRepository.cs

using dotnetapp.Models;

namespace dotnetapp.Repository
{
    public class MobilePhoneRepository
    {
        public List<MobilePhone> list = new();

        public List<MobilePhone> GetMobilePhones()
        {
            return list.ToList();
        }

        public MobilePhone GetMobilePhone(int id)
        {
            return list.FirstOrDefault(x => x.MobilePhoneId == id);
        }

        public MobilePhone SaveMobilePhone(MobilePhone mobilePhone)
        {
            list.Add(mobilePhone);
            return mobilePhone;
        }

        public MobilePhone UpdateMobilePhone(int id, MobilePhone mobilePhone)
        {
            MobilePhone ump = list.FirstOrDefault(x => x.MobilePhoneId == id);

            ump.Brand = mobilePhone.Brand;
            ump.Model = mobilePhone.Model;
            ump.Price = mobilePhone.Price;
            ump.StockQuantity = mobilePhone.StockQuantity;

            return mobilePhone;
        }

        public bool DeleteMobilePhone(int id)
        {
            MobilePhone dmp = list.FirstOrDefault(x => x.MobilePhoneId == id);

            if (dmp != null)
            {
                list.Remove(dmp);
                return true;
            }

            return false;
        }
    }
}    
dotnetapp/Services/IMobilePhoneService.cs
using dotnetapp.Models;
using dotnetapp.Repository;

namespace dotnetapp.Services{
    public interface IMobilePhoneService
    {
        List<MobilePhone> GetMobilePhones();
        MobilePhone GetMobilePhone(int id);
        MobilePhone SaveMobilePhone(MobilePhone mobilePhone);
        MobilePhone UpdateMobilePhone(int id, MobilePhone mobilePhone);
        bool DeleteMobilePhone(int id);
    }
}
dotnetapp/Services/MobilePhoneService.cs
using dotnetapp.Models;
using dotnetapp.Repository;

namespace dotnetapp.Services{
     public class MobilePhoneService : IMobilePhoneService
    {
        private readonly MobilePhoneRepository repo;

        public MobilePhoneService(MobilePhoneRepository _repo)
        {
            repo = _repo;
        }

        public List<MobilePhone> GetMobilePhones() => repo.GetMobilePhones();

        public MobilePhone GetMobilePhone(int id)
            => repo.GetMobilePhone(id);

        public MobilePhone SaveMobilePhone(MobilePhone mobilePhone)
            => repo.SaveMobilePhone(mobilePhone);

        public MobilePhone UpdateMobilePhone(int id, MobilePhone mobilePhone)
            => repo.UpdateMobilePhone(id, mobilePhone);

        public bool DeleteMobilePhone(int id)
            => repo.DeleteMobilePhone(id);
    }
}
dotnetapp/Program.cs
using dotnetapp.Repository;
using dotnetapp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<MobilePhoneRepository>();
builder.Services.AddScoped<IMobilePhoneService, MobilePhoneService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.Run();



//cod 3
dotnetapp/Controllers/ExpenseController.cs :
 
 
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Mvc;
 
namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/expense")]
    public class ExpenseController : ControllerBase
    {
        ExpenseService expenseService;
        public ExpenseController(ExpenseService _expenseService)
        {
            expenseService = _expenseService;
        }
        [HttpGet]
        public IActionResult GetAllExpenses()
        {
            var evt = expenseService.GetAllExpenses();
            if(evt == null)
            {
                return NoContent();
            }
            return Ok(evt);
        }
 
        [HttpGet("{expenseId}")]
        public IActionResult GetExpenseById(int expenseId)
        {
            var evt = expenseService.GetExpenseById(expenseId);
            if(evt == null)
            {
                return NotFound();
            }
            return Ok(evt);
        }
 
        [HttpPost]
        public IActionResult CreateExpense(Expense newExpense)
        {
            if(newExpense==null)
            {
                return BadRequest();
            }
            expenseService.CreateExpense(newExpense);
            return CreatedAtAction(nameof(GetExpenseById), new{expenseId=newExpense.ExpenseId},newExpense);
        }
 
        [HttpPut("{expenseId}")]
        public IActionResult UpdateExpense(int expenseId, Expense updatedExpense)
        {
            if(updatedExpense==null)
            {
                return BadRequest();
            }
            if(expenseService.GetExpenseById(expenseId)==null)
            {
                return NotFound();
            }
            return NoContent();
        }
 
        [HttpDelete("{expenseId}")]
        public IActionResult DeleteExpense(int expenseId)
        {
            if(expenseService.GetExpenseById(expenseId)==null)
            {
                return NotFound();
            }
            return NoContent();
        }
 
    }
}
 
dotnetapp/Models/Expense.cs :
 
 
using System;
namespace dotnetapp.Models
{
    public class Expense
    {
        public int ExpenseId{get;set;}
        public string Description{get;set;}
        public decimal Amount{get;set;}
        public DateTime Date{get;set;}
        public string Category{get;set;}
    }
}
 
dotnetapp/Services/ExpenseService.cs :
 
 
using System;
using System.Collections.Generic;
using System.Linq;
using dotnetapp.Models;
namespace dotnetapp.Services
{
    public class ExpenseService
    {
        private readonly List<Expense> Expenses = new List<Expense>
        {
            new Expense{ExpenseId=1,Description="Lunch",Amount=15.00m,Date=DateTime.Now.AddDays(-7),Category="Food"},
            new Expense{ExpenseId=2,Description="Groceries",Amount=50.00m,Date=DateTime.Now.AddDays(-14),Category="Food"},
            new Expense{ExpenseId=3,Description="Bus Ticket",Amount=2.50m,Date=DateTime.Now.AddDays(-21),Category="Transport"}
        };
        public List<Expense> GetAllExpenses()
        {
            return Expenses.ToList();
        }
        public Expense GetExpenseById(int expenseId)
        {
            return Expenses.FirstOrDefault(x=>x.ExpenseId==expenseId);
        }
        public void CreateExpense(Expense newExpense)
        {
            if(newExpense==null)
            {
                return;
            }
            int nextId = Expenses.Count==0?1:Expenses.Max(e=>e.ExpenseId)+1;
            newExpense.ExpenseId = nextId;
            Expenses.Add(newExpense);
        }
        public void UpdateExpense(int expenseId, Expense updatedExpense)
        {
            Expense expense = Expenses.FirstOrDefault(e=>e.ExpenseId==expenseId);
            if(expenseId==null)
            {
                return;
            }
            expense.Amount = updatedExpense.Amount;
            expense.Category = updatedExpense.Category;
            expense.Date = updatedExpense.Date;
            expense.Description = updatedExpense.Description;
        }
        public void DeleteExpense(int expenseId)
        {
            Expense expense = Expenses.FirstOrDefault(e=>e.ExpenseId==expenseId);
            if(expenseId==null)
            {
                return;
            }
            Expenses.Remove(expense);
        }
    }
 
}
 
dotnetapp/Program.cs :
 
 
using dotnetapp.Models;
using dotnetapp.Services;
using dotnetapp.Controllers;
var builder = WebApplication.CreateBuilder(args);
 
// Add Event services to the container.
 
builder.Services.AddControllers();
builder.Services.AddSingleton<ExpenseService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 
var app = builder.Build();
 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
 
app.UseHttpsRedirection();
 
app.UseAuthorization();
 
app.MapControllers();
 
app.Run();
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 
 
