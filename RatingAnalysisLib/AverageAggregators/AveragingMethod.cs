using System;
using System.Collections.Generic;
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


}
