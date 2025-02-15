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


        private static double Clamp(double value, double min, double max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

    }
}
