namespace EnqueteOnline.Application.Pagination
{
    public record PaginationRequest(int PageNumber = 1, int PageSize = 6);
}
