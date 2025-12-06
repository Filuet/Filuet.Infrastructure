using Filuet.Infrastructure.Abstractions.Enums;

namespace Filuet.Infrastructure.Abstractions.Business.Models
{
    public class ParcelLocker
    {
        public string Code { get; set; }
        public Country Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string FreightCode { get; set; }
        public string WarehouseCode { get; set; }
        public string Name { get; set; }
        public override string ToString()
            => $"{Address} ({Name} - #{Code})";
    }
}