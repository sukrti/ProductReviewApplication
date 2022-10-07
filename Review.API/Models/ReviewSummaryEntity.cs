using System;

namespace Review.API.Models
{
    public class ReviewSummaryEntity
    {
        public Guid Id { get; set; }
        public double AverageScore { get; set; }
        public double Recomendation { get; set; }
    }
}
