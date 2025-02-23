namespace BookManagement.Models.Models;

public class Book
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    public int PublicationYear { get; set; }
    public string AuthorName { get; set; }
    public int ViewsCount { get; set; }
    public bool IsDeleted { get; set; } = false;
}