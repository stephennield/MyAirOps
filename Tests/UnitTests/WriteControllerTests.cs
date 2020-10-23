using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyAirOpsLogger.Controllers;
using MyAirOpsLogger.Logging;
using MyAirOpsLogger.Models;
using System;

namespace UnitTests
{
    [TestClass]
    public class WriteControllerTests
    {
        [TestMethod]
        public void POSTWritesFormattedMessageToLog()
        {
            var logSystem = new Mock<ILogger<FileSystemLoggingCategory>>();
            var exceptionLogger = new Mock<ILogger<WriteController>>();

            var message =
                new Message()
                {
                    Id = 999,
                    Date = new DateTime(2020, 10, 23, 12, 05, 59),
                    Content = "this is a test"
                };

            var logMessage = "Message contains Id: 999, Date: 23/10/2020 12:05:59, Content: this is a test";

            logSystem
                .Setup(ls =>
                    ls.Log(
                        LogLevel.Information,
                        It.IsAny<EventId>(),
                        It.Is<It.IsAnyType>((o, _) => o.ToString() == logMessage),
                        It.IsAny<Exception>(),
                        It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)));            

            var controller =
                new WriteController(
                    logSystem.Object,
                    exceptionLogger.Object);

            var result = controller.Post(message);

            Assert.AreEqual(200, (result as StatusCodeResult).StatusCode);

            logSystem.VerifyAll();
        }
    }
}
