using System.Linq;
using Core.Entities;
using Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Tests.IntegrationTests
{
    [TestClass]
    public class FeedbackRepositoryTests : BaseIntegrationTests
    {
        [TestMethod]
        public void Given_FeedbackRepository_When_AddingAFeedback_Then_TheFeedbackShouldBeProperlySaved()
        {
            RunOnDatabase(ctx => {
                //Arrange
                var repository = new FeedbackRepository(ctx);
                var feedback = Feedback.Create("OK", null, null, 5);
                
                //Act
                repository.Add(feedback);

                //Assert
                Assert.AreEqual(repository.GetAll().Count(), 1);
            });
        }

        [TestMethod]
        public void Given_FeedbackRepository_When_DeletingAFeedback_Then_TheFeedbackShouldBeProperlyRemoved()
        {
            RunOnDatabase(ctx => {
                //Arrange
                var repository = new FeedbackRepository(ctx);
                var feedback = Feedback.Create("OK", null, null, 5);
                repository.Add(feedback);

                //Act
                repository.Delete(feedback);

                //Assert
                Assert.AreEqual(repository.GetAll().Count(), 0);
            });
        }

        [TestMethod]
        public void Given_FeedbackRepository_When_EditingAFeedback_Then_TheFeedbackShouldBeProperlyEdited()
        {
            RunOnDatabase(ctx => {
                //Arrange
                var repository = new FeedbackRepository(ctx);
                var feedback = Feedback.Create("OK", null, null, 5);
                repository.Add(feedback);

                var description = feedback.Description;
                feedback.Update("IT's OK", null, null, 4);
                
                //Act
                repository.Update(feedback);

                //Assert
                Assert.AreNotEqual(feedback.Description, description);
            });
        }

        [TestMethod]
        public void Given_FeedbackRepository_When_ReturningAFeedback_Then_TheFeedbackShouldBeProperlyReturned()
        {
            RunOnDatabase(ctx => {
                //Arrange
                var repository = new FeedbackRepository(ctx);
                var feedback = Feedback.Create("OK", null, null, 5);
                repository.Add(feedback);

                //Act
                var extractedFeedback = repository.GetById(feedback.FeedbackId);

                //Assert
                Assert.AreEqual(feedback, extractedFeedback);
            });
        }

        [TestMethod]
        public void Given_FeedbackRepository_When_ReturningAllFeedbacks_Then_AllFeedbacksShouldBeProperlyReturned()
        {
            RunOnDatabase(ctx => {
                //Arrange
                var repository = new FeedbackRepository(ctx);
                var feedback = Feedback.Create("OK", null, null, 5);
                repository.Add(feedback);

                //Act
                var count = repository.GetAll().Count();

                //Assert
                Assert.AreEqual(count, 1);
            });
        }
    }
}
