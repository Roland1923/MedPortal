using Core.Entities;
using Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Tests.IntegrationTests
{
    [TestClass]
    public class FeedbackRepositoryTests : BaseIntegrationTests
    {
        [TestMethod]
        public void Given_FeedbackRepository_When_AddAsyncingAFeedback_Then_TheFeedbackShouldBeProperlySaved()
        {
            RunOnDatabase(async ctx => {
                //Arrange
                var repository = new FeedbackRepository(ctx);
                var feedback = Feedback.Create("OK", null, null, 5);
                
                //Act
                await repository.AddAsync(feedback);

                //Assert
                Assert.AreEqual(repository.GetAllAsync().Result.Count, 1);
            });
        }

        [TestMethod]
        public void Given_FeedbackRepository_When_DeletingAFeedback_Then_TheFeedbackShouldBeProperlyRemoved()
        {
            RunOnDatabase(async ctx => {
                //Arrange
                var repository = new FeedbackRepository(ctx);
                var feedback = Feedback.Create("OK", null, null, 5);
                await repository.AddAsync(feedback);

                //Act
                await repository.DeleteAsync(feedback);

                //Assert
                Assert.AreEqual(repository.GetAllAsync().Result.Count, 0);
            });
        }

        [TestMethod]
        public void Given_FeedbackRepository_When_EditingAFeedback_Then_TheFeedbackShouldBeProperlyEdited()
        {
            RunOnDatabase(async ctx => {
                //Arrange
                var repository = new FeedbackRepository(ctx);
                var feedback = Feedback.Create("OK", null, null, 5);
                await repository.AddAsync(feedback);

                var description = feedback.Description;
                feedback.Update("IT's OK", null, null, 4);
                
                //Act
                await repository.UpdateAsync(feedback);

                //Assert
                Assert.AreNotEqual(feedback.Description, description);
            });
        }

        [TestMethod]
        public void Given_FeedbackRepository_When_ReturningAFeedback_Then_TheFeedbackShouldBeProperlyReturned()
        {
            RunOnDatabase(async ctx => {
                //Arrange
                var repository = new FeedbackRepository(ctx);
                var feedback = Feedback.Create("OK", null, null, 5);
                await repository.AddAsync(feedback);

                //Act
                var extractedFeedback = await repository.GetByIdAsync(feedback.FeedbackId);

                //Assert
                Assert.AreEqual(feedback, extractedFeedback);
            });
        }

        [TestMethod]
        public void Given_FeedbackRepository_When_ReturningAllFeedbacks_Then_AllFeedbacksShouldBeProperlyReturned()
        {
            RunOnDatabase(async ctx => {
                //Arrange
                var repository = new FeedbackRepository(ctx);
                var feedback = Feedback.Create("OK", null, null, 5);
                await repository.AddAsync(feedback);

                //Act
                var count = repository.GetAllAsync().Result.Count;

                //Assert
                Assert.AreEqual(count, 1);
            });
        }
    }
}
