using BookManagement.Models.Models.Dto;
using FluentValidation;

namespace BookManagement.Models.Validators;

public class UserLoginRequestDtoValidator : AbstractValidator<UserLoginRequestDto>
{
    public UserLoginRequestDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}