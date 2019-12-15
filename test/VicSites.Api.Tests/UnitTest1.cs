using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using VicSites.Api.Controllers;
using VicSites.Business.Definition;
using Xunit;

namespace VicSites.Api.Tests
{
    public class UnitTest1
    {

        private readonly Mock<IMainComponent> _mockMainComponent;
        private readonly Mock<ILogger<MainController>> _mockLogger;

        public UnitTest1()
        {
            _mockMainComponent = new Mock<IMainComponent>();
            _mockLogger = new Mock<ILogger<MainController>>();
        }

        [Fact]
        public async Task Test1()
        {
            var controller = new MainController(_mockLogger.Object, _mockMainComponent.Object);
            _mockMainComponent.Setup(e => e.GetNumberOfVisits()).ReturnsAsync(1);
            var result = await controller.GetNumberOfVisits();
            Assert.IsType<OkObjectResult>(result);


        }
    }
}
