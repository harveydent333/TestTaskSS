using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using TestTaskForSimbirSoft.Controllers;
using Xunit;


namespace TestTaskForSimbirSoft.Test
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexViewNameEqualIndex()
        {
            // Arrange
            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            tempData["Message"] = "";
            HomeController controller = new HomeController()
            {
                TempData = tempData
            };

            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.Equal("Index", result?.ViewName);
            Assert.NotNull(result);
        }

        [Fact]
        public void IndexCorrectlyPassesViewBag()
        {
            // Arrange
            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            tempData["Message"] = "Некорректный адрес страницы.";
            HomeController controller = new HomeController()
            {
                TempData = tempData
            };

            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.Equal(tempData["Message"], result?.ViewData["Message"]);
        }
    }
}
