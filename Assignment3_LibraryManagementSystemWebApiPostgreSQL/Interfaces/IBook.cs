using Assignment3_LibraryManagementSystemWebApiPostgreSQL.Models;

namespace Assignment3_LibraryManagementSystemWebApiPostgreSQL.Interfaces
{
    public interface IBook
    {
        Task<bool> AddBook(Book book);
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book?> GetBookById(int id);
        Task<bool> UpdateBook(int id, Book book);
        Task<bool> DeleteBook(int id);
    }
}
