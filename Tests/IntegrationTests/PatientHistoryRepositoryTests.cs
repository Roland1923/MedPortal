using System;
using Core.Entities;
using Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Tests.IntegrationTests
{

    [TestClass]
    public class PatientHistoryRepositoryTests : BaseIntegrationTests
    {
        [TestMethod]
        public void Given_PatientHistoryRepository_When_AddAsyncingAPatientHistory_Then_ThePatientHistoryShouldBeProperlySaved()
        {
            var doctor = Doctor.Create("Daniel", "Oana", "daniel.oana@gmail.com", "parola", "0746524459","ads", "Cardiologie", "Sf. Spiridon", "Iasi", "Str. Vasile Lupu");
            var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi", new DateTime(1996, 02, 10), "0746524459", null);

            RunOnDatabase(async ctx => {
                //Arrange
                var repository = new PatientHistoryRepository(ctx);
                var patientHistory = PatientHistory.Create(patient, doctor, "Paracetamol", "Febra", "Odihna", new DateTime(1996, 02, 10));

                //Act
                await repository.AddAsync(patientHistory);

                //Assert
                Assert.AreEqual(repository.GetAllAsync().Result.Count, 1);
            });
        }

        [TestMethod]
        public void Given_PatientHistoryRepository_When_DeletingAPatientHistory_Then_ThePatientHistoryShouldBeProperlyRemoved()
        {
            var doctor = Doctor.Create("Daniel", "Oana", "daniel.oana@gmail.com", "parola", "0746524459","ads", "Cardiologie", "Sf. Spiridon", "Iasi", "Str. Vasile Lupu");
            var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi", new DateTime(1996, 02, 10), "0746524459", null);

            RunOnDatabase(async ctx => {
                //Arrange
                var repository = new PatientHistoryRepository(ctx);
                var patientHistory = PatientHistory.Create(patient, doctor, "Paracetamol", "Febra", "Odihna", new DateTime(1996, 02, 10));
                await repository.AddAsync(patientHistory);

                //Act
                await repository.DeleteAsync(patientHistory);

                //Assert
                Assert.AreEqual(repository.GetAllAsync().Result.Count, 0);
            });
        }

        [TestMethod]
        public void Given_PatientHistoryRepository_When_EditingAPatientHistory_Then_ThePatientHistoryShouldBeProperlyEdited()
        {
            var doctor = Doctor.Create("Daniel", "Oana", "daniel.oana@gmail.com", "parola", "0746524459","ads", "Cardiologie", "Sf. Spiridon", "Iasi", "Str. Vasile Lupu");
            var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi", new DateTime(1996, 02, 10), "0746524459", null);

            RunOnDatabase(async ctx => {
                //Arrange
                var repository = new PatientHistoryRepository(ctx);
                var patientHistory = PatientHistory.Create(patient, doctor, "Paracetamol", "Febra", "Odihna", new DateTime(1996, 02, 10));
                await repository.AddAsync(patientHistory);

                var prescription = patientHistory.Prescription;
                patientHistory.Update(patient, doctor, "Fervex", "Febra", "Odihna", new DateTime(1996, 02, 10));
                var newPrescription = patientHistory.Prescription;

                //Act
                await repository.UpdateAsync(patientHistory);

                //Assert
                Assert.AreNotEqual(prescription, newPrescription);
            });
        }

        [TestMethod]
        public void Given_PatientHistoryRepository_When_ReturningAPatientHistory_Then_ThePatientHistoryShouldBeProperlyReturned()
        {
            var doctor = Doctor.Create("Daniel", "Oana", "daniel.oana@gmail.com", "parola", "0746524459","ads", "Cardiologie", "Sf. Spiridon", "Iasi", "Str. Vasile Lupu");
            var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi", new DateTime(1996, 02, 10), "0746524459", null);

            RunOnDatabase(async ctx => {
                //Arrange
                var repository = new PatientHistoryRepository(ctx);
                var patientHistory = PatientHistory.Create(patient, doctor, "Paracetamol", "Febra", "Odihna", new DateTime(1996, 02, 10));
                await repository.AddAsync(patientHistory);

                //Act
                var extractedPatientHistory = await repository.GetByIdAsync(patientHistory.HistoryId);

                //Assert
                Assert.AreEqual(patientHistory, extractedPatientHistory);
            });
        }

        [TestMethod]
        public void Given_PatientHistoryRepository_When_ReturningAllPatientHistories_Then_AllPatientHistoriesShouldBeProperlyReturned()
        {
            var doctor = Doctor.Create("Daniel", "Oana", "daniel.oana@gmail.com", "parola", "0746524459","ads", "Cardiologie", "Sf. Spiridon", "Iasi", "Str. Vasile Lupu");
            var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi", new DateTime(1996, 02, 10), "0746524459", null);

            RunOnDatabase(async ctx => {
                //Arrange
                var repository = new PatientHistoryRepository(ctx);
                var patientHistory = PatientHistory.Create(patient, doctor, "Paracetamol", "Febra", "Odihna", new DateTime(1996, 02, 10));
                await repository.AddAsync(patientHistory);

                //Act
                var count = repository.GetAllAsync().Result.Count;

                //Assert
                Assert.AreEqual(count, 1);
            });
        }
    }
}
