using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;

namespace WebCrawler.Logic.Tests
{
    public class SitemapParserTests
    {
        private readonly SitemapParser _parser;
        public SitemapParserTests()
        {
            _parser = new SitemapParser();
        }

        [Fact]
        public void GetLinks_ShouldSupportAbsoluteLinks()
        {
            //Arrange
            string document = System.IO.File.ReadAllText("Samples/xml1.txt");

            //Act
            List<Uri> result = _parser.GetLinks(document, new Uri("https://wwww.example.com/"));

            //Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(new Uri("https://www.example.com/example1/"), result);
            Assert.Contains(new Uri("https://www.example.com/example2/"), result);
        }

        [Fact]
        public void GetLinks_ShouldSupportRelativeLinks()
        {
            //Arrange
            string document = System.IO.File.ReadAllText("Samples/xml2.txt");

            //Act
            List<Uri> result = _parser.GetLinks(document, new Uri("https://www.example.com/"));

            //Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(new Uri("https://www.example.com/example1/"), result);
            Assert.Contains(new Uri("https://www.example.com/example2/"), result);
        }
    }
}
