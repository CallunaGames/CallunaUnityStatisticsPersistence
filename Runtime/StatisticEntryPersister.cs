using Calluna.DI;
using UnityEngine;

namespace Calluna.Statistics.Persistence
{
    public class StatisticEntryPersister : MonoBehaviour, Injectable, Initializable
    {
        [SerializeField] private StatisticId _id;
        
        private StatisticsPersistence _statisticsPersistence;
        private EntryDataConverterFactory _converterFactory;
        
        public void Inject(Resolver resolver)
        {
            _statisticsPersistence = resolver.Resolve<StatisticsPersistence>();
            _converterFactory = resolver.Resolve<EntryDataConverterFactory>();
        }

        public void Initialize()
        {
            _statisticsPersistence.AddConverter(_converterFactory.Create(_id));
        }
    }
}