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
        public void Given_AppointmentRepository_When_AddAsyncingAnAppointment_Then_TheAppointmentShouldBeProperlySaved()
        {
            RunOnDatabase(async ctx =>
            {
                //Arrange
                var repository = new AppointmentRepository(ctx);
                var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi",
                    new DateTime(1996, 02, 10), "0746524459", null);
                var doctor = Doctor.Create("a", "b", "c@c.com", "adcd", "0334123123", "ads", "ads", "dsd", "dsds", "dsds");
                var appointment = Appointment.Create(new DateTime(1996, 02, 10), doctor, patient);

                //Act
                await repository.AddAsync(appointment);

                //Assert
                Assert.AreEqual(repository.GetAllAsync().Result.Count, 1);
            });
        }

        [TestMethod]
        public void Given_AppointmentRepository_When_DeletingAnAppointment_Then_TheAppointmentShouldBeProperlyRemoved()
        {
            RunOnDatabase(async ctx =>
            {
                //Arrange
                var repository = new AppointmentRepository(ctx);
                var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi",
                    new DateTime(1996, 02, 10), "0746524459", null);
                var doctor = Doctor.Create("a", "b", "c@c.com", "adcd", "0334123123", "ads", "ads", "dsd", "dsds", "dsds");
                var appointment = Appointment.Create(new DateTime(1996, 02, 10), doctor, patient);
                await repository.AddAsync(appointment);

                //Act
                await repository.DeleteAsync(appointment);

                //Assert
                Assert.AreEqual(repository.GetAllAsync().Result.Count, 0);
            });
        }

        [TestMethod]
        public void Given_AppointmentRepository_When_EditingAnAppointment_Then_TheAppointmentShouldBeProperlyEdited()
        {
            RunOnDatabase(async ctx =>
            {
                //Arrange
                var repository = new AppointmentRepository(ctx);
                var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi",
                    new DateTime(1996, 02, 10), "0746524459", null);
                var patient2 = Patient.Create("Roland", "Oana", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi",
                    new DateTime(1996, 02, 10), "0746524459", null);
                var doctor = Doctor.Create("a", "b", "c@c.com", "adcd", "0334123123", "ads", "ads", "dsd", "dsds", "dsds");
                var appointment = Appointment.Create(new DateTime(1996, 02, 10), doctor, patient);

                await repository.AddAsync(appointment);

                var appointmentPatient = appointment.Patient;
                appointment.Update(new DateTime(1996, 02, 10), doctor, patient2);

                //Act
                await repository.UpdateAsync(appointment);

                //Assert
                Assert.AreNotEqual(appointmentPatient, appointment.Patient);
            });
        }

        [TestMethod]
        public void Given_AppointmentRepository_When_ReturningAnAppointment_Then_TheAppointmentShouldBeProperlyReturned()
        {
            RunOnDatabase(async ctx =>
            {
                //Arrange
                var repository = new AppointmentRepository(ctx);
                var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi",
                    new DateTime(1996, 02, 10), "0746524459", null);
                var doctor = Doctor.Create("a", "b", "c@c.com", "adcd", "0334123123", "ads", "ads", "dsd", "dsds", "dsds");
                var appointment = Appointment.Create(new DateTime(1996, 02, 10), doctor, patient);
                await repository.AddAsync(appointment);

                //Act
                var extractedAppointment = repository.GetByIdAsync(appointment.AppointmentId);

                //Assert
                Assert.AreEqual(appointment, extractedAppointment);
            });
        }

        [TestMethod]
        public void Given_AppointmentRepository_When_ReturningAllAppointments_Then_AllAppointmentsShouldBeProperlyReturned()
        {
            RunOnDatabase(async ctx =>
            {
                //Arrange
                var repository = new AppointmentRepository(ctx);
                var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi", new DateTime(1996, 02, 10), "0746524459", null);
                var doctor = Doctor.Create("a", "b", "c@c.com", "adcd", "0334123123", "ads", "ads", "dsd", "dsds", "dsds");
                var appointment = Appointment.Create(new DateTime(1996, 02, 10), doctor, patient);
                await repository.AddAsync(appointment);

                //Act
                var count = repository.GetAllAsync().Result.Count;

                //Assert
                Assert.AreEqual(count, 1);
            });
        }
    }
}
