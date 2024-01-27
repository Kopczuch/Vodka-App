using Konefeld.Kopiec.VodkaApp.Interfaces;

namespace Konefeld.Kopiec.VodkaApp.UI.Dto
{
    public class VodkaFilter : IVodkaFilter
    {
        public string SearchTerm { get; set; } = string.Empty;
        public double Volume { get; set; }
        public double Alcohol { get; set; }
        public double PriceLowerBound { get; set; }
        public double PriceUpperBound { get; set; }
        public string Type { get; set; } = string.Empty;
        public int ProducerId { get; set; }
    }
}
