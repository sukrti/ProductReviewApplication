using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Review.API.Models;
using Review.API.Services;
using ReviewApiTest.MockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ReviewApiTest.Systems.Services
{
    public class ReviewServiceTest:IDisposable
    {
        private readonly ReviewDBContext _databasecontext;
        public ReviewServiceTest()
        {
            var options = new DbContextOptionsBuilder<ReviewDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _databasecontext = new ReviewDBContext(options);

            _databasecontext.Database.EnsureCreated();


        }

        [Fact]
        public async Task GetAllReviews_ReturnReviewCollection()
        {
            //Arrange
            _databasecontext.Reviews.AddRange(MockReviewData.GetReviewMockData());
            _databasecontext.SaveChanges();

            var sut = new ReviewService(_databasecontext);

            //Act
            var result = await sut.GetAllReviews();


            //Assert
            result.Should().HaveCount(MockReviewData.GetReviewMockData().Count);

        }


        [Fact]
        public async Task Add_AddNewReviewItemInDataBase()
        {
            //Arrange

            var newdata = MockReviewData.AddNewReviewForTest();
            _databasecontext.Reviews.AddRange(MockReviewData.GetReviewMockData());
            _databasecontext.SaveChanges();

        

            var sut= new ReviewService(_databasecontext);   

            //Act
            await sut.Add(newdata);


            //Assert
            int expextedcount = MockReviewData.GetReviewMockData().Count + 1;
            _databasecontext.Reviews.Count().Should().Be(expextedcount);    

        }
        public void Dispose()
        {
            _databasecontext.Database.EnsureDeleted();
            _databasecontext.Dispose();
        }
    }
}
