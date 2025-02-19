using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Domain.Entities;
using TechLibrary.Exceptions;
using TechLibrary.Infrastructure;
using TechLibrary.Infrastructure.DataAccess;
using TechLibrary.Infrastructure.Security;

namespace TechLibrary.Cases.Login;

public class DoLoginUseCase {
    public ResponseRegisteredUserJson Execute(RequestLoginJson request) {
        TechLibraryDbContext context = new TechLibraryDbContext();
        User? user = context.Users.FirstOrDefault(user => user.Email.Equals(request.Email));
        if(user?.Password != null && Cryptography.VerifyPassword(request.Password, user.Password)) {
            return new ResponseRegisteredUserJson {
                Name = user.Name,
                AccessToken = AccessToken.Generate(user)
            };
        }

        throw new InvalidLoginException();
    }
}