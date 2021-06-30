using System.Linq;

namespace WebCrawler.Services.Extensions
{
    static class PaginationExtension
    {
        public static IQueryable<T> GetPagination<T>(this IQueryable<T> querry, int page, int pageSize)
        {
            return querry.Skip((page - 1) * pageSize)
                    .Take(pageSize);
        }
    }
}
