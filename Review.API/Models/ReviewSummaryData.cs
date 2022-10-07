using System;

namespace Review.API.Models
{
    public class ReviewSummaryData
    {
        public Guid Id { get; set; }
        public decimal AverageScore { get; set; }
        public string Recomendation { get; set; }
    }
}
