using System;

namespace Calluna.Statistics.Persistence
{
    public abstract class StatisticsEntryData
    {
        public string Id;
        public abstract StatisticsEntry ToEntry(StatisticId id);
    }

    public class StatisticsEntryData<T> : StatisticsEntryData
    {
        public T Value;
        
        public override StatisticsEntry ToEntry(StatisticId id)
        {
            if (id == Id)
                throw new ArgumentException($"Mismatching ids {id} and {Id}");
            return new StatisticsEntry<T>(id, Value);
        }
    }
}