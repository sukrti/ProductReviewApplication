using Review.API.Contracts;
using System.Collections.Generic;
using System;
using Review.API.Models;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http.Headers;
using static Review.API.Models.ReviewData;
using Microsoft.AspNetCore.Http;

namespace Review.API.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ReviewDBContext _context;
        public ReviewService(ReviewDBContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Gets data from in memory database and returns back to controller
        /// </summary>
        public async Task<IList<ReviewData>> GetAllReviews()
        {
            return await _context.Reviews.ToListAsync();
        }


        /// <summary>
        /// Gets new Review data from controller, add to the in memory database and returns back to controller
        /// </summary>
        public async Task Add(ReviewData newItem)
        {
            _context.Reviews.Add(newItem);
            await _context.SaveChangesAsync();          
        }

        /// <summary>
        /// Calls the api "/api/Review" to Get all the stored reviews data. Calculates AverageScore and 
        /// percentage of Recommendation and returns back to controller
        /// </summary>

        public async Task<ReviewSummaryData> GetSummary(string hostvalue)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);
            string path = "https://" + hostvalue + "/api/Review";
            var reviewsummary = new ReviewSummaryData();         
            List<ReviewData> ReviewData = null;
            int recommend_score = 0;
            decimal sum = 0;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync();
                ReviewData = JsonConvert.DeserializeObject<List<ReviewData>>(jsonString.Result);
                if (ReviewData != null)
                {
                    foreach (var item in ReviewData)
                    {
                        int score = item.Rating;

                        switch (score)
                        {
                            case 1:
                                sum += score * 1;
                                break;
                            case 2:
                                sum += score * 2;
                                break;
                            case 3:
                                sum += score * 3;
                                break;
                            case 4:
                                sum += score * 4;
                                break;
                            case 5:
                                sum += score * 5;
                                break;

                        }
                        var recommend = item.Recommendation;
                        if (recommend == RecommendationValues.Yes)
                        {
                            recommend_score += 100;
                        }
                        else if (recommend == RecommendationValues.No)
                        {
                            recommend_score += 0;
                        }
                        else
                        {
                            recommend_score += 50;
                        }
                    }
                    double averagescore = (double)(sum / 5);
                    double recommendation = (double)(recommend_score / 5);

                    reviewsummary = new ReviewSummaryData
                    {

                        AverageScore = (decimal)averagescore,
                        Recomendation = (recommendation + "%").ToString()

                    };

                    _context.ReviewSummary.Add(reviewsummary);
                    await _context.SaveChangesAsync();
                }
            }
            return reviewsummary;
        }

        /// <summary>
        /// Model of ReviewData
        /// </summary>
        public static ReviewData ReviewItemDTO(ReviewData ReviewItemDTO)=>
        
          new ReviewData
            {
                Id = Guid.NewGuid(),
                Title = ReviewItemDTO.Title,
                Rating = ReviewItemDTO.Rating,
                Comment = ReviewItemDTO.Comment,
                Recommendation = ReviewItemDTO.Recommendation
            };
        /// <summary>
        /// Model of Summary of review data
        /// </summary>
        public static  ReviewSummaryData ReviewSummaryData(ReviewSummaryData ReviewSummaryData) =>
           new ReviewSummaryData
           {
               Id= Guid.NewGuid(),
               AverageScore = ReviewSummaryData.AverageScore,
               Recomendation = ReviewSummaryData.Recomendation

           };
    }
}
