using Microsoft.EntityFrameworkCore;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Domain.Entities;
using TechLibrary.Infrastructure;
using TechLibrary.Infrastructure.DataAccess;

namespace TechLibrary.Cases.Books.Filter;

public class FilterBookUseCase {
    public ResponseBooksJson Execute(RequestFilterBookJson request) {
        TechLibraryDbContext context = new TechLibraryDbContext();

        IQueryable<Book> query = context.Books;
        int totalCount = query.Count();

        if(!string.IsNullOrWhiteSpace(request.Title)) {
            query = query.Where(book => book.Title.ToLower().Contains(request.Title.ToLower()));
            totalCount = query.Count();
        }

        List<Book> books = query
            .OrderBy(book => book.Title)
            .ThenBy(book => book.Author)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();

        return new ResponseBooksJson {
            Pagination = new ResponsePaginationJson {
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
            },
            Books = books.Select(book => new ResponseBookJson {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author
            }).ToList()
        };
    }
}