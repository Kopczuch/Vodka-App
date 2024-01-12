using Konefeld.Kopiec.VodkaApp.Core;

namespace Konefeld.Kopiec.VodkaApp.Interfaces
{
    public interface IVodkaDto
    {
        string Name { get; set; }
        int ProducerId { get; set; }
        VodkaType Type { get; set; }
        double AlcoholPercentage { get; set; }
        double VolumeInLiters { get; set; }
        double Price { get; set; }
        string? FlavourProfile { get; set; }
    }
}
