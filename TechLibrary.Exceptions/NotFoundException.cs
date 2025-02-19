using System.Net;

namespace TechLibrary.Exceptions;

public class NotFoundException : TechLibraryException{
    public NotFoundException(string message) : base([message]) { }

    public override HttpStatusCode GetStatusCode() {
        return HttpStatusCode.NotFound;
    }
}