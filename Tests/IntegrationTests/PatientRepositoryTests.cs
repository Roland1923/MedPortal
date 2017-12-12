using System;
using System.Linq;
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
                BloodDonor bloodDonor = BloodDonor.Create("A2", null);
                Patient patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi", new DateTime(1996, 02, 10), "0746524459", bloodDonor);

                //Act
                repository.Add(patient);

                //Assert
                Assert.AreEqual(repository.GetAll().Count(), 1);
            });
        }

        [TestMethod]
        public void Given_PatientRepository_When_DeletingAPatient_Then_ThePatientShouldBeProperlyRemoved()
        {
            RunOnDatabase(ctx => {
                //Arrange
                var repository = new PatientRepository(ctx);
                BloodDonor bloodDonor = BloodDonor.Create("A2", null);
                var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi", new DateTime(1996, 02, 10), "0746524459", bloodDonor);
                repository.Add(patient);

                //Act
                repository.Delete(patient);

                //Assert
                Assert.AreEqual(repository.GetAll().Count(), 0);
            });
        }

        [TestMethod]
        public void Given_PatientRepository_When_EditingAPatient_Then_ThePatientShouldBeProperlyEdited()
        {
            RunOnDatabase(ctx => {
                //Arrange
                var repository = new PatientRepository(ctx);
                BloodDonor bloodDonor = BloodDonor.Create("A2", null);
                var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi", new DateTime(1996, 02, 10), "0746524459", bloodDonor);
                var firstName = patient.FirstName;

                repository.Add(patient);

                patient.Update("Daniel", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi", new DateTime(1996, 02, 10), "0746524459", bloodDonor, null, null, null);
                var newFirstName = patient.FirstName;

                //Act
                repository.Update(patient);

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
                BloodDonor bloodDonor = BloodDonor.Create("A2", null);
                var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi", new DateTime(1996, 02, 10), "0746524459", bloodDonor);
                repository.Add(patient);

                //Act
                var extractedPatient = repository.GetById(patient.PatientId);

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
                BloodDonor bloodDonor = BloodDonor.Create("A2", null);
                var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi", new DateTime(1996, 02, 10), "0746524459", bloodDonor);
                repository.Add(patient);

                //Act
                var count = repository.GetAll().Count();

                //Assert
                Assert.AreEqual(count, 1);
            });
        }
    }
}
