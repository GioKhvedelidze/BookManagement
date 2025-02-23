using BookManagement.Models.Models.Dto;
using FluentValidation;

namespace BookManagement.Models.Validators;

public class UserRegistrationRequestDtoValidator : AbstractValidator<UserRegistrationRequestDto>
{
    public UserRegistrationRequestDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.");
        
    }
}