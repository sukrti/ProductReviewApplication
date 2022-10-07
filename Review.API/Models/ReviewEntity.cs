using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Review.API.Models
{
    public class ReviewEntity
    {
        public enum RecommendationValues { Yes,No,Maybe }
      
        public Guid Id { get; set; } 
        private int Score { get; set; }

        public string Title { get; set; } 

        public string Comment { get; set; }

        public RecommendationValues Recommendation { get; set; }

    }

   
}
