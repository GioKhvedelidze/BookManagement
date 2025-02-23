using BookManagement.Models.Models;
using FluentValidation;

namespace BookManagement.Models.Validators;

public class BookValidator : AbstractValidator<Book>
{
    public BookValidator()
    {
        RuleFor(book => book.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");
        
        RuleFor(book => book.PublicationYear)
            .NotEmpty().WithMessage("Publication year is required.")
            .InclusiveBetween(1800, DateTime.Now.Year)
            .WithMessage($"Publication year must be between 1800 and {DateTime.Now.Year}.");
        
        RuleFor(book => book.AuthorName)
            .NotEmpty().WithMessage("Author name is required.")
            .MaximumLength(50).WithMessage("Author name cannot exceed 50 characters.");
        
        RuleFor(book => book.ViewsCount)
            .GreaterThanOrEqualTo(0).WithMessage("Views count cannot be negative.");
    }
}