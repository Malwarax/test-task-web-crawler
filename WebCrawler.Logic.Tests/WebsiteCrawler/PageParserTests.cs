using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WebCrawler.Logic.Tests
{
    public class PageParserTests
    {
        private PageParser parser;

        public PageParserTests()
        {
            parser = new PageParser();
        }

        [Fact]
        public void GetLinks_ShouldReturnsAllUrlsFromHtmlDocument()
        {
            //Arrange
            string document = @"<body><a href=""https://www.example.com/example1/"">Link 1</a><a href=""https://www.example.com/example2/"">Link 2</a>";

            //Act
            List<Uri> result = parser.GetLinks(document, new Uri("https://www.example.com/"));

            //Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(new Uri("https://www.example.com/example1/"), result);
            Assert.Contains(new Uri("https://www.example.com/example2/"), result);
        }

        [Fact]
        public void GetLinks_ShouldSupportAbsoluteLinks()
        {
            //Arrange
            string document = @"<body><a href=""https://www.example.com/example1/"">Link</a>";

            //Act
            List<Uri> result = parser.GetLinks(document, new Uri("https://www.example.com/"));

            //Assert
            Assert.Contains(new Uri("https://www.example.com/example1/"), result);
        }

        [Fact]
        public void GetLinks_ShouldSupportRelativeLinks()
        {
            //Arrange
            string document = @"<body><a href=""/example1/"">Link</a>";

            //Act
            List<Uri> result = parser.GetLinks(document, new Uri("https://www.example.com/"));

            //Assert
            Assert.Contains(new Uri("https://www.example.com/example1/"), result);
        }

        [Fact]
        public void GetLinks_ShouldSupportLinksWithoutScheme()
        {
            //Arrange
            string document = @"<body><a href=""www.example.com/example1/"">Link</a>";

            //Act
            List<Uri> result = parser.GetLinks(document, new Uri("https://www.example.com/"));

            //Assert
            Assert.Contains(new Uri("https://www.example.com/example1/"), result);
        }
        
        [Fact]
        public void GetLinks_ShouldIgnoreAnotherDomainLinks()
        {
            //Arrange
            string document = @"<body><a href=""https://www.anotherexample.com/example1/"">Link</a>";

            //Act
            List<Uri> result = parser.GetLinks(document, new Uri("https://www.example.com/"));

            //Assert
            Assert.Empty(result);
        }
    }
}
