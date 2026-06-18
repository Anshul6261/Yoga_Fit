ORDER
 
PROGRAM.CS ORDER
using dotnetapp.Services;
 
var builder = WebApplication.CreateBuilder(args);
 
// Add Event services to the container.
builder.Services.AddSingleton<OrderService>();
builder.Services.AddControllers();
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
 
 
ORDER.CS
using System;
namespace dotnetapp.Models
{
     public class Order
     {
        public int OrderId {get;set;}
        public string CustomerName {get;set;}
        public DateTime OrderDate {get;set;}
        public decimal TotalAmount {get;set;}
        public string Status {get;set;}
 
     }
}
 
ORDER SERVICE
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Models;
using System.Linq;
 
namespace dotnetapp.Services
{
    public class OrderService
    {
        private static List<Order> list {get;set;}
 
        public OrderService()
        {
            list = new List<Order>
            {
            new Order
            {
                OrderId=1 ,
                CustomerName ="John Doe",
                OrderDate = new DateTime(2023,1,1),
                TotalAmount=100.50m,
                Status ="Shipped"
            },
            new Order
            {
                OrderId=2 ,
                CustomerName ="Jane Smith",
                OrderDate = new DateTime(2023,2,15),
                TotalAmount=250.75m,
                Status ="Processing"
            },
 
            new Order
            {
                 OrderId=3 ,
                CustomerName ="Alice Johnson",
                OrderDate = new DateTime(2023,3,20),
                TotalAmount=150.00m,
                Status ="Delivered"
            }
        };
 
    }
 
    public List<Order>GetAllOrders()
    {
        return list.ToList();
    }
 
    public Order GetOrderById(int orderId)
    {
        return list.FirstOrDefault(o => o.OrderId ==orderId);
 
    }
    public void AddOrder(Order newOrder)
    {
        if(newOrder==null)
        return ;
 
   
    int id=list.Count +1;
    newOrder.OrderId=id;
    list.Add(newOrder);
}
 }
 
}
 
 
ORDER CONTROLLER
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
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService orderService;
        public OrderController(OrderService _orderService)
        {
            orderService = _orderService;
 
        }
        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var orders = orderService.GetAllOrders();
            if(orders==null || orders.Count==0)
            {
                return NoContent();
            }
 
            return Ok(orders);
 
        }
 
        [HttpGet("{orderId}")]
        public IActionResult GetOrderById(int orderId)
        {
            var order = orderService.GetOrderById(orderId);
            if(order==null)
            {
                return NotFound();
            }
            return Ok(order);
        }
        [HttpPost]
 
            public IActionResult CreateOrder([FromBody]Order newOrder)
        {
            if(newOrder==null)
                return BadRequest();
           
            orderService.AddOrder(newOrder);
            return CreatedAtAction(
                nameof(GetOrderById),new {orderId = newOrder.OrderId},
                newOrder
            );
        }
       
 
       
    }
}
 
PRODUCT
 
 
PRODUCT CONTROLLER
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using dotnetapp.Services;
 
namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
         private readonly ProductService productService;
        public ProductController(ProductService _productService)
        {
            productService = _productService;
        }
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok(productService.GetAllProducts());
        }
        [HttpGet("{productId}")]
        public IActionResult GetProductById(int productId)
        {
            var evt = productService.GetProductById(productId);
            if(evt == null)
                return NotFound();
 
                return Ok(evt);
           
        }
        [HttpPost]
        public IActionResult CreateProduct([FromBody]Product newproduct)
        {
            if(newproduct== null)
            {
                return BadRequest();
            }
            productService.AddProduct(newproduct);
            return CreatedAtAction(nameof(GetProductById), new{productId =newproduct.Id},newproduct);
        }
 
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id,[FromBody]Product updatedProduct)
        {
            if(updatedProduct== null)
            {
                return BadRequest();
            }
            productService.UpdateProduct(id,updatedProduct);
            return NoContent();
        }
 
        [HttpDelete("{productId}")]
 
        public IActionResult DeleteProduct(int productId)
        {
            productService.DeleteProduct(productId);
            return NoContent();
        }
    }
}
 
PRODUCT CS
using System ;
 
namespace dotnetapp.Models
{
    public class Product
    {
        public int Id{get;set;}
        public string Name {get;set;}
 
        public decimal Price {get;set;}
        public string Description {get;set;}
       
    }
}
 
PRODUCT SERVICE
 
using System;
using System.Collections.Generic;
using System.Linq;
using dotnetapp.Models;
 
