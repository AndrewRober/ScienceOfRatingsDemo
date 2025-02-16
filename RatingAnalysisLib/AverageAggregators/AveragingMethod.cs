//using MathNet.Numerics.Distributions;

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

    /// <summary>
    /// Computes the Bayesian Average to prevent small review counts from skewing results.
    /// Uses a prior belief and a minimum number of reviews to balance influence.
    /// </summary>
    public static class BayesianAverageCalculator
    {
        /// <summary>
        /// Computes the Bayesian Average for a given set of rating data points.
        /// </summary>
        /// <param name="dataPoints">List of rating data points.</param>
        /// <param name="prior">The prior expectation (initial assumed average).</param>
        /// <param name="m">The weight of the prior (number of equivalent prior votes).</param>
        /// <returns>The computed Bayesian Average.</returns>
        public static double ComputeBayesianAverage(List<RatingDataPoint> dataPoints, double prior, double m)
        {
            if (dataPoints == null || dataPoints.Count == 0)
                return prior; // Return prior if no data is available

            double weightedSum = dataPoints.Sum(p => p.Rating * p.Weight);
            double totalWeight = dataPoints.Sum(p => p.Weight);

            return (weightedSum + (prior * m)) / (totalWeight + m);
        }

        /// <summary>
        /// Computes the Bayesian Average over time, generating a trend of Bayesian-adjusted scores.
        /// </summary>
        /// <param name="dataPoints">List of rating data points.</param>
        /// <param name="prior">The prior expectation (initial assumed average).</param>
        /// <param name="m">The weight of the prior (number of equivalent prior votes).</param>
        /// <returns>A list of Bayesian Average points computed at each timestamp.</returns>
        public static List<RatingDataPoint> ComputeBayesianAverageOverTime(List<RatingDataPoint> dataPoints, double prior, double m)
        {
            if (dataPoints == null || dataPoints.Count == 0)
                return new List<RatingDataPoint>();

            List<RatingDataPoint> result = new List<RatingDataPoint>();
            List<RatingDataPoint> accumulatedData = new List<RatingDataPoint>();

            foreach (var point in dataPoints.OrderBy(p => p.Timestamp))
            {
                accumulatedData.Add(point);
                double avg = ComputeBayesianAverage(accumulatedData, prior, m);
                result.Add(new RatingDataPoint(point.Timestamp, avg, point.Weight));
            }

            return result;
        }
    }

    /// <summary>
    /// Provides functionality to compute the median rating from a given dataset.
    /// The median helps mitigate the effect of extreme values, making it useful for skewed data.
    /// </summary>
    public static class MedianScoreCalculator
    {
        /// <summary>
        /// Computes the median score from a list of rating data points.
        /// </summary>
        /// <param name="dataPoints">List of rating data points.</param>
        /// <returns>The median rating value.</returns>
        public static double ComputeMedianScore(List<RatingDataPoint> dataPoints)
        {
            if (dataPoints == null || dataPoints.Count == 0)
                throw new ArgumentException("Data points cannot be null or empty.");

            var sortedRatings = dataPoints.Select(dp => dp.Rating).OrderBy(r => r).ToList();
            int count = sortedRatings.Count;

            if (count % 2 == 1)
            {
                return sortedRatings[count / 2]; // Odd count, return the middle value.
            }
            else
            {
                return (sortedRatings[(count / 2) - 1] + sortedRatings[count / 2]) / 2.0; // Even count, return the average of two middle values.
            }
        }

        /// <summary>
        /// Computes the median score at each point in time to create a time-series of median values.
        /// </summary>
        /// <param name="dataPoints">List of rating data points.</param>
        /// <returns>List of median scores computed over time.</returns>
        public static List<RatingDataPoint> ComputeMedianScoreOverTime(List<RatingDataPoint> dataPoints)
        {
            if (dataPoints == null || dataPoints.Count == 0)
                throw new ArgumentException("Data points cannot be null or empty.");

            List<RatingDataPoint> medianOverTime = new List<RatingDataPoint>();
            List<RatingDataPoint> accumulated = new List<RatingDataPoint>();

            foreach (var point in dataPoints.OrderBy(dp => dp.Timestamp))
            {
                accumulated.Add(point);
                double median = ComputeMedianScore(accumulated);
                medianOverTime.Add(new RatingDataPoint(point.Timestamp, median, point.Weight));
            }

            return medianOverTime;
        }
    }

    /// <summary>
    /// Provides functionality to compute a truncated mean (trimmed average),
    /// which excludes extreme values before averaging to reduce the effect of outliers.
    /// </summary>
    public static class TruncatedMeanCalculator
    {
        /// <summary>
        /// Computes the truncated mean by removing a percentage of extreme values before averaging.
        /// </summary>
        /// <param name="dataPoints">List of rating data points.</param>
        /// <param name="trimPercent">The percentage of extreme values to remove (0.0 - 0.5 range).</param>
        /// <returns>The truncated mean rating value.</returns>
        public static double ComputeTruncatedMean(List<RatingDataPoint> dataPoints, double trimPercent)
        {
            if (dataPoints == null || dataPoints.Count == 0)
                throw new ArgumentException("Data points cannot be null or empty.");
            if (trimPercent < 0.0 || trimPercent > 0.5)
                throw new ArgumentException("Trim percentage must be between 0.0 and 0.5 (50% max).");

            var sortedRatings = dataPoints.Select(dp => dp.Rating).OrderBy(r => r).ToList();
            int count = sortedRatings.Count;
            int trimCount = (int)Math.Round(count * trimPercent); // Ensure trimming is applied correctly

            int maxTrim = (count - 1) / 2; // Ensure at least one value remains
            trimCount = Math.Min(trimCount, maxTrim);

            // Remove extreme values
            var trimmedRatings = sortedRatings.Skip(trimCount).Take(count - 2 * trimCount).ToList();

            return trimmedRatings.Count > 0 ? trimmedRatings.Average() : sortedRatings.Average();
        }

        /// <summary>
        /// Computes the truncated mean over time, generating a time-series of truncated averages.
        /// </summary>
        /// <param name="dataPoints">List of rating data points.</param>
        /// <param name="trimPercent">The percentage of extreme values to remove at each step.</param>
        /// <returns>List of truncated mean scores computed over time.</returns>
        public static List<RatingDataPoint> ComputeTruncatedMeanOverTime(List<RatingDataPoint> dataPoints, double trimPercent)
        {
            if (dataPoints == null || dataPoints.Count == 0)
                throw new ArgumentException("Data points cannot be null or empty.");
            if (trimPercent < 0.0 || trimPercent > 0.5)
                throw new ArgumentException("Trim percentage must be between 0.0 and 0.5 (50% max).");

            List<RatingDataPoint> truncatedOverTime = new List<RatingDataPoint>();
            List<RatingDataPoint> accumulated = new List<RatingDataPoint>();

            foreach (var point in dataPoints.OrderBy(dp => dp.Timestamp))
            {
                accumulated.Add(point);
                double truncatedMean = ComputeTruncatedMean(accumulated, trimPercent);
                truncatedOverTime.Add(new RatingDataPoint(point.Timestamp, truncatedMean, point.Weight));
            }

            return truncatedOverTime;
        }
    }

    /// <summary>
    /// Computes an Exponential Moving Average (EMA) to give more weight to recent data points.
    /// The influence of older points decays exponentially over time.
    /// </summary>
    public static class ExponentialMovingAverageCalculator
    {
        /// <summary>
        /// Computes the Exponential Moving Average (EMA) for a given dataset.
        /// </summary>
        /// <param name="dataPoints">List of rating data points.</param>
        /// <param name="alpha">Smoothing factor (0 < alpha ≤ 1). Higher values prioritize recent data.</param>
        /// <returns>The computed EMA value.</returns>
        public static double ComputeExponentialMovingAverage(List<RatingDataPoint> dataPoints, double alpha)
        {
            if (dataPoints == null || dataPoints.Count == 0)
                throw new ArgumentException("Data points cannot be empty.");

            double ema = dataPoints[0].Rating; // Initialize EMA with the first rating

            foreach (var point in dataPoints.Skip(1))
            {
                ema = alpha * point.Rating + (1 - alpha) * ema;
            }

            return ema;
        }

        /// <summary>
        /// Computes the Exponential Moving Average over time, returning a series of points.
        /// </summary>
        /// <param name="dataPoints">List of rating data points.</param>
        /// <param name="alpha">Smoothing factor (0 < alpha ≤ 1). Higher values prioritize recent data.</param>
        /// <returns>List of computed EMA points over time.</returns>
        public static List<RatingDataPoint> ComputeExponentialMovingAverageOverTime(List<RatingDataPoint> dataPoints, double alpha)
        {
            if (dataPoints == null || dataPoints.Count == 0)
                return new List<RatingDataPoint>();

            List<RatingDataPoint> emaPoints = new List<RatingDataPoint>();
            double ema = dataPoints[0].Rating;

            foreach (var point in dataPoints)
            {
                ema = alpha * point.Rating + (1 - alpha) * ema;
                emaPoints.Add(new RatingDataPoint(point.Timestamp, ema, point.Weight));
            }

            return emaPoints;
        }
    }

    /// <summary>
    /// Computes a Wilson Score confidence interval lower bound to rank ratings with uncertainty adjustment.
    /// </summary>
    public static class WilsonScoreCalculator
    {
        /// <summary>
        /// Computes the Wilson score lower bound for a given dataset.
        /// </summary>
        /// <param name="positiveRatings">Number of positive ratings (e.g., 4-5 stars).</param>
        /// <param name="totalRatings">Total number of ratings.</param>
        /// <param name="confidenceLevel">Confidence level (e.g., 0.95 for 95%).</param>
        /// <returns>Wilson score lower bound.</returns>
        public static double ComputeWilsonScore(int positiveRatings, int totalRatings, double confidenceLevel = 0.95)
        {
            if (totalRatings == 0) return 0; // No data → Worst possible score

            // Get Z-score for confidence level
            double z = GetZScore(confidenceLevel);

            // Proportion of positive ratings
            double p = (double)positiveRatings / totalRatings;

            // Wilson lower bound formula
            double numerator = p + (z * z) / (2 * totalRatings) - z * Math.Sqrt((p * (1 - p) + (z * z) / (4 * totalRatings)) / totalRatings);
            double denominator = 1 + (z * z) / totalRatings;

            return numerator / denominator;
        }

        /// <summary>
        /// Computes Wilson scores over time for trend visualization.
        /// </summary>
        /// <param name="dataPoints">List of rating data points.</param>
        /// <param name="confidenceLevel">Confidence level (e.g., 0.95 for 95%).</param>
        /// <returns>List of Wilson score points over time.</returns>
        public static List<RatingDataPoint> ComputeWilsonScoreOverTime(List<RatingDataPoint> dataPoints, double confidenceLevel = 0.95)
        {
            List<RatingDataPoint> result = new List<RatingDataPoint>();
            int totalRatings = 0, positiveRatings = 0;

            foreach (var point in dataPoints.OrderBy(p => p.Timestamp))
            {
                totalRatings++;
                if (point.Rating >= 7) // Assuming 7+ is a "positive" rating
                    positiveRatings++;

                double wilsonScore = ComputeWilsonScore(positiveRatings, totalRatings, confidenceLevel);
                result.Add(new RatingDataPoint(point.Timestamp, wilsonScore * 10, point.Weight)); // Rescale Wilson score (0-1) → (0-10)
            }

            return result;
        }

        /// <summary>
        /// Gets the Z-score (critical value) for a given confidence level.
        /// </summary>
        /// <param name="confidenceLevel">Confidence level (e.g., 0.95 for 95%).</param>
        /// <returns>Corresponding Z-score.</returns>
        public static double GetZScore(double confidenceLevel)
        {
            //return Normal.InvCDF(0, 1, 1 - (1 - confidenceLevel) / 2);
            return ApproximateZScore(confidenceLevel);
        }

        public static double ApproximateZScore(double confidenceLevel)
        {
            if (confidenceLevel <= 0 || confidenceLevel >= 1)
                throw new ArgumentException("Confidence level must be between 0 and 1 (e.g., 0.95 for 95%)");

            // Approximate z-score using Beasley-Springer-Moro method
            double[] coefficients = { 2.515517, 0.802853, 0.010328 };
            double[] denominators = { 1.432788, 0.189269, 0.001308 };

            double p = 1 - (1 + confidenceLevel) / 2; // Tail probability
            double t = Math.Sqrt(-2 * Math.Log(p)); // Transformation

            double numerator = (coefficients[0] + coefficients[1] * t + coefficients[2] * t * t);
            double denominator = (1 + denominators[0] * t + denominators[1] * t * t + denominators[2] * t * t * t);

            return t - (numerator / denominator);
        }

    }


}
