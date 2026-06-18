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
 
 
