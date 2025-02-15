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
        public static double ComputeAveragePoint(List<RatingDataPoint> dataPoints)
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
        public static List<RatingDataPoint> ComputeAverageOverTime(List<RatingDataPoint> dataPoints)
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

    /// <summary>
    /// Computes the **Weighted Average** of rating data points.
    /// Each rating has a weight, influencing its impact on the final score.
    /// </summary>
    public class WeightedAverageCalculator
    {
        /// <summary>
        /// Computes the **Weighted Average** from a list of rating data points.
        /// </summary>
        /// <param name="dataPoints">List of RatingDataPoint (includes rating and weight).</param>
        /// <returns>The computed weighted average.</returns>
        public static double ComputeWeightedAverage(List<RatingDataPoint> dataPoints)
        {
            if (dataPoints == null || dataPoints.Count == 0)
                return 0.0;

            double weightedSum = dataPoints.Sum(dp => dp.Rating * dp.Weight);
            double totalWeight = dataPoints.Sum(dp => dp.Weight);

            return (totalWeight > 0) ? weightedSum / totalWeight : 0.0;
        }

        /// <summary>
        /// Computes the **weighted average at each timestamp**, producing a trend over time.
        /// </summary>
        /// <param name="dataPoints">List of RatingDataPoint.</param>
        /// <returns>List of computed weighted averages over time.</returns>
        public static List<RatingDataPoint> ComputeWeightedAverageOverTime(List<RatingDataPoint> dataPoints)
        {
            if (dataPoints == null || dataPoints.Count == 0)
                return new List<RatingDataPoint>();

            // Ensure data is sorted by timestamp
            var sortedData = dataPoints.OrderBy(dp => dp.Timestamp).ToList();
            List<RatingDataPoint> computedAverages = new List<RatingDataPoint>();

            for (int i = 1; i <= sortedData.Count; i++)
            {
                List<RatingDataPoint> subset = sortedData.Take(i).ToList();
                double avg = ComputeWeightedAverage(subset);
                computedAverages.Add(new RatingDataPoint(sortedData[i - 1].Timestamp, avg, 1)); // Neutral weight for computed points
            }

            return computedAverages;
        }
    }



}
