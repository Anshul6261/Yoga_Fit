using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using dotnetapp.Models;
 
namespace dotnetapp.Services
{
    public class BooksService // Class for Business logic and DBOps
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["myconnstring"].ConnectionString;
 
        public static void AddBook(List<Book> books, int libraryId, string title, string author, string category, decimal price)
        {
            int nextId = books.Count == 0 ? 1 : books[books.Count - 1].Id + 1; // Ternary Operator (? :), short-hand if-else; 0: assign 1, else: last ID + 1
            books.Add(new Book
            {
                Id = nextId,
                Title = title,
                Author = author,
                Category = category,
                Price = price,
                LibraryId = libraryId
 
            });
            Console.WriteLine("Book added successfully!");
        }
 
        public static void ListBooks(List<Book> books, List<Library> libraries) // Spec demands List<Library> libraries to be here
        {
            if(books == null || books.Count == 0)
            {
                Console.WriteLine("No Books available.");
                return;
            }
            Console.WriteLine("List of Books:");
            foreach(Book b in books)
            {
                Console.WriteLine($"Book ID: {b.Id}, Title: {b.Title}, Author: {b.Author}, Category: {b.Category}, Price: ${b.Price}");
            }
        }
 
        public static void FindBook(List<Book> books, string bookTitle)
        {
            Book b = books.Find(x=>x.Title == bookTitle); // x => is x tends to; => is a Lambda expression; We are using the Find() Function here.
            if(b == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }
 
            Console.WriteLine($"Book ID: {b.Id}, Title: {b.Title}, Author: {b.Author}, Category: {b.Category}, Price: ${b.Price}");
               
        }
 
        public static void EditBook(List<Book> books, int id, string title, string author, string category, string price)
        {
            Book b = books.Find(x=>x.Id == id);
            if(b == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }
 
            if(!string.IsNullOrEmpty(title)) b.Title = title;
            if(!string.IsNullOrEmpty(author)) b.Author = author;
            if(!string.IsNullOrEmpty(category)) b.Category = category;
            if(!string.IsNullOrEmpty(price)) b.Price = decimal.Parse(price);
 
            Console.WriteLine("Book information updated successfully!");
   
        }
 
        public static void DeleteBook(List<Book> books, int id)
        {
            Book b = books.Find(x=>x.Id==id);
            if(b==null)
            {
                Console.WriteLine("Book not found.");
                return;
            }
            books.Remove(b);
            Console.WriteLine("Book deleted successfully!");
               
        }
 
        public static void AddBookToDB(int libraryId, string title, string author, string category, decimal price)
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Books", connectionString);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                DataTable table = new DataTable();
                adapter.Fill(table);
 
                DataRow row = table.NewRow();
                row["Title"] = title;
                row["Author"] = author;
                row["Category"] = category;
                row["Price"] = price;
                row["LibraryId"] = libraryId;
                table.Rows.Add(row);
 
