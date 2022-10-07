using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Review.API.Models
{
    public class ReviewData
    {
        public enum RecommendationValues { Yes, No, Maybe }

        [Required]
        public Guid Id { get; set; }
        private int Score { get; set; }

        [Required]
        public string Title { get; set; }
        public string? Comment { get; set; }

        [Required]
        public RecommendationValues Recommendation { get; set; }

        [Required(ErrorMessage = "Required")]
        [Range(1, 5, ErrorMessage = "Please choose value between 1 and 5")]
        public int Rating
        {
            set { if ((value > 0) && (value<=5)) this.Score = value; }
            get { return this.Score; }
        }
    }
}
