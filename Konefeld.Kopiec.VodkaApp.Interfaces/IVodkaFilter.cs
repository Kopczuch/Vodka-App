namespace Konefeld.Kopiec.VodkaApp.Interfaces
{
    public interface IVodkaFilter
    {
        string SearchTerm { get; set; }
        double Volume { get; set; }
        double Alcohol { get; set; }
        double PriceLowerBound { get; set; }
        double PriceUpperBound { get; set; }
        string Type { get; set; }
        int ProducerId { get; set; }
    }
}
