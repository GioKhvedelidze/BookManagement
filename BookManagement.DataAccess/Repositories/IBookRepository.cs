using BookManagement.Models.Models;

namespace BookManagement.DataAccess.Repositories;

public interface IBookRepository
{
    Task<Book> GetBookByIdAsync(Guid id);
    Task AddBookAsync(Book book);
    Task AddBooksAsync(IEnumerable<Book> books);
    Task UpdateBookAsync(Book book);
    Task SoftDeleteBookAsync(Guid id);
    Task SoftDeleteBooksAsync(IEnumerable<Guid> ids);
    Task<IEnumerable<Book>> GetBooksAsync();
    Task<bool> BookExistsAsync(string title);
    Task<IEnumerable<Book>> GetPopularBooksAsync(int pageNumber, int pageSize);
    double CalculatePopularityScore(int viewsCount, int publicationYear);
}