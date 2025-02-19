using Microsoft.AspNetCore.Http;
using TechLibrary.Domain.Entities;
using TechLibrary.Exceptions;
using TechLibrary.Infrastructure.DataAccess;
using TechLibrary.Services;

namespace TechLibrary.Cases.Checkouts.Register;

public class RegisterBookCheckoutUseCase {
    private const int MaxLoanDays = 7;
    
    public void Execute(Guid bookId, HttpContext httpContext) {
        TechLibraryDbContext context = new TechLibraryDbContext();
        LoggedUserService loggedUserService = new LoggedUserService(httpContext);
        User user = loggedUserService.GetLoggedUser(context);
        
        this.Validate(bookId, context);
        
        context.Checkouts.Add(new Checkout {
            BookId = bookId,
            UserId = user.Id,
            ExpectedReturnDate = DateTime.UtcNow.AddDays(MaxLoanDays),
        });
        context.SaveChanges();
    }

    private void Validate(Guid bookId, TechLibraryDbContext context) {
        Book? book = context.Books.FirstOrDefault(book => book.Id == bookId);
        if(book is null) throw new NotFoundException("Book not found");
        
        int checkoutCount = context.Checkouts.Count(checkout => checkout.BookId == bookId && checkout.ReturnedDate == null);
        if(checkoutCount >= book.Amount) throw new LockedException("No copies of the book available to loan");
    }
}