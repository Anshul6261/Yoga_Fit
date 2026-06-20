
//pirate...............................................................................................................................................
//w3d6 1
//controller--
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Services;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {   
        private readonly EventService eventService;

        
        public EventController(EventService _eventService){
            eventService = _eventService;
        }

        [HttpGet]
        public IActionResult GetAllEvents()
        {
           var s=  eventService.GetAllEvents();
           if(s==null||s.Count==0)
           {
                return NoContent();
           }
           return Ok(s);
        }
        [HttpGet("{eventId}")]
        public IActionResult GetEventById(int eventId){
            var evt =  eventService.GetEventById(eventId);
            if(evt==null){
                return NotFound();
            }
            return Ok(evt);

        }
        [HttpPost]   
        public IActionResult CreateEvent(Event newEvent)
        {
            if(newEvent==null)
            {
                return BadRequest();
            }
            eventService.CreateEvent(newEvent);
            return CreatedAtAction(nameof(GetEventById), new{eventId= newEvent.EventId},newEvent);

        }
        [HttpPut("{eventId}")]
        public IActionResult UpdateEvent(int eventId,Event updatedEvent)
        {
            if(updatedEvent==null){
                return BadRequest();
            }
            bool updated = eventService.UpdateEvent(eventId, updatedEvent);
            if(!updated)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpDelete("{eventId}")]
        public  IActionResult DeleteEvent(int id)
        {
            bool evt = eventService.DeleteEvent(id);
            if(!evt)
            {
                return NotFound();
            }
            
            return NoContent();
            

        }
             

    }
}
//models

using System;
namespace dotnetapp.Models{
    public class Event{
    public int EventId{get;set;}
    public string Name{get;set;}
    public DateTime Date{get;set;}
    public string Location{get;set;}
}
}
//service

using System;
using System.Collections.Generic;
using System.Linq;
using dotnetapp.Models;
namespace dotnetapp.Services{
    public class EventService{

    private static List<Event> events{get;set;}

    public EventService(){
        events = new List<Event>
        {
            new Event{
                EventId=1,
                Name ="Event 1",
                Date =DateTime.Now.AddDays(7),
                Location="Location 1"
            },
            new Event{
                EventId=2,
                Name ="Event 2",
                Date =DateTime.Now.AddDays(14),
                Location="Location 2"
            },
            new Event{
                EventId=2,
                Name ="Event 3",
                Date =DateTime.Now.AddDays(21),
                Location="Location 3"
            }

        };

        
        
    }
    public List<Event> GetAllEvents(){
        return events.ToList();
    }
    public Event GetEventById(int eventId){
        return events.FirstOrDefault(e=>e.EventId==eventId);
    }
    public void CreateEvent(Event newEvent){
        if(newEvent==null){
            throw new ArgumentNullException("Event cant be null");
        }
        if(newEvent.EventId==0)
        {
            int nxtId = events.Count==0?1:events.Max(p=>p.EventId)+1;
            newEvent.EventId=nxtId;
        }
        events.Add(newEvent);
    }
    public bool UpdateEvent(int eventId, Event updatedEvent){
        Event e = events.FirstOrDefault(p=>p.EventId==eventId);
        if(e==null){
            return false;
        }
        e.Name = updatedEvent.Name;
        e.Date = updatedEvent.Date;
        e.Location  = updatedEvent.Location;
        return true;  
    }
    public bool DeleteEvent(int eventId){
        var s = events.FirstOrDefault(x=>x.EventId==eventId);
        if(s==null){
            return false;
        }
        events.Remove(s);
        return true;

    }   
}
}
//program.cs

using dotnetapp.Services;
using dotnetapp.Models;
var builder = WebApplication.CreateBuilder(args);

// Add Event services to the container.
builder.Services.AddSingleton<EventService>();
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


//w3d6 2
//controller

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Services;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        StudentService studentService;
        public StudentController(StudentService _studentService){
            studentService = _studentService;
        }
        [HttpGet]
        public IActionResult GetAllStudents(){
            var s = studentService.GetAllStudents();
            if(s==null||s.Count==0){
                return NoContent();
            }
            return Ok(s.ToList());
        }
        [HttpGet("{studentId}")]
        public IActionResult GetStudentById(int studentId)
        {
            var s= studentService.GetStudentById(studentId);
            if(s==null){
                return NotFound();
            }
            return Ok(s);

        }
        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            if(student==null){
                return BadRequest();
            }
            studentService.CreateStudent(student);
            return CreatedAtAction(nameof(GetStudentById), new {studentId=student.StudentId }, student);
        }
        [HttpPut("{studentId}")]
        public IActionResult UpdateStudent(int studentId, Student updatedStudent){
            var s = studentService.GetStudentById(studentId);
            if(s==null){
                return NotFound();
            }
            if(updatedStudent==null){
                return BadRequest();
            }
            studentService.UpadateStudent(studentId, updatedStudent);
            return NoContent();

        }
        [HttpDelete("{studentId}")]
        public IActionResult DeleteStudent(int studentId)
        {
            var s = studentService.GetStudentById(studentId);
            if(s==null)
            {
                return NotFound();
            }
            studentService.DeleteStudent(studentId);
            return NoContent();
        }  
    }
}

//models

namespace dotnetapp.Models{
    public class Student{
        public int StudentId{get;set;}
        public string Name{get;set;}
        public int Age {get;set;}
        public string Grade{get;set;}
        
    }
}
//StudentService.cs

