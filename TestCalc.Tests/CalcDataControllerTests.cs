using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestCalc.Controllers;
using Xunit;

namespace TestCalc.Tests
{
    public class CalcDataControllerTests
    {
        [Fact]
        public void CheckSum()
        {
            var mockRepo = new Mock<ILogger<CalcDataController>>();
            CalcDataController calcDataController = new CalcDataController(mockRepo.Object);
            OkObjectResult result = calcDataController.Get("4", "5", "+") as OkObjectResult;
            Assert.Equal(9d, result?.Value);
        }
        [Fact]
        public void CheckSubtraction()
        {
            var mockRepo = new Mock<ILogger<CalcDataController>>();
            CalcDataController calcDataController = new CalcDataController(mockRepo.Object);
            OkObjectResult result = calcDataController.Get("4", "5", "-") as OkObjectResult;
            Assert.Equal(-1d, result?.Value);
        }
        [Fact]
        public void CheckMultiplication()
        {
            var mockRepo = new Mock<ILogger<CalcDataController>>();
            CalcDataController calcDataController = new CalcDataController(mockRepo.Object);
            OkObjectResult result = calcDataController.Get("9", "5", "*") as OkObjectResult;
            Assert.Equal(45d, result?.Value);
            
        }
        [Fact]
        public void CheckDiv()
        {
            var mockRepo = new Mock<ILogger<CalcDataController>>();
            CalcDataController calcDataController = new CalcDataController(mockRepo.Object);
            OkObjectResult result = calcDataController.Get("10", "5", "div") as OkObjectResult;
            Assert.Equal(2d, result?.Value);
        }
        [Fact]
        public void CheckError()
        {
            var mockRepo = new Mock<ILogger<CalcDataController>>();
            CalcDataController calcDataController = new CalcDataController(mockRepo.Object);
            calcDataController.ModelState.AddModelError("Title", "Required");
            var result = calcDataController.Get("10dddd", "5", "ddd");
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task CheckReturnsUnauthorizedResult()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            var client = server.CreateClient();
            var url = "CalcData";
            var expected = HttpStatusCode.Unauthorized;
            var response = await client.GetAsync(url);
            Assert.Equal(expected, response.StatusCode);
        }

    }
}
