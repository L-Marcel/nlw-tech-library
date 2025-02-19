using System.Net;

namespace TechLibrary.Exceptions;

public class LockedException : TechLibraryException {
    public LockedException(string message) : base([message]) { }

    public override HttpStatusCode GetStatusCode() {
        return HttpStatusCode.Locked;
    }
}