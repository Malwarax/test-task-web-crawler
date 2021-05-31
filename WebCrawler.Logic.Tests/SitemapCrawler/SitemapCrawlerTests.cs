using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;

namespace WebCrawler.Logic.Tests
{
    public class SitemapCrawlerTests
    {
        private Mock<SitemapLinkReceiver> _linkReceiverMock;
        private Mock<PageDownloader> _pageDownloaderMock;
        private Mock<SitemapParser> _sitemapParserMock;

        public SitemapCrawlerTests()
        {
            _linkReceiverMock = new Mock<SitemapLinkReceiver>();
            _pageDownloaderMock = new Mock<PageDownloader>();
            _sitemapParserMock = new Mock<SitemapParser>();
        }

        [Fact]
        public void Crawl_ShouldSupportCommonSitemap()
        {
            //Arrange
            var expected = new List<Uri>() { new Uri("https://www.example1.com/"), new Uri("https://www.example2.com/") };
            _sitemapParserMock.Setup(s => s.GetLinks(It.IsAny<string>(), It.IsAny<Uri>())).Returns(expected);
            var sitemapCrawler = new SitemapCrawler(_pageDownloaderMock.Object, _linkReceiverMock.Object,  _sitemapParserMock.Object);

            //Act
            List<Uri> result = sitemapCrawler.Crawl(new Uri("https://www.example.com/"));

            //Assert
            Assert.Equal(expected, result);
        }
    }
}
