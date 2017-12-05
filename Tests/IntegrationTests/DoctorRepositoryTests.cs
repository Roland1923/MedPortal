using Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Entities;

namespace Tests.IntegrationTests
{
    [TestClass]
    public class DoctorRepositoryTests : BaseIntegrationTests
    {
        [TestMethod]
        public void Given_DoctorRepository_When_AddingADoctor_Then_TheDoctorShouldBeProperlySaved()
        {
            RunOnDatabase(ctx => {
                //Arrange
                var repository = new DoctorRepository(ctx);
                var doctor = Doctor.Create("Daniel", "Oana", "daniel.oana@gmail.com", "parola", "0746524459", "Cardiologie", "Sf. Spiridon", "Iasi", "Str. Vasile Lupu");

                //Act
                repository.AddDoctor(doctor);

                //Assert
                Assert.AreEqual(repository.GetAllDoctors().Count,1);
            });
        }

        [TestMethod]
        public void Given_DoctorRepository_When_DeletingADoctor_Then_TheDoctorShouldBeProperlyRemoved()
        {
            RunOnDatabase(ctx => {
                //Arrange
                var repository = new DoctorRepository(ctx);
                var doctor = Doctor.Create("Daniel", "Oana", "daniel.oana@gmail.com", "parola", "0746524459", "Cardiologie", "Sf. Spiridon", "Iasi", "Str. Vasile Lupu");
                repository.AddDoctor(doctor);
                var id = doctor.DoctorId;

                //Act
                repository.DeleteDoctor(id);

                //Assert
                Assert.AreEqual(repository.GetAllDoctors().Count, 0);
            });
        }

        [TestMethod]
        public void Given_DoctorRepository_When_EditingADoctor_Then_TheDoctorShouldBeProperlyEdited()
        {
            RunOnDatabase(ctx => {
                //Arrange
                var repository = new DoctorRepository(ctx);
                var doctor = Doctor.Create("Daniel", "Oana", "daniel.oana@gmail.com", "parola", "0746524459", "Cardiologie", "Sf. Spiridon", "Iasi", "Str. Vasile Lupu");
                repository.AddDoctor(doctor);

                var firstName = doctor.FirstName;

                doctor.Update("Oana", "Oana", "daniel.oana@gmail.com", "parola", "0746524459", "Cardiologie", "Sf. Spiridon", "Iasi", "Str. Vasile Lupu");
                var newFirstName = doctor.FirstName;

                //Act
                repository.EditDoctor(doctor);

                //Assert
                Assert.AreNotEqual(firstName, newFirstName);
            });
        }

        [TestMethod]
        public void Given_DoctorRepository_When_ReturningADoctor_Then_TheDoctorShouldBeProperlyReturned()
        {
            RunOnDatabase(ctx => {
                //Arrange
                var repository = new DoctorRepository(ctx);
                var doctor = Doctor.Create("Daniel", "Oana", "daniel.oana@gmail.com", "parola", "0746524459", "Cardiologie", "Sf. Spiridon", "Iasi", "Str. Vasile Lupu");
                repository.AddDoctor(doctor);

                //Act
                var extractedDoctor = repository.GetDoctorById(doctor.DoctorId);

                //Assert
                Assert.AreEqual(doctor, extractedDoctor);
            });
        }

        [TestMethod]
        public void Given_DoctorRepository_When_ReturningAllDoctors_Then_AllDoctorsShouldBeProperlyReturned()
        {
            RunOnDatabase(ctx => {
                //Arrange
                var repository = new DoctorRepository(ctx);
                var doctor = Doctor.Create("Daniel", "Oana", "daniel.oana@gmail.com", "parola", "0746524459", "Cardiologie", "Sf. Spiridon", "Iasi", "Str. Vasile Lupu");
                repository.AddDoctor(doctor);

                //Act
                var count = repository.GetAllDoctors().Count;

                //Assert
                Assert.AreEqual(count, 1);
            });
        }
    }
}
