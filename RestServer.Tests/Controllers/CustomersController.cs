using System;
using Xunit;
using RestServer.Controllers;
using Moq;
using RestServer.Services;
using RestServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace RestServer.Tests
{
    public class CustomersControllerTest
    {
        [Fact]
        public void TestGetEmptyContentShouldReturnEmptyJsonArray()
        {
            var expected = new Customer[]{};
            var cs = new Mock<ICustomersService>();
            cs.Setup(o => o.All())
                  .Returns(new Customer[]{});
            CustomersController ctrl = new CustomersController(cs.Object);
            var result = ctrl.Get() as JsonResult;

            cs.Verify(o => o.All(), Times.Once());
            Assert.Equal(expected, result.Value);
        }
    }
}
