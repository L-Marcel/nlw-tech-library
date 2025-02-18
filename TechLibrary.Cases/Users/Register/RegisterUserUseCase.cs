using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;

namespace TechLibrary.Cases.Users.Register;

public class RegisterUserUseCase {
    public ResponseRegisteredUserJson Execute(RequestUserJson request) {
        return new ResponseRegisteredUserJson();
    }
}