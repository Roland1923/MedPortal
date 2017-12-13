using System.Linq;
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
            RunOnDatabase(ctx => {
                //Arrange
                var repository = new BloodDonorRepository(ctx);
                var bloodDonor = BloodDonor.Create("AB4", null);

                //Act
                repository.Add(bloodDonor);

                //Assert
                Assert.AreEqual(repository.GetAll().Count(), 1);
            });
        }
        [TestMethod]
        public void Given_BloodDonorRepository_When_DeletingAnBloodDonor_Then_TheBloodDonorShouldBeProperlyRemoved()
        {
            RunOnDatabase(ctx => {
                //Arrange
                var repository = new BloodDonorRepository(ctx);
                var bloodDonor = BloodDonor.Create("AB4", null);

                repository.Add(bloodDonor);
                var id = bloodDonor.BloodDonorId;

                //Act
                repository.Delete(bloodDonor);

                //Assert
                Assert.AreEqual(repository.GetAll().Count(), 0);
            });
        }
        [TestMethod]
        public void Given_BloodDonorRepository_When_EditingAnBloodDonor_Then_TheBloodDonorShouldBeProperlyEdited()
        {
            RunOnDatabase(ctx => {
                //Arrange
                var repository = new BloodDonorRepository(ctx);
                var bloodDonor = BloodDonor.Create("AB4", null);
                repository.Add(bloodDonor);
                var currentType = bloodDonor.Type;
                bloodDonor.Update("A4", null);
                var newCurrentType = bloodDonor.Type;

                //Act
                repository.Update(bloodDonor);

                //Assert
                Assert.AreNotEqual(currentType, newCurrentType);
            });
        }
        [TestMethod]
        public void Given_BloodDonorRepository_When_ReturningAnBloodDonor_Then_TheBloodDonorShouldBeProperlyReturned()
        {
            RunOnDatabase(ctx => {
                //Arrange
                var repository = new BloodDonorRepository(ctx);
                var bloodDonor = BloodDonor.Create("AB4", null);
                repository.Add(bloodDonor);

                //Act
                var extractedBloodDonor = repository.GetById(bloodDonor.BloodDonorId);

                //Assert
                Assert.AreEqual(bloodDonor, extractedBloodDonor);
            });
        }
        [TestMethod]
        public void Given_BloodDonorRepository_When_ReturningAllBloodDonors_Then_AllBloodDonorsShouldBeProperlyReturned()
        {
            RunOnDatabase(ctx => {
                //Arrange
                var repository = new BloodDonorRepository(ctx);
                var bloodDonor = BloodDonor.Create("AB4", null);
                repository.Add(bloodDonor);

                //Act
                var count = repository.GetAll().Count();

                //Assert
                Assert.AreEqual(count, 1);
            });
        }
    }
}
