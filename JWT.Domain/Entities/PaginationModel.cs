using System;

namespace JWT.Domain.Entities
{
    public class PaginationModel
    {
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 10;

        public int TotalPages => (int) Math.Ceiling(decimal.Divide(Count, PageSize));
    }
}
