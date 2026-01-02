using System;
using UnityEngine;

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

        public EntryDataConverter(StatisticsEntry<T> entry)
        {
            _entry = entry;
        }

        public StatisticsEntryData<T> ToConcreteData()
        {
            return new StatisticsEntryData<T>() { Id = _entry.Id.Id, Value = _entry.Value.Value };
        }

        public override StatisticsEntryData ToData()
        {
            return ToConcreteData();
        }

        public override bool TryLoad(StatisticsEntryData data)
        {
            if (_entry.Id != data.Id)
                return false;
            if(data is not StatisticsEntryData<T> typedData)
                throw new ArgumentException($"Data has invalid type. Expected generic type of {typeof(T)}. Actual {data.GetType()}");
            _entry.Value.Value = typedData.Value;
            return true;
        }
    }
}