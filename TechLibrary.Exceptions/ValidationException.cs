using System.Net;

namespace TechLibrary.Exceptions;

public class ValidationException : TechLibraryException {
    public ValidationException(List<string> messages) : base(messages) { }

    public override HttpStatusCode GetStatusCode() {
        return HttpStatusCode.BadRequest;
    }
}