using Core.Entities;
using Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Tests.IntegrationTests
{
    [TestClass]
    public class BloodBankRepositoryTests : BaseIntegrationTests
    {
        [TestMethod]
        public void Given_BloodBankRepository_When_AddAsyncingABloodBank_Then_TheBloodBankShouldBeProperlySaved()
        {
            RunOnDatabase(async ctx => {
                //Arrange
                var repository = new BloodBankRepository(ctx);
                var doctor = Doctor.Create("Daniel", "Oana", "daniel.oana@gmail.com", "parola", "0746524459", "bla", "Cardiologie", "Sf. Spiridon", "Iasi", "Str. Vasile Lupu");
                var bloodBank = BloodBank.Create(doctor);
                //Act
                await repository.AddAsync(bloodBank);

                //Assert
                Assert.AreEqual(repository.GetAllAsync().Result.Count, 1);
            });
        }

        [TestMethod]
        public void Given_BloodBankRepository_When_DeletingABloodBank_Then_TheBloodBankShouldBeProperlyRemoved()
        {
            RunOnDatabase(async ctx => {
                // Arrange
                var repository = new BloodBankRepository(ctx);
                var doctor = Doctor.Create("Daniel", "Oana", "daniel.oana@gmail.com", "parola", "0746524459", "bla", "Cardiologie", "Sf. Spiridon", "Iasi", "Str. Vasile Lupu");
                var bloodBank = BloodBank.Create(doctor);
                await repository.AddAsync(bloodBank);

                //Act
                await repository.DeleteAsync(bloodBank);

                //Assert
                Assert.AreEqual(repository.GetAllAsync().Result.Count, 0);
            });
        }

        [TestMethod]
        public void Given_BloodBankRepository_When_EditingABloodBank_Then_TheBloodBankShouldBeProperlyEdited()
        {
            RunOnDatabase(async ctx => {
                //Arrange
                var repository = new BloodBankRepository(ctx);
                var doctor = Doctor.Create("Daniel", "Oana", "daniel.oana@gmail.com", "parola", "0746524459", "bla", "Cardiologie", "Sf. Spiridon", "Iasi", "Str. Vasile Lupu");
                var bloodBank = BloodBank.Create(doctor);
                await repository.AddAsync(bloodBank);

                //var firstName = doctor.FirstName;

                bloodBank.Update("A2", doctor);

                //var newFirstName = doctor.FirstName;

                //Act
                await repository.UpdateAsync(bloodBank);

                //Assert
                Assert.AreEqual(repository.GetAllAsync().Result[0].Types["A2"], 1);
            });
        }

        [TestMethod]
        public void Given_BloodBankRepository_When_ReturningABloodBank_Then_TheBloodBankShouldBeProperlyReturned()
        {
            RunOnDatabase(async ctx => {
                //Arrange
                var repository = new BloodBankRepository(ctx);
                var doctor = Doctor.Create("Daniel", "Oana", "daniel.oana@gmail.com", "parola", "0746524459", "bla", "Cardiologie", "Sf. Spiridon", "Iasi", "Str. Vasile Lupu");
                var bloodBank = BloodBank.Create(doctor);
                await repository.AddAsync(bloodBank);

                //Act
                var extractedBloodBank = await repository.GetByIdAsync(bloodBank.BloodBankId);

                //Assert
                Assert.AreEqual(bloodBank, extractedBloodBank);
            });
        }

        [TestMethod]
        public void Given_BloodBankRepository_When_ReturningAllBloodBanks_Then_AllBloodBanksShouldBeProperlyReturned()
        {
            RunOnDatabase(async ctx => {
                //Arrange
                var repository = new BloodBankRepository(ctx);
                var doctor = Doctor.Create("Daniel", "Oana", "daniel.oana@gmail.com", "parola", "0746524459", "bla", "Cardiologie", "Sf. Spiridon", "Iasi", "Str. Vasile Lupu");
                var bloodBank = BloodBank.Create(doctor);
                await repository.AddAsync(bloodBank);

                //Act
                var count = repository.GetAllAsync().Result.Count;

                //Assert
                Assert.AreEqual(count, 1);
            });
        }
    }
}
