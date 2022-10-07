using Review.API.Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace Review.API.Contracts
{
    public interface IReviewService
    {
        Task <IList<ReviewData>> GetAllReviews();

      
        //Task<ReviewSummaryData> GetReviewSummaryById(long id);
        Task  Add(ReviewData newItem);
        Task<ReviewSummaryData> GetSummary(string value);
        static ReviewData ReviewItemDTO(ReviewData ReviewItemDTO)=> throw new NotImplementedException();

        static ReviewSummaryData ReviewSummaryData(ReviewSummaryData ReviewSummaryData)=> throw new NotImplementedException();


    }
}
