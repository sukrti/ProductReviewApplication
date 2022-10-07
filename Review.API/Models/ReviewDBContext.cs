using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Review.API.Models
{   public class ReviewDBContext:DbContext
    {
        public ReviewDBContext(DbContextOptions options)
            : base(options)  
        { }
        public DbSet<ReviewData> Reviews { get; set; } = null!;
        public DbSet<ReviewSummaryData> ReviewSummary { get; set; } = null!;
      
    }
}
