using Core.Entities;
using Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.IntegrationTests
{
    [TestClass]
    public class BloodDonorRepositoryTests : BaseIntegrationTests
    {
        [TestMethod]
        public void Given_BloodDonorRepository_When_AddingAnBloodDonor_Then_TheBloodDonorShouldBeProperlySaved()
        {
            RunOnDatabase(async ctx => {
                //Arrange
                var repository = new BloodDonorRepository(ctx);
                var bloodDonor = BloodDonor.Create("AB4", null);

                //Act
                await repository.AddAsync(bloodDonor);

                //Assert
                Assert.AreEqual(repository.GetAllAsync().Result.Count, 1);
            });
        }
        [TestMethod]
        public void Given_BloodDonorRepository_When_DeletingAnBloodDonor_Then_TheBloodDonorShouldBeProperlyRemoved()
        {
            RunOnDatabase(async ctx => {
                //Arrange
                var repository = new BloodDonorRepository(ctx);
                var bloodDonor = BloodDonor.Create("AB4", null);

                await repository.AddAsync(bloodDonor);

                //Act
                await repository.DeleteAsync(bloodDonor);

                //Assert
                Assert.AreEqual(repository.GetAllAsync().Result.Count, 0);
            });
        }
        [TestMethod]
        public void Given_BloodDonorRepository_When_EditingAnBloodDonor_Then_TheBloodDonorShouldBeProperlyEdited()
        {
            RunOnDatabase(async ctx => {
                //Arrange
                var repository = new BloodDonorRepository(ctx);
                var bloodDonor = BloodDonor.Create("AB4", null);
                await repository.AddAsync(bloodDonor);
                var currentType = bloodDonor.Type;
                bloodDonor.Update("A4", null);
                var newCurrentType = bloodDonor.Type;

                //Act
                await repository.UpdateAsync(bloodDonor);

                //Assert
                Assert.AreNotEqual(currentType, newCurrentType);
            });
        }
        [TestMethod]
        public void Given_BloodDonorRepository_When_ReturningAnBloodDonor_Then_TheBloodDonorShouldBeProperlyReturned()
        {
            RunOnDatabase(async ctx => {
                //Arrange
                var repository = new BloodDonorRepository(ctx);
                var bloodDonor = BloodDonor.Create("AB4", null);
                await repository.AddAsync(bloodDonor);

                //Act
                var extractedBloodDonor = repository.GetByIdAsync(bloodDonor.BloodDonorId);

                //Assert
                Assert.AreEqual(bloodDonor, extractedBloodDonor);
            });
        }
        [TestMethod]
        public void Given_BloodDonorRepository_When_ReturningAllBloodDonors_Then_AllBloodDonorsShouldBeProperlyReturned()
        {
            RunOnDatabase(async ctx => {
                //Arrange
                var repository = new BloodDonorRepository(ctx);
                var bloodDonor = BloodDonor.Create("AB4", null);
                await repository.AddAsync(bloodDonor);

                //Act
                var count = repository.GetAllAsync().Result.Count;

                //Assert
                Assert.AreEqual(count, 1);
            });
        }
    }
}
