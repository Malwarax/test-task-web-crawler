using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;

namespace WebCrawler.Logic.Tests
{
    public class WebsiteCrawlerTests
    {
        private readonly Mock<PageDownloader> _pageDownloaderMock;
        private readonly Mock<PageParser> _pageParserMock;
        private readonly WebsiteCrawler _websiteCrawler;
        public WebsiteCrawlerTests()
        {
            _pageDownloaderMock = new Mock<PageDownloader>();
            _pageParserMock = new Mock<PageParser>();
            _websiteCrawler = new WebsiteCrawler(_pageDownloaderMock.Object, _pageParserMock.Object);
        }

        [Fact]
        public void Crawl_WithOneHtmlPage_ShouldReturnTwoLinks()
        {
            //Arrange
            _pageParserMock.Setup(p => p.GetLinks(It.IsAny<string>(), It.IsAny<Uri>()))
                .Returns(new List<Uri>() { new Uri("https://www.example.com/example1/"), new Uri("https://www.example.com/example2/") });

            //Act
            var result = _websiteCrawler.Crawl(new Uri("https://www.example.com/"));

            //Assert
            Assert.Equal(3, result.Count);
            Assert.Contains(new Uri("https://www.example.com/"), result);
            Assert.Contains(new Uri("https://www.example.com/example1/"), result);
            Assert.Contains(new Uri("https://www.example.com/example2/"), result);
        }

        [Fact]
        public void Crawl_WithTwoHtmlPages_ShouldReturnAllLinks()
        {
            //Arrange
            var parserResult1 = new List<Uri>() { new Uri("https://www.example.com/example1/"), new Uri("https://www.example.com/example2/") };
            var parserResult2 = new List<Uri>() { new Uri("https://www.example.com/example3/")};
            _pageParserMock.SetupSequence(p => p.GetLinks(It.IsAny<string>(), It.IsAny<Uri>()))
                .Returns(parserResult1)
                .Returns(parserResult1)
                .Returns(parserResult2)
                .Returns(parserResult2);

            //Act
            var result = _websiteCrawler.Crawl(new Uri("https://www.example.com/"));

            //Assert
            Assert.Equal(4, result.Count);
            Assert.Contains(new Uri("https://www.example.com/"), result);
            Assert.Contains(new Uri("https://www.example.com/example1/"), result);
            Assert.Contains(new Uri("https://www.example.com/example2/"), result);
            Assert.Contains(new Uri("https://www.example.com/example3/"), result);
        }
    }
}
