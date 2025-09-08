using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Helpers;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    public class BotUser
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("language")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Language? Language { get; set; }
        [JsonPropertyName("country")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Country? Country { get; set; }
        [JsonPropertyName("channel")]
        public BotChannel Channel { get; set; }
        [JsonPropertyName("customerUid")]
        public string CustomerUid { get; set; }
        /// <summary>
        /// Language of the current request
        /// </summary>
        [JsonIgnore]
        public Language InqueryLanguage { get; set; }
        [JsonIgnore]
        public Language ReplyLanguage => Language ?? InqueryLanguage;

        public override string ToString()
            => $"{Channel.GetCode()} {Name} {Id}";
    }
}