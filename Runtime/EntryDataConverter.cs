using Calluna.Persistence;

namespace Calluna.Statistics.Persistence
{
    public abstract class EntryDataConverter
    {
        public abstract StatisticId Id { get; }
        public abstract StatisticsEntryData ToData();
        public abstract bool TryLoad(StatisticsEntryData data);
    }

    public class EntryDataConverter<T> : EntryDataConverter
    {
        public override StatisticId Id => _entry.Id;
        
        private readonly StatisticsEntry<T> _entry;
        private readonly JsonSerializer _serializer;

        public EntryDataConverter(StatisticsEntry<T> entry, JsonSerializer serializer)
        {
            _entry = entry;
            _serializer = serializer;
        }

        public StatisticsEntryData ToConcreteData()
        {
            return new StatisticsEntryData() { Id = _entry.Id.Id, Value = _serializer.Serialize(_entry.Value.Value) };
        }

        public override StatisticsEntryData ToData()
        {
            return ToConcreteData();
        }

        public override bool TryLoad(StatisticsEntryData data)
        {
            if (_entry.Id != data.Id)
                return false;
            _entry.Value.Value = _serializer.Deserialize<T>(data.Value);
            return true;
        }
    }
}