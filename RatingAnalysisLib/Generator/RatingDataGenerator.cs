using System;
using System.Collections.Generic;

namespace RatingAnalysisLib
{

    public static class RatingDataGenerator
    {
        private static readonly Random random = new Random();

        public static List<RatingDataPoint> GenerateRatings(
            RatingTrend trend,
            DateTime startTime,
            TimeSpan interval,
            int dataPoints)
        {
            List<RatingDataPoint> ratings = new List<RatingDataPoint>();
            DateTime currentTime = startTime;

            for (int i = 0; i < dataPoints; i++)
            {
                double rating = GenerateRating(trend, i, dataPoints);
                ratings.Add(new RatingDataPoint(currentTime, Clamp(rating, 1.0, 10.0)));
                currentTime = currentTime.Add(interval);
            }

            return ratings;
        }

        public static List<RatingDataPoint> GenerateRatings(RatingTrend trend, DateTime startTime,
            TimeSpan interval, int dataPoints, WeightGenerationMethod weightMethod)
        {
            List<RatingDataPoint> ratings = new List<RatingDataPoint>();
            DateTime currentTime = startTime;

            for (int i = 0; i < dataPoints; i++)
            {
                double rating = GenerateRating(trend, i, dataPoints);
                double weight = GenerateWeight(rating, i, dataPoints, weightMethod);
                ratings.Add(new RatingDataPoint(currentTime, Clamp(rating, 1.0, 10.0), weight));
                currentTime = currentTime.Add(interval);
            }

            return ratings;
        }


        private static double GenerateRating(RatingTrend trend, int t, int total)
        {
            double rating;
            switch (trend)
            {
                case RatingTrend.SteadyGood:
                    rating = random.NextDouble() * 2 + 8; // 8-10 range
                    break;
                case RatingTrend.SteadyBad:
                    rating = random.NextDouble() * 2 + 1; // 1-3 range
                    break;
                case RatingTrend.GoodWentBad:
                    rating = 10 - (t / (double)total) * 9; // Decline from 10 to 1
                    break;
                case RatingTrend.BadImproved:
                    rating = 1 + (t / (double)total) * 9; // Improve from 1 to 10
                    break;
                case RatingTrend.ExtremeSwings:
                    rating = random.Next(1, 11); // Completely random
                    break;
                case RatingTrend.FakeGood:
                    rating = (random.NextDouble() < 0.7) ? 10 : random.Next(3, 7);
                    break;
                case RatingTrend.FakeBad:
                    rating = (random.NextDouble() < 0.7) ? 1 : random.Next(6, 10);
                    break;
                case RatingTrend.GradualOscillation:
                    rating = 5 + 4 * Math.Sin(Math.PI * t / (total / 2)); // Smooth wave
                    break;
                case RatingTrend.SuddenDropRecovery:
                    int crisisPoint = total / 3;
                    rating = (t < crisisPoint) ? 9 - random.NextDouble() * 2 : 2 + (t - crisisPoint) / (double)(total - crisisPoint) * 8;
                    break;
                case RatingTrend.NewBusinessEffect:
                    rating = (t < total / 4) ? 9 - random.NextDouble() * 1.5 : random.Next(4, 10);
                    break;
                default:
                    rating = 5; // Fallback
                    break;
            }

            return Math.Round(rating, 1); // Ensure single precision (1 decimal place)
        }

        public static double GenerateWeight(double rating, int t, int total, WeightGenerationMethod method)
        {
            double baseWeight = random.NextDouble() * 4 + 1; // Random baseline weight (1.0 - 5.0)

            switch (method)
            {
                case WeightGenerationMethod.HighRatingMoreWeight:
                    baseWeight += (rating - 1) / 9 * 5; // More weight to higher ratings
                    break;

                case WeightGenerationMethod.LowRatingMoreWeight:
                    baseWeight += (10 - rating) / 9 * 5; // More weight to lower ratings
                    break;

                case WeightGenerationMethod.RecentMoreWeight:
                    baseWeight += ((double)(total - t) / total) * 5; // Newer reviews get more weight
                    break;

                case WeightGenerationMethod.OlderMoreWeight:
                    baseWeight += ((double)t / total) * 5; // Older reviews get more weight
                    break;

                default: // Random (default)
                    break;
            }

            return Math.Round(baseWeight, 2);
        }



        public static double Clamp(double value, double min, double max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

    }


}