namespace dotnetapp.Services
{
    public class ProductService
    {
        private static List<Product>products {get;set;}
        public ProductService()
        {
            products= new List<Product>
            {
                new Product{Id=1, Name="Product 1", Price=10.99m,Description="Description 1"},
                new Product{Id=2, Name="Product 2", Price=10.99m,Description="Description 2"},
                new Product{Id=3, Name="Product 3", Price=10.99m,Description="Description 3"},
            } ;
        }
            public List<Product> GetAllProducts()
            {
                return products.ToList();
            }
 
            public Product GetProductById(int productId)
            {
                return products.FirstOrDefault(p=>p.Id==productId);
 
            }
 
            public void AddProduct(Product newProduct)
            {
                if(newProduct== null)
                {
                    throw new ArgumentNullException("Product is empty");
                }
                if(newProduct.Id ==0)
                {
                    int nextId = products.Count ==0?1:products.Max(p=>p.Id)+1;
                    newProduct.Id= nextId;
                }
                products.Add(newProduct);
 
            }
 
            public void UpdateProduct(int id,Product updateProduct)
            {
                Product findProduct = products.FirstOrDefault(p=>p.Id==id);
                if(findProduct== null)
                {
                    return;
                }
                else{
                    findProduct.Name=updateProduct.Name;
                    findProduct.Price=updateProduct.Price;
                    findProduct.Description=updateProduct.Description;
 
                }
            }
 
            public void DeleteProduct(int id)
            {
                Product product = products.FirstOrDefault(p=>p.Id==id);
                if(product == null)
                {
                    return;
                }
                products.Remove(product);
               
            }
        }
    }
 
 
PROGRAM
 
 
using dotnetapp.Services;
 
var builder = WebApplication.CreateBuilder(args);
 
// Add Event services to the container.
builder.Services.AddSingleton<ProductService>();
 
builder.Services.AddControllers();
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
 
 
INVENTORY
 
INVENTORY CONTROLLER
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Services;
 
namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        public InventoryService inventoryService;
        public InventoryController(InventoryService _inventoryService)
        {
            inventoryService =_inventoryService;
        }
 
        [HttpGet]
        public IActionResult GetAllInventories()
        {
            var e= inventoryService.GetAllItems();
            if(e==null || e.Count==0)
             return NotFound();
 
             return Ok(e);
        }
 
        [HttpGet("{itemId}")]
        public IActionResult GetInventoriesById(int itemId)
        {
            var e= inventoryService.GetItemById(itemId);
            if(e== null)
            return NotFound();
 
            return Ok(e);
        }
        [HttpPost]
        public IActionResult CreateInventoryItem(InventoryItem newItem)
        {
                if(newItem == null)
                return BadRequest();
               
                inventoryService.AddItem(newItem);
                return CreatedAtAction(nameof(GetInventoriesById),new {itemId = newItem.ItemId},newItem);
        }
       
    }
}
 
INVENTORY ITEM
 
 
using System.ComponentModel.DataAnnotations;
namespace dotnetapp.Models
{
    // Define the InventoryItem class and its properties here.
    public class InventoryItem
    {
        [Key]
        public int ItemId {get;set;}
        public string ItemName {get;set;}
        public int Quantity {get;set;}
        public decimal Price {get;set;}
        public string Category {get;set;}
 
    }
}
 
 
APPDB
 
 
using System;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
namespace dotnetapp.Models
{
    // Write the ApplicationDbContext class here
    // Define the DbSet properties for the InventoryItem entity
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
 
        }
        public DbSet<InventoryItem>InventoryItems{get;set;}
 
 
    }
}
 
SERVICE
 
using System;
using dotnetapp.Models;
using System.Collections.Generic;
using System.Linq;
 
namespace dotnetapp.Services
{
    //Define InventoryService class here
 
    public class InventoryService
    {
    public List<InventoryItem> list{get;set;}
    private readonly ApplicationDbContext context;
    public InventoryService(ApplicationDbContext _context)
    {
       context =_context;
       list= new List<InventoryItem>();
    }
    public List<InventoryItem>GetAllItems()
    {
        if(context != null)
        {
            return context.InventoryItems.ToList();
        }
        return list.ToList();
    }
 
