namespace TechLibrary.Communication.Responses;

public class ResponseBooksJson {
    public ResponsePaginationJson? Pagination { get; set; }
    public List<ResponseBookJson> Books { get; set; } = [];
}