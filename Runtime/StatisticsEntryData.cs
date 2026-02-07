using System;
using Newtonsoft.Json.Linq;

namespace Calluna.Statistics.Persistence
{
    public class StatisticsEntryData
    {
        public string Id;
        public JToken Value;
    }
}