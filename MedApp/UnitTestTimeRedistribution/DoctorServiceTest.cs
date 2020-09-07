using MedAppCore;
using MedAppCore.Models;
using MedAppServices;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTestTimeRedistribution
{
    public class DoctorServiceTest
    {
        [Fact]
        public void GetAll_reeturns_doctor_list_and_check_list_count()
        {
            Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
            DoctorService doctorService = new DoctorService(unitOfWork.Object);

            List<Doctor> doctors = new List<Doctor>()
            {
                new Doctor()
                {
                    Id = Guid.NewGuid(),
                    Name = "Kristijan"
                },
                new Doctor()
                {
                    Id = Guid.NewGuid(),
                    Name = "Filip"
                }
            };

            unitOfWork.Setup(_ => _.Doctors.GetAllWithAppointmentAsync()).Returns(Task.FromResult<IEnumerable<Doctor>>(doctors));

            var a = doctorService.GetAllWithAppointment();

            var res = Assert.IsType<List<Doctor>>(a.Result);
            Assert.Equal(2, res.Count);    
        }

        [Fact]
        public void GetById_check_id_and_name_matching()
        {
            Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
            DoctorService doctorService = new DoctorService(unitOfWork.Object);

            var doctorId = Guid.NewGuid();
            var name = "Kristijan";
            Doctor doctor =
                new Doctor()
                {
                    Id = doctorId,
                    Name = name
                };

            unitOfWork.Setup(_ => _.Doctors.GetByIdAsync(doctorId)).Returns(new ValueTask<Doctor>(doctor));

            var a = doctorService.GetDoctorById(doctorId);

            var res = Assert.IsType<Doctor>(a.Result);
            Assert.Equal(res.Name, name);
            Assert.Equal(res.Id, doctorId);
        }

        [Fact]
        public void GetById_returns_null()
        {
            Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
            DoctorService doctorService = new DoctorService(unitOfWork.Object);

            var doctorId = Guid.NewGuid();
            Doctor doc = null;

            unitOfWork.Setup(_ => _.Doctors.GetByIdAsync(It.IsAny<Guid>())).Returns(new ValueTask<Doctor>(doc));

            var a = doctorService.GetDoctorById(doctorId);
            
            Assert.Null(a.Result);
        }
    }
}
