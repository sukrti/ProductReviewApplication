using Review.API.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ReviewApiTest.MockData;
using Review.API.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace ReviewApiTest.Systems.Controllers
{
    public class TestReviewController
    {
        [Fact]
        public async Task GetAllReviews_ShouldReturn200Status()
        {
            //Arrange
            var reviewService = new Mock<IReviewService>();
            reviewService.Setup(x => x.GetAllReviews()).ReturnsAsync(MockReviewData.GetReviewMockData());
            var sut = new ReviewController(reviewService.Object);

            //Act
            var result=await sut.GetAllReviews();

            //Assert

            result.GetType().Should().Be(typeof(OkObjectResult));
            (result as OkObjectResult).StatusCode.Should().Be(200);
        }
        [Fact]
        public async Task GetAllReviews_ReceivedEmptyData_ShouldReturn204Status()
        {
            //Arrange
            var reviewService = new Mock<IReviewService>();
            reviewService.Setup(x => x.GetAllReviews()).ReturnsAsync(MockReviewData.GetReviewEmptyData());
            var sut = new ReviewController(reviewService.Object);

            //Act
            var result = await sut.GetAllReviews();

            //Assert

            result.GetType().Should().Be(typeof(NoContentResult));
            (result as NoContentResult).StatusCode.Should().Be(204);
        }


        [Fact]
        public async Task AddReviews__ShouldCallAddOnce()
        {
            //Arrange
            var reviewService = new Mock<IReviewService>();
            var newreview= MockReviewData.AddNewReviewForTest();
            var sut = new ReviewController(reviewService.Object);

            //Act
            var result = await sut.AddReviews(newreview);

            //Assert
            reviewService.Verify(_ => _.Add(newreview), Times.Exactly(1));

        }


    }
}
