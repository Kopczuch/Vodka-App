using Konefeld.Kopiec.VodkaApp.Interfaces;

namespace Konefeld.Kopiec.VodkaApp.UI.FilterObjects
{
    public class VodkaFilter : IVodkaFilter
    {
        public VodkaFilter()
        {
            SearchTerm = string.Empty;
            Type = string.Empty;
        }

        public string SearchTerm { get; set; }
        public double Volume { get; set; }
        public double Alcohol { get; set; }
        public double PriceLowerBound { get; set; }
        public double PriceUpperBound { get; set; }
        public string Type { get; set; }
        public int ProducerId { get; set; }
    }
}
