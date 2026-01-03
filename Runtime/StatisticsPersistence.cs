using System.Collections.Generic;
using System.Linq;
using Calluna.Persistence;
using UnityEngine;

namespace Calluna.Statistics.Persistence
{
    public class StatisticsPersistence : DataSaveLoader<StatisticsData>
    {
        [SerializeField] private string _dataId = "Statistics";

        public override string DataId => _dataId;

        private readonly Dictionary<string, EntryDataConverter> _converters =
            new Dictionary<string, EntryDataConverter>();

        private StatisticsData _loadedData;

        public void AddConverter(EntryDataConverter converter)
        {
            _converters.Add(converter.Id.Id, converter);
            if (_loadedData == null)
                return;
            StatisticsEntryData entryData = _loadedData.Entries.FirstOrDefault(d => d.Id == converter.Id.Id);
            if (entryData != null)
                converter.TryLoad(entryData);
        }

        protected override void HandleLoadedData(StatisticsData data)
        {
            _loadedData = data;
            foreach (StatisticsEntryData entryData in data.Entries)
            {
                if (!_converters.TryGetValue(entryData.Id, out EntryDataConverter converter))
                    continue;
                if (!converter.TryLoad(entryData))
                    Debug.LogWarning($"Failed to load statistics entry data for id {entryData.Id}");
            }
        }

        protected override StatisticsData GetDefaultData()
        {
            return new StatisticsData() { Entries = new List<StatisticsEntryData>() };
        }

        protected override StatisticsData GetData()
        {
            List<StatisticsEntryData> entriesData = new List<StatisticsEntryData>();
            
            foreach (EntryDataConverter converter in _converters.Values)
            {
                entriesData.Add(converter.ToData());
            }

            return new StatisticsData() { Entries = entriesData };
        }
    }
}