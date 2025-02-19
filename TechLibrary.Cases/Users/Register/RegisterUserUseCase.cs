using FluentValidation.Results;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Domain.Entities;
using TechLibrary.Exceptions;
using TechLibrary.Infrastructure;
using TechLibrary.Infrastructure.DataAccess;
using TechLibrary.Infrastructure.Security;

namespace TechLibrary.Cases.Users.Register;



public class RegisterUserUseCase {
    public ResponseRegisteredUserJson Execute(RequestUserJson request) {
        TechLibraryDbContext context = new TechLibraryDbContext();
        this.Validate(request, context);

        User user = new User {
            Name = request.Name,
            Email = request.Email,
            Password = Cryptography.HashPassword(request.Password),
        };
        
        context.Users.Add(user);
        context.SaveChanges();
        
        return new ResponseRegisteredUserJson {
            Name = request.Name,
            AccessToken = AccessToken.Generate(user)
        };
    }

    private void Validate(RequestUserJson request, TechLibraryDbContext context) {
        RegisterUserValidator validator = new();
        ValidationResult? result = validator.Validate(request);
        
        bool emailAlreadyExists = context.Users.Any(user => user.Email.Equals(request.Email));
        if(emailAlreadyExists) result.Errors.Add(new ValidationFailure("Email", "E-mail already in use"));
        
        if(result.IsValid) return;
        List<string> errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
        throw new ValidationException(errorMessages);
    }
}