    public InventoryItem GetItemById(int itemId)
    {
        if(context!=null)
        {
            return context.InventoryItems.FirstOrDefault(x => x.ItemId == itemId);
        }
        return list.FirstOrDefault(x => x.ItemId == itemId);
    }
    public void AddItem(InventoryItem newItem)
    {
        if(newItem == null)
         return;
 
         if(context!=null){
         context.InventoryItems.Add(newItem);
         context.SaveChanges();
         }
         else
         {
            list.Add(newItem);
         }
 
    }
    }
 
}
 
PROGRAM CS
 
using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.EntityFrameworkCore;
 
 
var builder = WebApplication.CreateBuilder(args);
 
// Add Event services to the container.
builder.Services.AddScoped<InventoryService>();
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("myconnstring"))
);
builder.Services.AddControllers();
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
 
 --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

     dotnetapp/Controllers/ProductsController.cs
     using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Data;
using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {   private readonly ApplicationDbContext context;
        public ProductsController(ApplicationDbContext _context)
        {
            context = _context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Product>>GetProducts()
        {
            
            return Ok(context.Products.ToList());
        }
        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var evt=context.Products.FirstOrDefault(x=>x.Id==id) ;
            if(evt==null){
                return NotFound();
            }
            return Ok(evt);
        }
        [HttpGet("filter")]
        public ActionResult<IEnumerable<Product>>GetProductsByCategory([FromQuery] string category)
        {
            var evt = context.Products.Where(x=>x.Category==category).ToList();
            if(evt==null){
                return NotFound();
            }
            return Ok(evt);
        }
        [HttpPost]
        public ActionResult<Product> CreateProduct(Product product){
            context.Products.Add(product);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetProductById),new {id=product.Id}, product);
        }

    }
}
dotnetapp/Data/ApplicationDbContext.cs

using Microsoft.EntityFrameworkCore;
using dotnetapp.Models;

namespace dotnetapp.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<Product> Products {get;set;}
    }
    

}
dotnetapp/Models/Product.cs

using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetapp.Models{
    public class Product{
        [Key]
        public int Id{get;set;}
        public string Name{get;set;}
        public string Category{get;set;}
        public decimal Price{get;set;}
        public int Stock{get;set;}

    }
}
dotnetapp/Program.cs
using dotnetapp.Data;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(
    options=>options.UseSqlServer(builder.Configuration.GetConnectionString("ProductConnection"))
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



cod 2 router
 
controller:
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Data;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtworksController : ControllerBase
    {
        ApplicationDbContext context;
        public ArtworksController(ApplicationDbContext _context)
        {
            context = _context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Artwork>> GetArtworks()
        {
            var evt = context.Artworks.ToList();
            if(evt==null)
            {
                return NotFound();
            }
            return Ok(evt);
        }
        [HttpGet("{id}")]
        public ActionResult<Artwork> GetArtWorkById(int id)
        {
            var evt = context.Artworks.FirstOrDefault(x=>x.ArtworkId==id);
            if(evt==null)
            {
                return NotFound();
            }
            return Ok(evt);
        }
        [HttpGet("filter")]
        public ActionResult<IEnumerable<Artwork>> GetArtworksByArtist([FromQuery] string artist)
        {
            var evt = context.Artworks.Where(x=>x.Artist==artist).ToList();
            if(evt == null)
            {
                return NotFound();
            }
            return Ok(evt);
        }
        [HttpPost]
        public ActionResult<Artwork> CreateArtwork(Artwork artwork)
        {
            context.Artworks.Add(artwork);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetArtWorkById),new{id=artwork.ArtworkId},artwork);
        }
    }
}
-------------------------------------------------------------------------------------------

 
ApplcationDbContext:
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
using Microsoft.EntityFrameworkCore;
using dotnetapp.Models;
namespace dotnetapp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Artwork> Artworks{get;set;}
    }
}
 
-----------------------------------------------------------------------------------
Models:
using System.ComponentModel.DataAnnotations;
namespace dotnetapp.Models
{
    public class Artwork
    {
    [Key]
    public int ArtworkId{get;set;}
    public string Title{get;set;}
    public string Artist{get;set;}
    public int Year{get;set;}
    public string Medium{get;set;}
    public string Description{get;set;}
    }
}
--------------------------------------------------------
program.cs:
 
using Microsoft.EntityFrameworkCore;
using dotnetapp.Models;
using dotnetapp.Data;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(
    options=>options.UseSqlServer(builder.Configuration.GetConnectionString("myconnstring")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
--------------------------------------------------------------------------------------
appsettings.json
 
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "myconnstring": "User ID=sa;password=examlyMssql@123;server=localhost;Database=appdb;trusted_connection=false;Persist Security Info=False;Encrypt=False"
  }
}
 
 
----------------------------------------------------------------
COD 3 ROUTER:
 