using System.Collections.Generic;
using System.Linq;
using dotnetapp.Models;
namespace dotnetapp.Services{
    public class StudentService{
        private List<Student> students{get;set;}

        public StudentService(){
            students  = new List<Student>{
                new Student{
                    StudentId  =1,
                    Name = "Alice",
                    Age = 18,
                    Grade ="A"
                },
                new Student{
                    StudentId  =2,
                    Name = "Bob",
                    Age = 17,
                    Grade ="B"
                },
                new Student{
                    StudentId  =3,
                    Name = "Charlie",
                    Age = 16,
                    Grade ="C"
                },

            };
            
        }
        public List<Student> GetAllStudents(){
                return students.ToList();
        }
        public Student GetStudentById(int studentId){
            var s =  students.FirstOrDefault(x=>x.StudentId==studentId);
            
            return s;
        }
        public void CreateStudent(Student newStudent){
            if (newStudent==null){
                return;
            }
            students.Add(newStudent);
        }
        public void UpadateStudent(int studentId, Student updateStudent){
            var s=  students.FirstOrDefault(x=>x.StudentId==studentId);
            if(s==null){
                return;
            }
            s.Name = updateStudent.Name;
            s.Age = updateStudent.Age;
            s.Grade = updateStudent.Grade;

        }
        public void DeleteStudent(int id){
            var s = students.FirstOrDefault(x=>x.StudentId==id);
            if(s==null) return;
            
            students.Remove(s);
        }



        
    }
}

//program.cs
using dotnetapp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add Event services to the container.

builder.Services.AddSingleton<StudentService>();

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

//w3d6 3

//controller 
using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Models;
namespace dotnetapp.Controllers
{

    [ApiController]

    [Route("api/[controller]")]

    public class BookingController : ControllerBase
    {
    private readonly ApplicationDbContext context;
    public BookingController(ApplicationDbContext _context)
    {

        context = _context;

    }

    // GET: api/booking

    [HttpGet]

    public IActionResult GetBookings()

    {

    var evt = context.Bookings.ToList();

    if (evt == null || evt.Count == 0)

    return Ok(new List<Booking>());

    return Ok(evt);

    }

    // GET: api/booking/{id}

    [HttpGet("{id}")]
    public IActionResult GetBooking(int id)
    {
        var evt = context.Bookings.Find(id);
        if (evt == null)
        return NotFound();

        return Ok(evt);
    }

    // POST: api/booking

    [HttpPost]

    public IActionResult CreateBooking(Booking booking)
    {

        context.Bookings.Add(booking);
        context.SaveChanges();
        return CreatedAtAction(nameof(GetBooking),new { id = booking.BookingId },booking);

    }


    [HttpPut("{bookingId}")]
    public IActionResult UpdateBooking(int bookingId, Booking updatedBooking)
    {

        var evt = context.Bookings.Find(bookingId);
        if (evt == null)
        {
            return NotFound();
        }
    
        evt.BookingId = bookingId;
        evt.CustomerName = updatedBooking.CustomerName;
        evt.EventName = updatedBooking.EventName;
        evt.BookingDate = updatedBooking.BookingDate;
        evt.NumberOfTickets = updatedBooking.NumberOfTickets;
        context.SaveChanges();

        return NoContent();

    }

    // DELETE: api/booking/{bookingId}

    [HttpDelete("{bookingId}")]

    public IActionResult DeleteBooking(int bookingId)
    {

            var evt = context.Bookings.Find(bookingId);
            if (evt == null)
            return NotFound();

            context.Bookings.Remove(evt);
            context.SaveChanges();

            return NoContent();

    }

    }

}
//data / appdbcontext

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

    // Constructor

    public ApplicationDbContext(DbContextOptions options)

    : base(options)

    {

    }

    // DbSet for Booking table

    public DbSet<Booking> Bookings { get; set; }

    }

}
// models

using System.ComponentModel.DataAnnotations;
namespace dotnetapp.Models{
    public class Booking
{
    [Key]
    public int BookingId { get; set; }
    public string CustomerName { get; set; }
    public string EventName { get; set; }
    public DateTime BookingDate { get; set; }
    public int NumberOfTickets { get; set; }
}
}

//program.cs

using dotnetapp.Data;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);


// Add Event services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext <ApplicationDbContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));
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





class Rectangle {
    constructor(width, height) {
        this.width = width;
        this.height = height;
    }

    calculateArea() {
        return this.width * this.height;
    }

    calculatePerimeter() {
        return 2 * (this.width + this.height);
    }
}

const input = require('fs').readFileSync(0, 'utf8').trim().split(/\s+/).map(Number);

const width = input[0];
const height = input[1];

const rect = new Rectangle(width, height);

console.log("Area: " + rect.calculateArea());
console.log("Perimeter: " + rect.calculatePerimeter());



const fs = require('fs');
const input = fs.readFileSync(0, 'utf8').trim().split(/\s+/).map(Number);

const a = input[0];
const b = input[1];

function errorLoggingMiddleware(callback) {
    return function (...args) {
        try {
            return callback(...args);
        } catch (error) {
            console.log("Error:", error.message);
        }
    };
}

function divide(x, y) {
    if (y === 0) {
        throw new Error("Division by zero");
    }
    return x / y;
}

const safeDivide = errorLoggingMiddleware(divide);
const result = safeDivide(a, b);

if (result !== undefined) {
    console.log("Result: " + result);
}



