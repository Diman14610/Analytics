﻿namespace Analytics.Shared.Analytics.Comparator
{
    public class ComparisonResult
    {
        public ComparisonResult(string assertionName, double score, int numberSuccessfulBlocks, ComparisonPriority comparisonMode)
        {
            AssertionName = assertionName;
            Score = score;
            NumberSuccessfulBlocks = numberSuccessfulBlocks;
            ComparisonMode = comparisonMode;
        }

        public string AssertionName { get; }

        public double Score { get; }

        public int NumberSuccessfulBlocks { get; }

        public ComparisonPriority ComparisonMode { get; }
    }
}
