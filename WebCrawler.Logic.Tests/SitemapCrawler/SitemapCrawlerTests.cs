using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;

namespace WebCrawler.Logic.Tests
{
    public class SitemapCrawlerTests
    {
        private Mock<SitemapLinkReceiver> linkReceiverMock;
        private Mock<PageDownloader> pageDownloaderMock;
        private Mock<SitemapParser> sitemapParserMock;

        public SitemapCrawlerTests()
        {
            linkReceiverMock = new Mock<SitemapLinkReceiver>();
            pageDownloaderMock = new Mock<PageDownloader>();
            sitemapParserMock = new Mock<SitemapParser>();
        }

        [Fact]
        public void Crawl_ShouldSupportCommonSitemap()
        {
            //Arrange
            var expected = new List<Uri>() { new Uri("https://www.example1.com/"), new Uri("https://www.example2.com/") };
            sitemapParserMock.Setup(s => s.GetLinks(It.IsAny<string>(), It.IsAny<Uri>())).Returns(expected);
            var sitemapCrawler = new SitemapCrawler();

            //Act
            List<Uri> result = sitemapCrawler.Crawl(It.IsAny<Uri>(), linkReceiverMock.Object, pageDownloaderMock.Object, sitemapParserMock.Object);

            //Assert
            Assert.Equal(expected, result);
        }
    }
}
