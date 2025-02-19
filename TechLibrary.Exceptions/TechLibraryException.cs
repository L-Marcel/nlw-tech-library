using System.Net;

namespace TechLibrary.Exceptions;

public abstract class TechLibraryException : SystemException {
    private readonly List<string> _messages;

    protected TechLibraryException(List<string> messages) {
        this._messages = messages;
    }

    public List<string> GetErrorMessages() {
        return this._messages;
    }

    public abstract HttpStatusCode GetStatusCode();
}