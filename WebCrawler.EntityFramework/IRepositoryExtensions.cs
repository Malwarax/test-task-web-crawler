using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCrawler.Data;

namespace WebCrawler.EntityFramework
{
    public static class IRepositoryExtensions
    {
        public static Website GetByUrl(this IRepository<Website> repository, string url)
        {
            var website = repository.GetAll().Where(website => website.WebsiteLink == url).Include(w=>w.PerformanceResults).FirstOrDefault();
            return website;
        }
    }
}
