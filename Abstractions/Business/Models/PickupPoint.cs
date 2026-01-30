using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Helpers;
using System.Text.Json.Serialization;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    public class PickupPoint
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("country")]
        public Country Country { get; set; }
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("freightCode")]
        public string FreightCode { get; set; }
        [JsonPropertyName("warehouseCode")]
        public string WarehouseCode { get; set; }
        [JsonPropertyName("serviceName")]
        public string ServiceName { get; set; }

        public void Initialize(string data) {
            if (string.IsNullOrWhiteSpace(data))
                return;

            string[] blocks = data.Split(";", System.StringSplitOptions.RemoveEmptyEntries);

            foreach (var b in blocks) {
                if (b.StartsWith("code:"))
                    Code = b.Replace("code:", string.Empty);
                else if (b.StartsWith("country:"))
                    Country = EnumHelpers.GetValueFromCode<Country>(b.Replace("country:", string.Empty));
                else if (b.StartsWith("city:"))
                    City = b.Replace("city:", string.Empty);
                else if (b.StartsWith("address:"))
                    Address = b.Replace("address:", string.Empty).Replace("&#59;", ";");
                else if (b.StartsWith("freightCode:"))
                    FreightCode = b.Replace("freightCode:", string.Empty);
                else if (b.StartsWith("warehouseCode:"))
                    WarehouseCode = b.Replace("warehouseCode:", string.Empty);
                else if (b.StartsWith("serviceName:"))
                    ServiceName = b.Replace("serviceName:", string.Empty);
            }
        }

        [JsonIgnore]
        public bool IsSufficient
            => !string.IsNullOrWhiteSpace(Code) && !string.IsNullOrWhiteSpace(Address) && !string.IsNullOrWhiteSpace(City);

        [JsonIgnore]
        public Enums.PickupPoint? ParcelDeliveryService
        {
            get {
                string service = ServiceName.ToLower();
                if (service.StartsWith("3pl"))
                    return Enums.PickupPoint.PL3;

                return null;
            }
        }

        public string Serialize()
            => $"code:{Code};country:{Country.GetCode()};city:{City};address:{Address.Replace(";", "&#59;")};freightCode:{FreightCode};warehouseCode:{WarehouseCode};serviceName:{ServiceName}";

        public override string ToString()
            => $"{Address} ({ServiceName} - №{Code})";
    }
}