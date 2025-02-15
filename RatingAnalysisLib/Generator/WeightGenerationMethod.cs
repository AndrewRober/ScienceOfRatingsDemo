namespace RatingAnalysisLib
{
    public enum WeightGenerationMethod
    {
        Random,                // Default
        HighRatingMoreWeight,  // Higher ratings get more weight
        LowRatingMoreWeight,   // Lower ratings get more weight
        RecentMoreWeight,      // More weight for recent reviews
        OlderMoreWeight        // More weight for older reviews
    }


}
