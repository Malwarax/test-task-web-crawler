using System;
using System.Linq;

namespace WebCrawler.Services.Extensions
{
    static class PaginationExtension
    {
        public static IQueryable<T> GetPagination<T>(this IQueryable<T> querry, int page, int pageSize)
        {
            if (page < 0 || pageSize < 0)
            {
                throw new ArgumentOutOfRangeException("Page number or page size can't be negative");
            }
            else if(page != 0 && pageSize != 0)
            {
                querry = querry.Skip((page - 1) * pageSize)
                .Take(pageSize);
            }

            return querry;
        }
    }
}
