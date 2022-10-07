using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Review.API.Models;

namespace ReviewApiTest.MockData
{
    public class MockReviewData
    {
        public static List<ReviewData> GetReviewMockData()
        {
            return new List<ReviewData>
            {
                new ReviewData{Id=Guid.NewGuid(),Rating=2,Title="Amazing product", Comment="Wow! I like the product-1"
                ,Recommendation=ReviewData.RecommendationValues.Yes},
                 new ReviewData{Id=Guid.NewGuid(),Rating=3,Title="Amazing product-1", Comment="Wow! I like the product-2"
                , Recommendation=ReviewData.RecommendationValues.No},
                  new ReviewData{Id=Guid.NewGuid(),Rating=5,Title="Amazing product-2", Comment="Wow! I like the product-3"
                ,Recommendation=ReviewData.RecommendationValues.Maybe}
            };
        }

        public static List<ReviewData> GetReviewEmptyData()
        {
            return new List<ReviewData>();
        }

        public static ReviewData AddNewReviewForTest()
        {
            return new ReviewData()

            {
                Id = Guid.NewGuid(),
                Rating = 2,
                Title = "Amazing product-4",
                Comment = "I was ok product",
                Recommendation = ReviewData.RecommendationValues.Yes
            };

            
        }
    }
}
