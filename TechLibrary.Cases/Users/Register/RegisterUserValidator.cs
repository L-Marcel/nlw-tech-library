using FluentValidation;
using TechLibrary.Communication.Requests;

namespace TechLibrary.Cases.Users.Register;

public class RegisterUserValidator : AbstractValidator<RequestUserJson> {
    public RegisterUserValidator() {
        this.RuleFor(request => request.Name)
            .NotEmpty().WithMessage("Name can't be empty");
        this.RuleFor(request => request.Email)
            .EmailAddress()
            .WithMessage("Email is invalid");
        this.RuleFor(request => request.Password)
            .NotEmpty().WithMessage("Password can't be empty")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters");
    }
}