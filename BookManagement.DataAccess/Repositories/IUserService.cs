namespace BookManagement.DataAccess.Repositories;

public interface IUserService
{
    Task<string> RegisterAsync(string email, string password);
    Task<string> LoginAsync(string email, string password);
}