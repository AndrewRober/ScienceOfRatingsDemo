using System;

namespace RatingAnalysisLib
{
    public class RatingDataPoint
    {
        public DateTime Timestamp { get; set; }
        public double Rating { get; set; }
        public double Weight { get; set; }  // New field for weight

        public RatingDataPoint(DateTime timestamp, double rating, double weight = 1.0)
        {
            Timestamp = timestamp;
            Rating = rating;
            Weight = weight;
        }
    }

}
