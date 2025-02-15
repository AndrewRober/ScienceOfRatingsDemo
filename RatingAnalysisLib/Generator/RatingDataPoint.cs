using System;

namespace RatingAnalysisLib
{
    public class RatingDataPoint
    {
        public DateTime Timestamp { get; set; }
        public double Rating { get; set; }

        public RatingDataPoint(DateTime timestamp, double rating)
        {
            Timestamp = timestamp;
            Rating = rating;
        }
    }
}
