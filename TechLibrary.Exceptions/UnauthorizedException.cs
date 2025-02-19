using System.Net;

namespace TechLibrary.Exceptions;

public class UnauthorizedException : TechLibraryException {
    public UnauthorizedException() : base(["Unauthorized"]) { }

    public override HttpStatusCode GetStatusCode() {
        return HttpStatusCode.Unauthorized;
    }
}