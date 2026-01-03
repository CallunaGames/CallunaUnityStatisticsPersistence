using Calluna.DI;
using Calluna.Persistence;

namespace Calluna.Statistics.Persistence
{
    public class EntryDataConverterFactory : Injectable
    {
        private Statistics _statistics;
        private JsonSerializer _serializer;
        
        public void Inject(Resolver resolver)
        {
            _statistics = resolver.Resolve<Statistics>();
            _serializer = resolver.Resolve<JsonSerializer>();
        }

        public EntryDataConverter<T> Create<T>(StatisticId statisticId)
        {
            StatisticsEntry<T> entry = _statistics.GetOrCreateEntry<T>(statisticId);
            return new EntryDataConverter<T>(entry, _serializer);
        }
    }
}