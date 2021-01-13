using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TestTaskForSimbirSoft.Controllers;
using TestTaskForSimbirSoft.Infrastructure;
using TestTaskForSimbirSoft.Models;
using Xunit;

namespace TestTaskForSimbirSoft.Test
{
    public class PageControllerTests
    {
        private Mock<IPageRepository> mockPage;
        private Mock<IWordRepository> mockWord;
        private Mock<IWordsetHandler> mockWordsetHandler;
        private Mock<IWebHostEnvironment> mockWebHostEnvironment; 
        
        public PageControllerTests()
        {
            mockPage = new Mock<IPageRepository>();
            mockWord = new Mock<IWordRepository>();
            mockWordsetHandler = new Mock<IWordsetHandler>();
            mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
        }

        [Fact]
        public void IndexViewNameEqualIndex()
        {
            // Arrange
            var controller = new PageController(mockWordsetHandler.Object, mockWord.Object, mockPage.Object, mockWebHostEnvironment.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.Equal("Page", result?.ViewName);
            Assert.NotNull(result);
        }

        [Fact]
        public void IndexReturnsAViewResultWithAListOfPages()
        {
            // Arrange
            mockPage.Setup(rp => rp.Pages).Returns((IEnumerable<Page>)GetTestPages());
            var controller = new PageController(mockWordsetHandler.Object, mockWord.Object, mockPage.Object, mockWebHostEnvironment.Object);

            //Act
            var result = controller.Index() as ViewResult;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Page>>(viewResult.Model);
            Assert.Equal(GetTestPages().Count, model.Count());
            Assert.Equal("Page", result.ViewName);
        }

        private List<Page> GetTestPages()
        {
            var pages = new List<Page>
            {
                new Page {Id = 1, PageAddress="https://www.simbirsoft.com", CountWords=600},
                new Page {Id = 2, PageAddress="https://www.mvideo.ru", CountWords=1000},
                new Page {Id = 3, PageAddress="https://www.aliexpress.com/", CountWords=1700},
            };
            return pages;
        }

        private List<Word> GetTestWords()
        {
            var words = new List<Word>
            {
                new Word {Id = 1, PageId=1, WordCount=2, WordName="Тест"},
                new Word {Id = 2, PageId=1, WordCount=3, WordName="Метод"},
                new Word {Id = 3, PageId=2, WordCount=4, WordName="Класс"},
                new Word {Id = 4, PageId=2, WordCount=5, WordName="Интерфейс"},
                new Word {Id = 5, PageId=3, WordCount=6, WordName="Переменная"},
                new Word {Id = 6, PageId=3, WordCount=7, WordName="Свойство"},
            };
            return words;
        }
    }
}