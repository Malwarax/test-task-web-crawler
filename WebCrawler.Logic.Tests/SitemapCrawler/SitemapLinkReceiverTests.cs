using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;

namespace WebCrawler.Logic.Tests
{
    public class SitemapLinkReceiverTests
    {
        private SitemapLinkReceiver receiver;
        private Mock<PageDownloader> downloaderMock;

        public SitemapLinkReceiverTests()
        {
            receiver = new SitemapLinkReceiver();
            downloaderMock = new Mock<PageDownloader>();
        }

        [Fact]
        public void GetSitemapLink_WithLinkInRobots_ShouldReturnLink()
        {
            //Arrange
            var expected = new Uri("https://www.example.com/sitemap-index.xml");
            var robots = "line1\nline2\nSitemap: https://www.example.com/sitemap-index.xml";
            downloaderMock.Setup(d => d.GetPage(It.IsAny<Uri>())).Returns(robots);

            //Act
            var result = receiver.GetSitemapUri(new Uri("https://www.example.com/"),downloaderMock.Object);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetSitemapLink_WithoutLinkInRobots_ShouldReturnDefaultLink()
        {
            //Arrange
            var expected = new Uri("https://www.example.com/sitemap.xml");
            var robots = "robots";
            downloaderMock.Setup(d => d.GetPage(It.IsAny<Uri>())).Returns(robots);

            //Act
            var result = receiver.GetSitemapUri(new Uri("https://www.example.com/"), downloaderMock.Object);

            //Assert
            Assert.Equal(expected, result);
        }
    }
}
