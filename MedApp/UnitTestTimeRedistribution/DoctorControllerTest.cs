using AutoMapper;
using MedAppCore.Models;
using MedAppCore.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeRedistribution.Controllers;
using Xunit;

namespace UnitTestTimeRedistribution
{
    public class DoctorControllerTest
    {
        private readonly Mock<IDoctorService> _mockService;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IAmqService> _amqrMock;
        private readonly DoctorController _controller;
        public DoctorControllerTest()
        {
            _mapperMock = new Mock<IMapper>();
            _amqrMock = new Mock<IAmqService>();
            _mockService = new Mock<IDoctorService>();
            _controller = new DoctorController(_mockService.Object, _mapperMock.Object, _amqrMock.Object);
        }

        [Fact]
        public void GetAllDoctors_CheckType()
        {
            var result = _controller.GetAllDoctors();
            // Assert
            Assert.IsType<Task<ActionResult<IEnumerable<Doctor>>>>(result);
        }

        [Fact]
        public async Task GetAllDoctors_ShouldReturnNothing()
        {  
            _mockService.Setup(_ => _.GetAllWithAppointment()).ReturnsAsync(() => null);

            var result = await _controller.GetAllDoctors();           

            var actionResult = Assert.IsType<ActionResult<IEnumerable<Doctor>>>(result);

            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task DeleteDoctor_Test()
        {
            var guid = Guid.NewGuid();
            Doctor doctor = new Doctor {
                Id = guid,
                Name = "Kristijan",
                Surname = "Delac",
                DateOfBirth = DateTime.Now
            };


            _mockService.Setup(repo => repo.GetDoctorById(guid)).ReturnsAsync(doctor);
            _mockService.Setup(repo => repo.DeleteDoctor(doctor)).Returns(Task.CompletedTask);

            await _controller.DeleteDoctor(guid);

            _mockService.Verify(repo => repo.DeleteDoctor(doctor), Times.Once);

        }
    }
}