                adapter.Update(table);
                Console.WriteLine("Book added to the database successfully!");
 
            }
            catch(Exception)
            {
                Console.WriteLine("Database connection failed.");
            }
               
        }
 
        public static void ListBooksFromDB()
        {  
            try
            {
                string query = "SELECT b.Id, b.Title, b.Author, b.Category, b.Price, l.Name AS LibraryName FROM Books b INNER JOIN Libraries l ON b.LibraryId = l.Id";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connectionString);
                DataTable table = new DataTable();
                adapter.Fill(table);
 
                foreach(DataRow row in table.Rows)
                {
                    Console.WriteLine($"Book ID: {row["Id"]}, Title: {row["Title"]}, Author: {row["Author"]}, Category: {row["Category"]}, Price: ${row["Price"]}, Library: {row["LibraryName"]}");
                }
                   
            }
            catch(Exception)
            {
                Console.WriteLine("Database connection failed.");
            }
        }
 
        public static void EditBookInDB(int id, string title, string author, string category, string price)
        {   try
            {
                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM Books WHERE Id = {id}", connectionString);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                DataTable table = new DataTable();
                adapter.Fill(table);
 
                if(table.Rows.Count == 0)
                {
                    Console.WriteLine("Book not found.");
                    return;
                }  
 
                DataRow row = table.Rows[0];
 
                if(!string.IsNullOrEmpty(title)) row["Title"] = title;
                if(!string.IsNullOrEmpty(author)) row["Author"] = author;
                if(!string.IsNullOrEmpty(category)) row["Category"] = category;
                if(!string.IsNullOrEmpty(price)) row["Price"] = decimal.Parse(price);
 
                adapter.Update(table);
                Console.WriteLine("Book updated successfully!");
 
            }
 
            catch(Exception)
            {
                Console.WriteLine("Database connection failed.");
            }
           
        }
 
        public static void DeleteBookFromDB(int id)
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM Books WHERE Id = {id}", connectionString);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                DataTable table = new DataTable();
                adapter.Fill(table);
 
                if(table.Rows.Count == 0)
                {
                    Console.WriteLine("Book not found.");
                    return;
                }
 
                table.Rows[0].Delete();
                adapter.Update(table);
                Console.WriteLine("Book deleted successfully from the database!");
 
            }
 
            catch(Exception)
            {
                Console.WriteLine("Database connection failed.");
 
            }
        }
       
 
    }
}
 
 
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using dotnetapp.Models;
 
namespace dotnetapp.Services
{
    public class LibrariesService // Class for Business logic and DBOps
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["myconnstring"].ConnectionString;
        public static void AddLibrary(List<Library> libraries, string name, string address, int maximumCapacity)
        {
            int nextId = libraries.Count == 0 ? 1 : libraries[libraries.Count - 1].Id + 1;
            libraries.Add(new Library
            {
                Id = nextId,
                Name = name,
                Address = address,
                MaximumCapacity = maximumCapacity
 
            });
 
            Console.WriteLine("Library added successfully!");
        }
 
        public static void ListLibraries(List<Library> libraries)
        {
            if(libraries==null || libraries.Count == 0)
            {  
                Console.WriteLine("No Libraries available.");
                return;
            }
 
            Console.WriteLine("List of Libraries:");
 
            foreach(Library l in libraries)
            {
               
                Console.WriteLine($"Library ID: {l.Id}, Name: {l.Name}, Address: {l.Address}, Capacity: {l.MaximumCapacity}");
               
            }
       
        }
 
        public static void AddLibraryToDB(string name, string address, int maximumCapacity)
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Libraries", connectionString);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                DataTable table = new DataTable();
                adapter.Fill(table);
 
               
                DataRow row = table.NewRow();
                row["Name"] = name;
                row["Address"] = address;
                row["MaximumCapacity"] = maximumCapacity;
                table.Rows.Add(row);
 
                adapter.Update(table);
                Console.WriteLine("Library added to the database successfully!");
            }
 
            catch(Exception)
            {
                Console.WriteLine("Database connection failed.");
 
            }
        }
 
        public static void ListLibrariesFromDB()
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Libraries", connectionString);
                DataTable table = new DataTable();
                adapter.Fill(table);
 
                foreach(DataRow row in table.Rows)
                {
                    Console.WriteLine($"Library ID: {row["Id"]}, Name: {row["Name"]}, Address: {row["Address"]}, Capacity: {row["MaximumCapacity"]}");
                }
                   
            }
           
            catch(Exception)
            {
                Console.WriteLine("Database connection failed.");
            }
        }
 
 
    }
}
 
 
namespace dotnetapp.Managers
{
    public interface IBookManager // Interface for DIP. Any method declared here but me implemented or defined in any class that implements this interface.
    {
        void AddBook(int libraryId);
        void ListBooks();
        void FindBook(string bookTitle);
        void EditBook();
        void DeleteBook();
        void AddBookToDB(int libraryId);
        void ListBooksFromDB();
        void EditBookInDB();
        void DeleteBookFromDB();
 
    }
}
 
namespace dotnetapp.Managers
{
    public interface ILibraryManager  // Two interfaces for Interface Segregation Principle
    {
        void AddLibrary();
        void ListLibraries();
        void AddLibraryToDB();
        void ListLibrariesFromDB();
       
    }
}
 
