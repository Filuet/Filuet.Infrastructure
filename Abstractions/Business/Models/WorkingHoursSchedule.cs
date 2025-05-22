using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Helpers;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    public class WorkingHoursSchedule
    {
        [JsonPropertyName("Mon")]
        public IEnumerable<WorkingHoursSlot> Monday { get; set; }
        [JsonPropertyName("Tue")]
        public IEnumerable<WorkingHoursSlot> Tuesday { get; set; }
        [JsonPropertyName("Wed")]
        public IEnumerable<WorkingHoursSlot> Wednesday { get; set; }
        [JsonPropertyName("Thu")]
        public IEnumerable<WorkingHoursSlot> Thursday { get; set; }
        [JsonPropertyName("Fri")]
        public IEnumerable<WorkingHoursSlot> Friday { get; set; }
        [JsonPropertyName("Sat")]
        public IEnumerable<WorkingHoursSlot> Saturday { get; set; }
        [JsonPropertyName("Sun")]
        public IEnumerable<WorkingHoursSlot> Sunday { get; set; }
    }

    public class WorkingHoursSlot
    {
        [JsonPropertyName("from")]
        public string From { get; set; }
        [JsonPropertyName("to")]
        public string To { get; set; }
        [JsonPropertyName("mode")]
        public string Mode { get; set; }

        [JsonIgnore]
        public TimeSpan FromOfDay => TimeSpan.Parse(From);

        [JsonIgnore]
        public TimeSpan ToOfDay => TimeSpan.Parse(To);

        [JsonIgnore]
        public AscKioskMode KioskMode =>
            string.Equals(Mode, "AAAS", StringComparison.InvariantCultureIgnoreCase) ? AscKioskMode.AAAS: EnumHelpers.GetValueFromCode<AscKioskMode>(Mode);
    }
}