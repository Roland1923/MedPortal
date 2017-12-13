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
            RunOnDatabase(ctx =>
            {
                //Arrange
                var repository = new AppointmentRepository(ctx);
                var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi",
                    new DateTime(1996, 02, 10), "0746524459", null);
                var doctor = Doctor.Create("a", "b", "c@c.com", "adcd", "0334123123", "ads", "dsd", "dsds", "dsds");
                var appointment = Appointment.Create(new DateTime(1996, 02, 10), doctor, patient);

                //Act
                repository.Add(appointment);

                //Assert
                Assert.AreEqual(repository.GetAll().Count(), 1);
            });
        }

        [TestMethod]
        public void Given_AppointmentRepository_When_DeletingAnAppointment_Then_TheAppointmentShouldBeProperlyRemoved()
        {
            RunOnDatabase(ctx =>
            {
                //Arrange
                var repository = new AppointmentRepository(ctx);
                var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi",
                    new DateTime(1996, 02, 10), "0746524459", null);
                var doctor = Doctor.Create("a", "b", "c@c.com", "adcd", "0334123123", "ads", "dsd", "dsds", "dsds");
                var appointment = Appointment.Create(new DateTime(1996, 02, 10), doctor, patient);
                repository.Add(appointment);

                //Act
                repository.Delete(appointment);

                //Assert
                Assert.AreEqual(repository.GetAll().Count(), 0);
            });
        }

        [TestMethod]
        public void Given_AppointmentRepository_When_EditingAnAppointment_Then_TheAppointmentShouldBeProperlyEdited()
        {
            RunOnDatabase(ctx =>
            {
                //Arrange
                var repository = new AppointmentRepository(ctx);
                var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi",
                    new DateTime(1996, 02, 10), "0746524459", null);
                var patient2 = Patient.Create("Roland", "Oana", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi",
                    new DateTime(1996, 02, 10), "0746524459", null);
                var doctor = Doctor.Create("a", "b", "c@c.com", "adcd", "0334123123", "ads", "dsd", "dsds", "dsds");
                var appointment = Appointment.Create(new DateTime(1996, 02, 10), doctor, patient);

                repository.Add(appointment);

                var appointmentPatient = appointment.Patient;
                appointment.Update(new DateTime(1996, 02, 10), doctor, patient2);

                //Act
                repository.Update(appointment);

                //Assert
                Assert.AreNotEqual(appointmentPatient, appointment.Patient);
            });
        }

        [TestMethod]
        public void Given_AppointmentRepository_When_ReturningAnAppointment_Then_TheAppointmentShouldBeProperlyReturned()
        {
            RunOnDatabase(ctx =>
            {
                //Arrange
                var repository = new AppointmentRepository(ctx);
                var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi",
                    new DateTime(1996, 02, 10), "0746524459", null);
                var doctor = Doctor.Create("a", "b", "c@c.com", "adcd", "0334123123", "ads", "dsd", "dsds", "dsds");
                var appointment = Appointment.Create(new DateTime(1996, 02, 10), doctor, patient);
                repository.Add(appointment);
                //Act
                var extractedAppointment = repository.GetById(appointment.AppointmentId);

                //Assert
                Assert.AreEqual(appointment, extractedAppointment);
            });
        }

        [TestMethod]
        public void Given_AppointmentRepository_When_ReturningAllAppointments_Then_AllAppointmentsShouldBeProperlyReturned()
        {
            RunOnDatabase(ctx =>
            {
                //Arrange
                var repository = new AppointmentRepository(ctx);
                var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi", new DateTime(1996, 02, 10), "0746524459", null);
                var doctor = Doctor.Create("a", "b", "c@c.com", "adcd", "0334123123", "ads", "dsd", "dsds", "dsds");
                var appointment = Appointment.Create(new DateTime(1996, 02, 10), doctor, patient);
                repository.Add(appointment);

                //Act
                var count = repository.GetAll().Count();

                //Assert
                Assert.AreEqual(count, 1);
            });
        }
    }
}
