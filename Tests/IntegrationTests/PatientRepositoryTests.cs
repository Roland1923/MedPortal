using System;
using Core.Entities;
using Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Tests.IntegrationTests
{
    [TestClass]
    public class PatientRepositoryTests : BaseIntegrationTests
    {
        [TestMethod]
        public void Given_PatientRepository_When_AddingAPatient_Then_ThePatientShouldBeProperlySaved()
        {
            RunOnDatabase(ctx => {
                //Arrange
                var repository = new PatientRepository(ctx);
                var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi", new DateTime(1996, 02, 10), "0746524459");

                //Act
                repository.AddPatient(patient);

                //Assert
                Assert.AreEqual(repository.GetAllPatients().Count,1);
            });
        }

        [TestMethod]
        public void Given_PatientRepository_When_DeletingAPatient_Then_ThePatientShouldBeProperlyRemoved()
        {
            RunOnDatabase(ctx => {
                //Arrange
                var repository = new PatientRepository(ctx);
                var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi", new DateTime(1996, 02, 10), "0746524459");
                repository.AddPatient(patient);
                var id = patient.PatientId;

                //Act
                repository.DeletePatient(id);

                //Assert
                Assert.AreEqual(repository.GetAllPatients().Count, 0);
            });
        }

        [TestMethod]
        public void Given_PatientRepository_When_EditingAPatient_Then_ThePatientShouldBeProperlyEdited()
        {
            RunOnDatabase(ctx => {
                //Arrange
                var repository = new PatientRepository(ctx);
                var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi", new DateTime(1996, 02, 10), "0746524459");
                var firstName = patient.FirstName;

                repository.AddPatient(patient);

                patient.Update("Daniel", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi", new DateTime(1996, 02, 10), "0746524459");
                var newFirstName = patient.FirstName;

                //Act
                repository.EditPatient(patient);

                //Assert
                Assert.AreNotEqual(firstName, newFirstName);
            });
        }

        [TestMethod]
        public void Given_PatientRepository_When_ReturningAPatient_Then_ThePatientShouldBeProperlyReturned()
        {
            RunOnDatabase(ctx => {
                //Arrange
                var repository = new PatientRepository(ctx);
                var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi", new DateTime(1996, 02, 10), "0746524459");
                repository.AddPatient(patient);

                //Act
                var extractedPatient = repository.GetPatientById(patient.PatientId);

                //Assert
                Assert.AreEqual(patient, extractedPatient);
            });
        }

        [TestMethod]
        public void Given_PatientRepository_When_ReturningAllPatients_Then_AllPatientsShouldBeProperlyReturned()
        {
            RunOnDatabase(ctx => {
                //Arrange
                var repository = new PatientRepository(ctx);
                var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi", new DateTime(1996, 02, 10), "0746524459");
                repository.AddPatient(patient);

                //Act
                var count = repository.GetAllPatients().Count;

                //Assert
                Assert.AreEqual(count, 1);
            });
        }
    }
}
