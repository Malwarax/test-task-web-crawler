using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;

namespace WebCrawler.Logic.Tests
{
    public class WebsiteCrawlerTests
    {
        private Mock<PageDownloader> pageDownloaderMock;
        private Mock<PageParser> pageParserMock;

        public WebsiteCrawlerTests()
        {
            pageDownloaderMock = new Mock<PageDownloader>();
            pageParserMock = new Mock<PageParser>();
        }

        [Fact]
        public void Crawl_WithOneHtmlPage_ShouldReturnTwoLinks()
        {
            //Arrange
            pageParserMock.Setup(p => p.GetLinks(It.IsAny<string>(), It.IsAny<Uri>()))
                .Returns(new List<Uri>() { new Uri("https://www.example.com/example1/"), new Uri("https://www.example.com/example2/") });
            var websiteCrawler = new WebsiteCrawler();

            //Act
            var result = websiteCrawler.Crawl(new Uri("https://www.example.com/"), pageDownloaderMock.Object, pageParserMock.Object);

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
            pageParserMock.SetupSequence(p => p.GetLinks(It.IsAny<string>(), It.IsAny<Uri>()))
                .Returns(parserResult1)
                .Returns(parserResult1)
                .Returns(parserResult2)
                .Returns(parserResult2);
            var websiteCrawler = new WebsiteCrawler();

            //Act
            var result = websiteCrawler.Crawl(new Uri("https://www.example.com/"), pageDownloaderMock.Object, pageParserMock.Object);

            //Assert
            Assert.Equal(4, result.Count);
            Assert.Contains(new Uri("https://www.example.com/"), result);
            Assert.Contains(new Uri("https://www.example.com/example1/"), result);
            Assert.Contains(new Uri("https://www.example.com/example2/"), result);
            Assert.Contains(new Uri("https://www.example.com/example3/"), result);
        }
    }
}
