using Konefeld.Kopiec.VodkaApp.Core;
using Konefeld.Kopiec.VodkaApp.Interfaces;

namespace Konefeld.Kopiec.VodkaApp.UI.WEB.Models.FilterObjects
{
    public class VodkaFilter : IVodkaFilter
    {
        public string SearchTerm { get; set; }
        public double Volume { get; set; }
        public double Alcohol { get; set; }
        public double PriceLowerBound { get; set; }
        public double PriceUpperBound { get; set; }
        public string Type { get; set; }
        public int ProducerId { get; set; }
    }
}
