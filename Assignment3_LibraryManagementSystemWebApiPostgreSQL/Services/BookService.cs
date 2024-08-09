using Assignment3_LibraryManagementSystemWebApiPostgreSQL.Interfaces;
using Assignment3_LibraryManagementSystemWebApiPostgreSQL.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment3_LibraryManagementSystemWebApiPostgreSQL.Services
{
    public class BookService:IBook
    {
        private readonly LibraryContext _context;

        public BookService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<bool> AddBook(Book book)
        {
            var existingBook = await _context.Books.FirstOrDefaultAsync(cek => cek.Isbn == book.Isbn || cek.Title == book.Title);
            if (existingBook != null)
            {
                return false;
            }
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book?> GetBookById(int id)
        {
            var existingBook = await _context.Books.FirstOrDefaultAsync(cek=>cek.Id == id);
            if(existingBook == null)
            {
                return null;
            }
            return await _context.Books.FindAsync(id);
        }

        public async Task<bool> UpdateBook(int id, Book book)
        {
            var existingBook = await _context.Books.FindAsync(id);
            if (existingBook == null)
            {
                return false;
            }

            var duplicateBook = await _context.Books.AnyAsync(b => b.Isbn == book.Isbn && b.Id != id);
            if (duplicateBook)
            {
                return false;
            }

            var duplicateTitle = await _context.Books.AnyAsync(b => b.Title == book.Title && b.Id != id);
            if (duplicateTitle)
            {
                return false;
            }

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Publicationyear = book.Publicationyear;
            existingBook.Isbn = book.Isbn;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return false;
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
