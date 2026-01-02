using Calluna.DI;
using UnityEngine;

namespace Calluna.Statistics.Persistence
{
    public abstract class StatisticEntryPersister<T> : MonoBehaviour, Injectable, Initializable
    {
        [SerializeField] private StatisticId _id;
        private Statistics _statistics;
        private StatisticsPersistence _statisticsPersistence;
        
        public void Inject(Resolver resolver)
        {
            _statistics = resolver.Resolve<Statistics>();
            _statisticsPersistence = resolver.Resolve<StatisticsPersistence>();
        }

        public void Initialize()
        {
            StatisticsEntry<T> entry = _statistics.GetOrCreateEntry<T>(_id);
            _statisticsPersistence.AddConverter(new EntryDataConverter<T>(entry));
        }
    }
}