using System.Threading.Tasks;

namespace WebCrawler.Services.Interfaces
{
    public interface ICrawlerService
    {
        Task<int> Crawl(string url);
    }
}
