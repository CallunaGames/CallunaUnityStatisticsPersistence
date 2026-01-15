using System;
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

        public EntryDataConverter Create(StatisticId statisticId)
        {
            Type creatorType = typeof(EntryDataConverterCreator<>).MakeGenericType(statisticId.Type.Type);
            EntryDataConverterCreator creator = (EntryDataConverterCreator)Activator.CreateInstance(creatorType);
            return creator.Create(_statistics, statisticId, _serializer);
        }
    }
}