using Calluna.Persistence;

namespace Calluna.Statistics.Persistence
{
    public abstract class EntryDataConverterCreator
    {
        public abstract EntryDataConverter Create(Statistics statistics, StatisticId id, JsonSerializer serializer);
    }
    
    public class EntryDataConverterCreator<T> : EntryDataConverterCreator
    {
        public override EntryDataConverter Create(Statistics statistics, StatisticId id, JsonSerializer serializer)
        {
            StatisticsEntry<T> entry = statistics.GetOrCreateEntry<T>(id);
            return new EntryDataConverter<T>(entry, serializer);
        }
    }
}