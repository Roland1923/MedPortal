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
        public void Given_PatientHistoryRepository_When_AddingAPatientHistory_Then_ThePatientHistoryShouldBeProperlySaved()
        {
            var doctor = Doctor.Create("Daniel", "Oana", "daniel.oana@gmail.com", "parola", "0746524459", "Cardiologie", "Sf. Spiridon", "Iasi", "Str. Vasile Lupu");
            var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi", new DateTime(1996, 02, 10), "0746524459");

            RunOnDatabase(ctx => {
                //Arrange
                var repository = new PatientHistoryRepository(ctx);
                var patientHistory = PatientHistory.Create(patient, doctor, "Paracetamol", "Febra", "Odihna");

                //Act
                repository.AddPatientHistory(patientHistory);

                //Assert
                Assert.AreEqual(repository.GetAllPatientHistories().Count, 1);
            });
        }

        [TestMethod]
        public void Given_PatientHistoryRepository_When_DeletingAPatientHistory_Then_ThePatientHistoryShouldBeProperlyRemoved()
        {
            var doctor = Doctor.Create("Daniel", "Oana", "daniel.oana@gmail.com", "parola", "0746524459", "Cardiologie", "Sf. Spiridon", "Iasi", "Str. Vasile Lupu");
            var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi", new DateTime(1996, 02, 10), "0746524459");

            RunOnDatabase(ctx => {
                //Arrange
                var repository = new PatientHistoryRepository(ctx);
                var patientHistory = PatientHistory.Create(patient, doctor, "Paracetamol", "Febra", "Odihna");
                repository.AddPatientHistory(patientHistory);
                var id = patientHistory.HistoryId;

                //Act
                repository.DeletePatientHistory(id);

                //Assert
                Assert.AreEqual(repository.GetAllPatientHistories().Count, 0);
            });
        }

        [TestMethod]
        public void Given_PatientHistoryRepository_When_EditingAPatientHistory_Then_ThePatientHistoryShouldBeProperlyEdited()
        {
            var doctor = Doctor.Create("Daniel", "Oana", "daniel.oana@gmail.com", "parola", "0746524459", "Cardiologie", "Sf. Spiridon", "Iasi", "Str. Vasile Lupu");
            var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi", new DateTime(1996, 02, 10), "0746524459");

            RunOnDatabase(ctx => {
                //Arrange
                var repository = new PatientHistoryRepository(ctx);
                var patientHistory = PatientHistory.Create(patient, doctor, "Paracetamol", "Febra", "Odihna");
                repository.AddPatientHistory(patientHistory);

                var prescription = patientHistory.Prescription;
                patientHistory.Update(patient, doctor, "Fervex", "Febra", "Odihna");
                var newPrescription = patientHistory.Prescription;

                //Act
                repository.EditPatientHistory(patientHistory);

                //Assert
                Assert.AreNotEqual(prescription, newPrescription);
            });
        }

        [TestMethod]
        public void Given_PatientHistoryRepository_When_ReturningAPatientHistory_Then_ThePatientHistoryShouldBeProperlyReturned()
        {
            var doctor = Doctor.Create("Daniel", "Oana", "daniel.oana@gmail.com", "parola", "0746524459", "Cardiologie", "Sf. Spiridon", "Iasi", "Str. Vasile Lupu");
            var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi", new DateTime(1996, 02, 10), "0746524459");

            RunOnDatabase(ctx => {
                //Arrange
                var repository = new PatientHistoryRepository(ctx);
                var patientHistory = PatientHistory.Create(patient, doctor, "Paracetamol", "Febra", "Odihna");
                repository.AddPatientHistory(patientHistory);

                //Act
                var extractedPatientHistory = repository.GetPatientHistoryById(patientHistory.HistoryId);

                //Assert
                Assert.AreEqual(patientHistory, extractedPatientHistory);
            });
        }

        [TestMethod]
        public void Given_PatientHistoryRepository_When_ReturningAllPatientHistories_Then_AllPatientHistoriesShouldBeProperlyReturned()
        {
            var doctor = Doctor.Create("Daniel", "Oana", "daniel.oana@gmail.com", "parola", "0746524459", "Cardiologie", "Sf. Spiridon", "Iasi", "Str. Vasile Lupu");
            var patient = Patient.Create("Roland", "Iordache", "roland.iordache96@gmail.com", "asfdsdssd", "Iasi", new DateTime(1996, 02, 10), "0746524459");

            RunOnDatabase(ctx => {
                //Arrange
                var repository = new PatientHistoryRepository(ctx);
                var patientHistory = PatientHistory.Create(patient, doctor, "Paracetamol", "Febra", "Odihna");
                repository.AddPatientHistory(patientHistory);

                //Act
                var count = repository.GetAllPatientHistories().Count;

                //Assert
                Assert.AreEqual(count, 1);
            });
        }
    }
}
