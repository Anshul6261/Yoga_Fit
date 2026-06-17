dotnetapp/Controllers/ArtworksController.cs :
 
 
 
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
 
dotnetapp/Data/ApplicationDbContext.cs :
 
 
 
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
 
dotnetapp/Models/Artwork.cs :
 
 
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
 
Get started with Swashbuckle and ASP.NET Core | Microsoft Learn
Learn how to add Swashbuckle to your ASP.NET Core web API project to integrate the Swagger UI.
 
Practice_Route_QueryString_3 :
 
dotnetapp/Controllers/PetsController.cs :
 
 
 
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
 
dotnetapp/Data/ApplicationDbContext.cs :
 
 
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
 
dotnetapp/Models/Pet.cs :
 
 
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
 
dotnetapp/Program.cs :
 
 
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
 
Get started with Swashbuckle and ASP.NET Core | Microsoft Learn
Learn how to add Swashbuckle to your ASP.NET Core web API project to integrate the Swagger UI.
