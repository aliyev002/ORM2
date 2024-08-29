using Dapper;
using ORM2.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM2.Data
{
    internal class Database
    {
        public SqlConnection connection;
        public Database(string connectionString)
        {
            connection = new SqlConnection(connectionString);
        }
        public List<Book> getAllBooks()
        {
            var query = "Select * From Books Where Quantity>0";
            var books = connection.Query<Book>(query).ToList();
            return books;
        }
        public int TakeBook(int idStudent, int idBook)
        {
            var query = "Select Max(Id) From S_Cards";
            var lastId = connection.ExecuteScalar<int>(query);

            var cmd = "Insert Into S_Cards(Id, Id_Student, Id_Book, DateOut, Id_Lib) Values (@Id, @IdStudent, @IdBook, @DateOut, @IdLib)";

            var result = connection.Execute(cmd, param: new { Id = lastId + 1, IdBook = idBook, IdStudent = idStudent, DateOut = DateTime.Now, IdLib = 1 });


            return result;
        }
        public List<Book> takenBooks(int IdStudent)
        {
            var sql = @"SELECT b.Id, b.Name FROM Books b JOIN S_Cards s ON b.Id = s.Id_Book 
            WHERE s.Id_Student = @StudentId AND s.DateIn IS NULL";
            
            var takenBooks = connection.Query<Book>(sql, new { StudentId = IdStudent }).ToList();
            return takenBooks;

        }
        public void returnBook(int IdStudent, int IdBook)
        {
            var command = "UPDATE S_Cards SET DateIn = GETDATE() WHERE Id_Student = @IdStudent AND Id_Book = @IdBook";
            int affectedRows = connection.Execute(command, new { IdStudent, IdBook });

            if (affectedRows > 0)
            {
                Console.WriteLine("Book return date updated successfully.");
            }
            else
            {
                Console.WriteLine("No matching record found to return the book.");
            }
        }

        public Student? Login(int studentId)
        {
            var query = "Select * From Students Where Id=@studentId";
            var student = connection.QueryFirstOrDefault<Student>(query);
            return student;
        }
        public void Dispose()
        {
            connection.Dispose();
            GC.SuppressFinalize(this);
        }
        ~Database()
        {
            Dispose();
        }
    }
}
