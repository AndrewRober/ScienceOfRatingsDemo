using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RatingAnalysisLib.AverageAggregators
{
    public enum AveragingMethod
    {
        SimpleAverage,
        WeightedAverage,
        BayesianAverage,
        MedianScore,
        TruncatedMean,
        ExponentialMovingAverage,
        WilsonScoreInterval,
        GiniBasedReviewSpread,
        RecencyAdjustedRating,
        GeometricMean
    }

    /// <summary>
    /// Implements the Simple Average calculation method.
    /// </summary>
    public static class SimpleAverageCalculator
    {
        /// <summary>
        /// Calculates the simple average from a list of rating data points.
        /// </summary>
        /// <param name="dataPoints">The list of rating data points.</param>
        /// <returns>The average rating.</returns>
        public static double CalculateAveragePoint(List<RatingDataPoint> dataPoints)
        {
            if (dataPoints == null || dataPoints.Count == 0)
                return 0; // Default fallback if no data

            return dataPoints.Average(dp => dp.Rating);
        }

        /// <summary>
        /// Calculates the simple moving average over time.
        /// </summary>
        /// <param name="dataPoints">The list of rating data points.</param>
        /// <returns>A list of averaged points over time.</returns>
        public static List<RatingDataPoint> CalculateAverageOverTime(List<RatingDataPoint> dataPoints)
        {
            if (dataPoints == null || dataPoints.Count == 0)
                return new List<RatingDataPoint>();

            List<RatingDataPoint> averagedPoints = new List<RatingDataPoint>();

            for (int i = 1; i <= dataPoints.Count; i++)
            {
                double avg = dataPoints.Take(i).Average(dp => dp.Rating);
                averagedPoints.Add(new RatingDataPoint(dataPoints[i - 1].Timestamp, avg, dataPoints[i - 1].Weight));
            }

            return averagedPoints;
        }
    }

}
