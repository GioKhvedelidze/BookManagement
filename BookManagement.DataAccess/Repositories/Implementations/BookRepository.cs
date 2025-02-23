using BookManagement.DataAccess.Data;
using BookManagement.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.DataAccess.Repositories.Implementations;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _dbContext;

    public BookRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Book> GetBookByIdAsync(Guid id)
    {
        var book = await _dbContext.Books.FindAsync(id);
        if (book != null)
        {
            book.ViewsCount++; 
            await _dbContext.SaveChangesAsync();
        }
        
        return book;
    }

    public async Task AddBookAsync(Book book)
    {
        _dbContext.Books.Add(book);
        await _dbContext.SaveChangesAsync();
    }
    

    public async Task UpdateBookAsync(Book book)
    {
        _dbContext.Entry(book).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task SoftDeleteBookAsync(Guid id)
    {
        var book = await _dbContext.Books.FindAsync(id);
        if (book != null)
        {
            book.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task AddBooksAsync(IEnumerable<Book> books)
    {
        await _dbContext.Books.AddRangeAsync(books);
        await _dbContext.SaveChangesAsync();
    }

    public async Task SoftDeleteBooksAsync(IEnumerable<Guid> ids)
    {
        var books = await _dbContext.Books.Where(b => ids.Contains(b.Id)).ToListAsync();
        foreach (var book in books)
        {
            book.IsDeleted = true;
        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Book>> GetBooksAsync()
    {
        return await _dbContext.Books.Where(b => !b.IsDeleted).ToListAsync();
    }

    public async Task<bool> BookExistsAsync(string title)
    {
        return await _dbContext.Books.AnyAsync(b => b.Title == title);
    }

    public async Task<IEnumerable<Book>> GetPopularBooksAsync(int pageNumber, int pageSize)
    {
        var books = await _dbContext.Books
            .Where(b => !b.IsDeleted)
            .ToListAsync();

        var popularBooks = books
            .Select(b => new
            {
                Book = b,
                PopularityScore = CalculatePopularityScore(b.ViewsCount, b.PublicationYear)
            })
            .OrderByDescending(b => b.PopularityScore)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(b => b.Book);

        return popularBooks;
    }

    public double CalculatePopularityScore(int viewsCount, int publicationYear)
    {
        int yearsSincePublished = DateTime.Now.Year - publicationYear;
        return viewsCount * 0.5 + yearsSincePublished * 2;
    }
}