Controller:
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models;
using dotnetapp.Data;
using Microsoft.AspNetCore.Mvc;
namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        ApplicationDbContext context;
        public PetsController(ApplicationDbContext _context)
        {
            context = _context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Pet>> GetPets()
        {
            return Ok(context.Pets.ToList());
        }
        [HttpGet("{id}")]
        public ActionResult<Pet> GetPetById(int id)
        {
            var evt = context.Pets.FirstOrDefault(x=>x.PetId==id);
            if(evt==null)
            {
                return NotFound();
            }
            return Ok(evt);
        }
        [HttpGet("filter")]
        public ActionResult<IEnumerable<Pet>> GetPetsByType([FromQuery] string type)
        {
            return Ok(context.Pets.Where(x=>x.Type==type));
        }
        [HttpPost]
        public ActionResult<Pet> CreatePet(Pet pet)
        {
            context.Pets.Add(pet);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetPetById),new{id=pet.PetId},pet);
        }
    }
}
 
---------------------------------------------------------------------------------
Models:
 
using System;
using System.ComponentModel.DataAnnotations;
namespace dotnetapp.Models
{
    public class Pet
    {
        [Key]
        public int PetId{get;set;}
        public string Name{get;set;}
        public string Type{get;set;}
        public string Breed{get;set;}
        public int Age{get;set;}
        public string Description{get;set;}
    }
}
 
----------------------------------------------------------------------------
Program.cs:
 
using dotnetapp.Models;
using dotnetapp.Data;
using dotnetapp.Controllers;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(
    options=>options.UseSqlServer(builder.Configuration.GetConnectionString("myconnstring"))
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
 
------------------------------------------------------------------------
ApplicationDbContext:
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
namespace dotnetapp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Pet> Pets{get;set;}
    }
}
 

appdsetting.json:
 
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "myconnstring": "User ID=sa;password=examlyMssql@123;server=localhost;Database=appdb;trusted_connection=false;Persist Security Info=False;Encrypt=False"
  }
}
 

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
50000000000000000000000
    w3day5 ses2 cod 2
    dotnetapp/Controllers/HomeController.cs

    using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;

namespace dotnetapp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

dotnetapp/Models/ApplicationDbContext.cs
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
namespace dotnetapp.Models{
    public class ApplicationDbContext: DbContext{
        public ApplicationDbContext(DbContextOptions options): base(options){

        }
        public DbSet<Course> Courses{get;set;}
        public DbSet<Student>Students{get;set;}
        public DbSet<Enrollment>Enrollments{get;set;}
        public DbSet<Instructor>Instructors{get;set;}
        public DbSet<CourseAssignment>CourseAssignments{get;set;}

    }
}

dotnetapp/Models/Course.cs
namespace dotnetapp.Models{
    public class Course{
    public int CourseID{get;set;}
    public string Title{get;set;}
    public int Credits{get;set;}
    public ICollection<Enrollment> Enrollments{get;set;}
}
}
dotnetapp/Models/CourseAssignment.cs
namespace dotnetapp.Models{
    public class CourseAssignment{
    public int CourseAssignmentID{get;set;}
    public int CourseID{get;set;}
    public int InstructorID{get;set;}
    public Course? Course{get;set;}
    public Instructor? Instructor{get;set;}
}
}
dotnetapp/Models/Enrollment.cs
namespace dotnetapp.Models{
public class Enrollment{
    public int EnrollmentID{get;set;}
    public int CourseID{get;set;}
    public int StudentID{get;set;}
    public Course? Course{get;set;}
    public Student? Student{get;set;}
}
}
dotnetapp/Models/Instructor.cs

namespace dotnetapp.Models{
    public class Instructor{
    public int InstructorID{get;set;}
    public string FirstName{get;set;}
    public string LastName{get;set;}
    public DateTime HireDate{get;set;}
    public ICollection<CourseAssignment> CourseAssignments{get;set;}
    
}
}
dotnetapp/Models/Student.cs
namespace dotnetapp.Models{
public class Student{
    public int StudentID{get;set;}
    public string FirstName{get;set;}
    public string LastName{get;set;}
    public ICollection<Enrollment> Enrollments{get;set;}
}
}
dotnetapp/Program.cs

using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(
    options=>options.UseSqlServer(builder.Configuration.GetConnectionString("myconnstring"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
