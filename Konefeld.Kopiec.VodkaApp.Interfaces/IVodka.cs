using Konefeld.Kopiec.VodkaApp.Core;

namespace Konefeld.Kopiec.VodkaApp.Interfaces
{
    public interface IVodka
    {
        int Id { get; set; }
        string Name { get; set; }
        IProducer Producer { get; set; }
        VodkaType Type { get; set; }
        double AlcoholPercentage { get; set; }
        double VolumeInLiters { get; set; }
        double Price { get; set; }
        string? FlavourProfile { get; set; }
    }
}
