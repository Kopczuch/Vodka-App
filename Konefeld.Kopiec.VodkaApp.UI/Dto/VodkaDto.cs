using Konefeld.Kopiec.VodkaApp.Core;
using Konefeld.Kopiec.VodkaApp.Interfaces;

namespace Konefeld.Kopiec.VodkaApp.UI.Dto
{
    public class VodkaDto : IVodkaDto
    {
        public VodkaDto()
        {
            Name = string.Empty;
        }

        public VodkaDto(string name, int producerId, VodkaType vodkaType, double alcoholPercentage,
            double volumeInLiters, double price, string? flavourProfile)
        {
            Name = name;
            ProducerId = producerId;
            Type = vodkaType;
            AlcoholPercentage = alcoholPercentage;
            VolumeInLiters = volumeInLiters;
            Price = price;
            FlavourProfile = flavourProfile;
        }

        public string Name { get; set; }
        public int ProducerId { get; set; }
        public VodkaType Type { get; set; }
        public double AlcoholPercentage { get; set; }
        public double VolumeInLiters { get; set; }
        public double Price { get; set; }
        public string? FlavourProfile { get; set; }
    }
}
