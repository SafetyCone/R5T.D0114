using System;

using R5T.D0114.A001;


namespace System
{
    public static class IServiceAggregationIncrementExtensions
    {
        public static T FillFrom<T>(this T aggregation,
            IServiceAggregationIncrement other)
            where T : IServiceAggregationIncrement
        {
            throw new NotImplementedException();
        }
    }
}