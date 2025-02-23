using System.Text.Json;
using BookManagement.DataAccess.Repositories;
using BookManagement.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookRepository _bookRepo;

    public BooksController(IBookRepository bookRepo)
    {
        _bookRepo = bookRepo;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        var books = await _bookRepo.GetBooksAsync();
        return Ok(books);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Book>> GetBookById(Guid id)
    {
        var book = await _bookRepo.GetBookByIdAsync(id);
        if (book == null || book.IsDeleted)
        {
            return NotFound();
        }
        return Ok(book);
    }
    
    [HttpGet("popular")]
    public async Task<IActionResult> GetPopularBooks(int pageNumber = 1, int pageSize = 10)
    {
        var books = await _bookRepo.GetPopularBooksAsync(pageNumber, pageSize);
        return Ok(books);
    }
    
    [HttpPost]
    public async Task<ActionResult<Book>> AddBook(Book book)
    {
        await _bookRepo.AddBookAsync(book);
        return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
    }
    
    [HttpPost("bulk")]
    public async Task<ActionResult> AddBooks(List<Book> books)
    {
        // Check for duplicates in the input list
        var duplicateTitles = books
            .GroupBy(b => b.Title)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        if (duplicateTitles.Any())
        {
            return BadRequest($"Duplicate titles found in the request: {string.Join(", ", duplicateTitles)}");
        }

        // Check if any of the books already exist in the database
        var existingTitles = new List<string>();
        foreach (var book in books)
        {
            if (await _bookRepo.BookExistsAsync(book.Title))
            {
                existingTitles.Add(book.Title);
            }
        }

        if (existingTitles.Any())
        {
            return BadRequest($"Books with the following titles already exist: {string.Join(", ", existingTitles)}");
        }

        await _bookRepo.AddBooksAsync(books);
        return Ok();
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateBook(Guid id, Book book)
    {
        if (id != book.Id)
        {
            return BadRequest();
        }

        await _bookRepo.UpdateBookAsync(book);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> SoftDeleteBook(Guid id)
    {
        await _bookRepo.SoftDeleteBookAsync(id);
        return NoContent();
    }
    
    [HttpDelete("bulk")]
    public async Task<IActionResult> SoftDeleteBooks(List<Guid> ids)
    {
        await _bookRepo.SoftDeleteBooksAsync(ids);
        return NoContent();
    }
}