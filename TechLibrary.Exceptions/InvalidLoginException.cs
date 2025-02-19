using System.Net;

namespace TechLibrary.Exceptions;

public class InvalidLoginException : TechLibraryException {
    public InvalidLoginException() : base(["Invalid e-mail or password."]) { }

    public override HttpStatusCode GetStatusCode() {
        return HttpStatusCode.Unauthorized;
    }
}