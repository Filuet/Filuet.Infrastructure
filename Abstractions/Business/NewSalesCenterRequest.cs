using Filuet.Infrastructure.Abstractions.Enums;

namespace Filuet.Infrastructure.Abstractions.Business
{
    public class NewSalesCenterRequest
    {
        public Country Country { get; set; }
        public string City { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longtitite { get; set; }
        public string WorkingHours { get; set; } = "<p>24 hours a day</p>";
    }
}
