using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;

namespace WebCrawler.Logic.Tests
{
    public class SitemapLinkReceiverTests
    {
        private readonly SitemapLinkReceiver _receiver;

        public SitemapLinkReceiverTests()
        {
            _receiver = new SitemapLinkReceiver();
        }

        [Fact]
        public void GetSitemapLink_WithLinkInRobots_ShouldReturnLink()
        {
            //Arrange
            var expected = new Uri("https://www.example.com/sitemap-index.xml");
            var robots = "line1\nline2\nSitemap: https://www.example.com/sitemap-index.xml";

            //Act
            var result = _receiver.GetSitemapUri(new Uri("https://www.example.com/"),robots);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetSitemapLink_WithoutLinkInRobots_ShouldReturnDefaultLink()
        {
            //Arrange
            var expected = new Uri("https://www.example.com/sitemap.xml");
            var robots = "robots";

            //Act
            var result = _receiver.GetSitemapUri(new Uri("https://www.example.com/"), robots);

            //Assert
            Assert.Equal(expected, result);
        }
    }
}
