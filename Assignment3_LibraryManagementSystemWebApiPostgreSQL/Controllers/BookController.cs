using Asp.Versioning;
using Assignment3_LibraryManagementSystemWebApiPostgreSQL.Models;
using Assignment3_LibraryManagementSystemWebApiPostgreSQL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment3_LibraryManagementSystemWebApiPostgreSQL.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookController"/> class.
        /// </summary>
        /// <param name="bookService">The service used for book operations.</param>
        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Adds a new book to the collection.
        /// </summary>
        /// <remarks>
        /// The request body must contain the book details. Ensure that the ISBN and title are unique.
        /// 
        /// Sample request:
        /// 
        ///     POST /api/book
        ///     {
        ///         "title": "The Great Gatsby",
        ///         "author": "F. Scott Fitzgerald",
        ///         "publicationyear": 1925,
        ///         "isbn": "9780743273565"
        ///     }
        /// </remarks>
        /// <param name="book">The book details to be added.</param>
        /// <returns>
        /// This endpoint returns a message indicating the result of the operation.
        /// </returns>
        /// <response code="200">Book added successfully.</response>
        /// <response code="400">Invalid input data or a book with the same Name or ISBN already exists.</response>
        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest("Invalid input data. Please check the book details.");
            }

            var success = await _bookService.AddBook(book);
            if (!success)
            {
                return BadRequest("A book with the same Name or ISBN already exists.");
            }

            return Ok("Book added successfully.");
        }

        /// <summary>
        /// Retrieves all books from the collection.
        /// </summary>
        /// <remarks>
        /// This endpoint returns a list of all books.
        /// 
        /// Sample request:
        /// 
        ///     GET /api/book
        /// </remarks>
        /// <returns>
        /// A list of all books.
        /// </returns>
        /// <response code="200">A list of books.</response>
        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            var books = await _bookService.GetAllBooks();
            return Ok(books);
        }

        /// <summary>
        /// Retrieves a specific book by ID.
        /// </summary>
        /// <remarks>
        /// Ensure the ID is greater than zero. If the ID is invalid or the book does not exist, an appropriate message will be returned.
        /// 
        /// Sample request:
        /// 
        ///     GET /api/book/{id}
        /// </remarks>
        /// <param name="id">The ID of the book to retrieve.</param>
        /// <returns>
        /// The details of the requested book.
        /// </returns>
        /// <response code="200">The details of the requested book.</response>
        /// <response code="400">Invalid ID. The ID must be greater than zero.</response>
        /// <response code="404">Book with the specified ID was not found.</response>
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID. The ID must be greater than zero.");
            }

            var book = await _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound($"Book with ID {id} was not found.");
            }

            return Ok(book);
        }

        /// <summary>
        /// Updates an existing book.
        /// </summary>
        /// <remarks>
        /// The request body must contain the updated book details. Ensure that the ISBN and title are unique.
        /// 
        /// Sample request:
        /// 
        ///     PUT /api/book/{id}
        ///     {
        ///         "title": "The Great Gatsby - Updated",
        ///         "author": "F. Scott Fitzgerald",
        ///         "publicationyear": 1925,
        ///         "isbn": "9780743273565"
        ///     }
        /// </remarks>
        /// <param name="id">The ID of the book to update.</param>
        /// <param name="book">The updated book details.</param>
        /// <returns>
        /// This endpoint returns a message indicating the result of the operation.
        /// </returns>
        /// <response code="200">Book updated successfully.</response>
        /// <response code="400">Invalid input data or unable to update book. Title or ISBN might already exist.</response>
        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest("Invalid input data. Please check the book details.");
            }

            var success = await _bookService.UpdateBook(id, book);
            if (!success)
            {
                return BadRequest("Unable to update book. Title or ISBN might already exist.");
            }

            return Ok("Book updated successfully.");
        }

        /// <summary>
        /// Deletes a book by ID.
        /// </summary>
        /// <remarks>
        /// This endpoint removes a book from the collection based on the provided ID.
        /// 
        /// Sample request:
        /// 
        ///     DELETE /api/book/{id}
        /// </remarks>
        /// <param name="id">The ID of the book to delete.</param>
        /// <returns>
        /// This endpoint returns a message indicating the result of the operation.
        /// </returns>
        /// <response code="200">Book deleted successfully.</response>
        /// <response code="404">Book not found.</response>
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var success = await _bookService.DeleteBook(id);
            if (!success)
            {
                return NotFound("Book not found.");
            }

            return Ok("Book deleted successfully.");
        }
    }


}
