using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ATNB.Service.Abstractions;
using Moq;
using ATNB.Web.Controllers;
using ATNB.Model;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ATNB.Test
{
    [TestClass]
    public class AirPortControllerTest
    {
        private Mock<IAirPortService> _airportServiceMock;
        AirPortsController objController;
        List<AirPort> listAirPort;

        [TestInitialize]
        public void Initialize()
        {
            _airportServiceMock = new Mock<IAirPortService>();
            objController = new AirPortsController(_airportServiceMock.Object);
            listAirPort = new List<AirPort>() {
               new AirPort() { Id = "AP00001", Name = "VN", RunwaySize = 12, MaxFWParkingPlace = 5, MaxRWParkingPlace = 10 },
               new AirPort() { Id = "AP00002", Name = "India", RunwaySize = 12, MaxFWParkingPlace = 5, MaxRWParkingPlace = 10 },
               new AirPort() { Id = "AP00003", Name = "Lao", RunwaySize = 12, MaxFWParkingPlace = 5, MaxRWParkingPlace = 10 },
              };
        }

        [TestMethod]
        public void AirPort_Get_All()
        {
            //Arrange
            _airportServiceMock.Setup(x => x.GetAll()).Returns(listAirPort);

            //Act
            var result = ((objController.Index("") as ViewResult).Model) as List<AirPort>;

            //Assert
            Assert.AreEqual(result.Count, 3);
            Assert.AreEqual("VN", result[0].Name);
            Assert.AreEqual("India", result[1].Name);
            Assert.AreEqual("Lao", result[2].Name);
        }

        [TestMethod]
        public void Valid_AirPort_Create()
        {
            //Arrange
            AirPort c = new AirPort() { Id = "AP00001", Name = "VN", RunwaySize = 12, MaxFWParkingPlace = 5, MaxRWParkingPlace = 10 };

            //Act
            var result = (RedirectToRouteResult)objController.Create(c);

            //Assert 
            _airportServiceMock.Verify(m => m.Create(c), Times.Once);
            Assert.AreEqual("Index", result.RouteValues["action"]);

        }

        [TestMethod]
        public void Invalid_AirPort_Create()
        {
            // Arrange
            AirPort c = new AirPort() { Id = "", Name = "VN", RunwaySize = 12, MaxFWParkingPlace = 5, MaxRWParkingPlace = 10 };
            objController.ModelState.AddModelError("Error", "Something went wrong");

            //Act
            var result = (ViewResult)objController.Create(c);

            //Assert
            _airportServiceMock.Verify(m => m.Create(c), Times.Never);
            Assert.AreEqual("", result.ViewName);
        }

    }
}
