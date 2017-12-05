using System;
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
                var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "Iasi", new DateTime(1996, 02, 10), "0746524459");
                var appointment = Appointment.Create(new DateTime(1996, 02, 10),patient);

                //Act
                repository.AddAppointment(appointment);

                //Assert
                Assert.AreEqual(repository.GetAllAppointments().Count, 1);
            });
        }
        [TestMethod]
        public void Given_AppointmentRepository_When_DeletingAnAppointment_Then_TheAppointmentShouldBeProperlyRemoved()
        {
            RunOnDatabase(ctx => {
                //Arrange
                var repository = new AppointmentRepository(ctx);
                var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "Iasi", new DateTime(1996, 02, 10), "0746524459");
                var appointment = Appointment.Create(new DateTime(1996, 02, 10), patient);

                repository.AddAppointment(appointment);
                var id = appointment.AppointmentId;

                //Act
                repository.DeleteAppointment(id);

                //Assert
                Assert.AreEqual(repository.GetAllAppointments().Count, 0);
            });
        }
        [TestMethod]
        public void Given_AppointmentRepository_When_EditingAnAppointment_Then_TheAppointmentShouldBeProperlyEdited()
        {
            RunOnDatabase(ctx => {
                //Arrange
                var repository = new AppointmentRepository(ctx);
                var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "Iasi", new DateTime(1996, 02, 10), "0746524459");
                var appointment = Appointment.Create(new DateTime(2017, 12, 04), patient);
                repository.AddAppointment(appointment);
                var currentDate = appointment.AppointmentDate;
                appointment.Update(new DateTime(2017,12,03),patient);
                var newCurrentDate = appointment.AppointmentDate;

                //Act
                repository.EditAppointment(appointment);

                //Assert
                Assert.AreNotEqual(currentDate, newCurrentDate);
            });
        }
        [TestMethod]
        public void Given_AppointmentRepository_When_ReturningAnAppointment_Then_TheAppointmentShouldBeProperlyReturned()
        {
            RunOnDatabase(ctx => {
                //Arrange
                var repository = new AppointmentRepository(ctx);
                var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "Iasi", new DateTime(1996, 02, 10), "0746524459");
                var appointment = Appointment.Create(new DateTime(1996, 02, 10), patient);
                repository.AddAppointment(appointment);

                //Act
                var extractedAppointment = repository.GetAppointmentById(appointment.AppointmentId);

                //Assert
                Assert.AreEqual(appointment, extractedAppointment);
            });
        }
        [TestMethod]
        public void Given_AppointmentRepository_When_ReturningAllAppointments_Then_AllAppointmentsShouldBeProperlyReturned()
        {
            RunOnDatabase(ctx => {
                //Arrange
                var repository = new AppointmentRepository(ctx);
                var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "Iasi", new DateTime(1996, 02, 10), "0746524459");
                var appointment = Appointment.Create(new DateTime(1996, 02, 10), patient);
                repository.AddAppointment(appointment);

                //Act
                var count = repository.GetAllAppointments().Count;

                //Assert
                Assert.AreEqual(count, 1);
            });
        }
    }
}
