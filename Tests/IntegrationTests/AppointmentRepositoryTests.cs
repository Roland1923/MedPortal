using System;
using System.Linq;
using Core.Entities;
using Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.IntegrationTests
{
    [TestClass]
    public class AppointmentRepositoryTests : BaseIntegrationTests
    {
        [TestMethod]
        public void Given_AppointmentRepository_When_AddingAnAppointment_Then_TheAppointmentShouldBeProperlySaved()
        {
            RunOnDatabase(ctx => {
                //Arrange
                var repository = new AppointmentRepository(ctx);
                var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi", new DateTime(1996, 02, 10), "0746524459", null);
                var doctor = Doctor.Create("a", "b", "c@c.com", "adcd", "0334123123", "ads", "dsd", "dsds", "dsds");
                var appointment = Appointment.Create(new DateTime(1996, 02, 10),doctor,patient);

                //Act
                repository.Add(appointment);

                //Assert
                Assert.AreEqual(repository.GetAll().ToList().Count, 1);
            });
        }
      
    }
}
