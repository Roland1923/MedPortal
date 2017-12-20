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
        public void Given_PatientRepository_When_AddAsyncingAPatient_Then_ThePatientShouldBeProperlySaved()
        {
            RunOnDatabase(async ctx => {
                //Arrange
                var repository = new PatientRepository(ctx);
                BloodDonor bloodDonor = BloodDonor.Create("A2", null);
                Patient patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi", new DateTime(1996, 02, 10), "0746524459", bloodDonor);

                //Act
                await repository.AddAsync(patient);

                //Assert
                Assert.AreEqual(repository.GetAllAsync().Result.Count, 1);
            });
        }

        [TestMethod]
        public void Given_PatientRepository_When_DeletingAPatient_Then_ThePatientShouldBeProperlyRemoved()
        {
            RunOnDatabase(async ctx => {
                //Arrange
                var repository = new PatientRepository(ctx);
                BloodDonor bloodDonor = BloodDonor.Create("A2", null);
                var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi", new DateTime(1996, 02, 10), "0746524459", bloodDonor);
                await repository.AddAsync(patient);

                //Act
                await repository.DeleteAsync(patient);

                //Assert
                Assert.AreEqual(repository.GetAllAsync().Result.Count, 0);
            });
        }

        [TestMethod]
        public void Given_PatientRepository_When_EditingAPatient_Then_ThePatientShouldBeProperlyEdited()
        {
            RunOnDatabase(async ctx => {
                //Arrange
                var repository = new PatientRepository(ctx);
                BloodDonor bloodDonor = BloodDonor.Create("A2", null);
                var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi", new DateTime(1996, 02, 10), "0746524459", bloodDonor);
                var firstName = patient.FirstName;

                await repository.AddAsync(patient);

                patient.Update("Daniel", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi", new DateTime(1996, 02, 10), "0746524459", bloodDonor, null, null, null);
                var newFirstName = patient.FirstName;

                //Act
                await repository.UpdateAsync(patient);

                //Assert
                Assert.AreNotEqual(firstName, newFirstName);
            });
        }

        [TestMethod]
        public void Given_PatientRepository_When_ReturningAPatient_Then_ThePatientShouldBeProperlyReturned()
        {
            RunOnDatabase(async ctx => {
                //Arrange
                var repository = new PatientRepository(ctx);
                BloodDonor bloodDonor = BloodDonor.Create("A2", null);
                var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi", new DateTime(1996, 02, 10), "0746524459", bloodDonor);
                await repository.AddAsync(patient);

                //Act
                var extractedPatient = await repository.GetByIdAsync(patient.PatientId);

                //Assert
                Assert.AreEqual(patient, extractedPatient);
            });
        }

        [TestMethod]
        public void Given_PatientRepository_When_ReturningAllPatients_Then_AllPatientsShouldBeProperlyReturned()
        {
            RunOnDatabase(async ctx => {
                //Arrange
                var repository = new PatientRepository(ctx);
                BloodDonor bloodDonor = BloodDonor.Create("A2", null);
                var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi", new DateTime(1996, 02, 10), "0746524459", bloodDonor);
                await repository.AddAsync(patient);

                //Act
                var count = repository.GetAllAsync().Result.Count;

                //Assert
                Assert.AreEqual(count, 1);
            });
        }
    }
}
