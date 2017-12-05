using System;
using Core.Entities;
using Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.IntegrationTests
{
    [TestClass]
    public class BloodTypeRepositoryTests : BaseIntegrationTests
    {
        [TestMethod]
        public void Given_BloodTypeRepository_When_AddingAnBloodType_Then_TheBloodTypeShouldBeProperlySaved()
        {
            RunOnDatabase(ctx => {
                //Arrange
                var repository = new BloodTypeRepository(ctx);
                var bloodType = BloodType.Create("AB4");

                //Act
                repository.AddBloodType(bloodType);

                //Assert
                Assert.AreEqual(repository.GetAllBloodTypes().Count, 1);
            });
        }
        [TestMethod]
        public void Given_BloodTypeRepository_When_DeletingAnBloodType_Then_TheBloodTypeShouldBeProperlyRemoved()
        {
            RunOnDatabase(ctx => {
                //Arrange
                var repository = new BloodTypeRepository(ctx);
                var bloodType = BloodType.Create("AB4");

                repository.AddBloodType(bloodType);
                var id = bloodType.BloodTypeId;

                //Act
                repository.DeleteBloodType(id);

                //Assert
                Assert.AreEqual(repository.GetAllBloodTypes().Count, 0);
            });
        }
        [TestMethod]
        public void Given_BloodTypeRepository_When_EditingAnBloodType_Then_TheBloodTypeShouldBeProperlyEdited()
        {
            RunOnDatabase(ctx => {
                //Arrange
                var repository = new BloodTypeRepository(ctx);
                var bloodType = BloodType.Create("AB4");
                repository.AddBloodType(bloodType);
                var currentType = bloodType.Type;
                bloodType.Update("A2");
                var newCurrentType = bloodType.Type;

                //Act
                repository.EditBloodType(bloodType);

                //Assert
                Assert.AreNotEqual(currentType, newCurrentType);
            });
        }
        [TestMethod]
        public void Given_BloodTypeRepository_When_ReturningAnBloodType_Then_TheBloodTypeShouldBeProperlyReturned()
        {
            RunOnDatabase(ctx => {
                //Arrange
                var repository = new BloodTypeRepository(ctx);
                var bloodType = BloodType.Create("AB4");
                repository.AddBloodType(bloodType);

                //Act
                var extractedBloodType = repository.GetBloodTypeById(bloodType.BloodTypeId);

                //Assert
                Assert.AreEqual(bloodType, extractedBloodType);
            });
        }
        [TestMethod]
        public void Given_BloodTypeRepository_When_ReturningAllBloodTypes_Then_AllBloodTypesShouldBeProperlyReturned()
        {
            RunOnDatabase(ctx => {
                //Arrange
                var repository = new BloodTypeRepository(ctx);
                var bloodType = BloodType.Create("AB4");
                repository.AddBloodType(bloodType);

                //Act
                var count = repository.GetAllBloodTypes().Count;

                //Assert
                Assert.AreEqual(count, 1);
            });
        }
    }
}
