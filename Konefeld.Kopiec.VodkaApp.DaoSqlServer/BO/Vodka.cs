using Konefeld.Kopiec.VodkaApp.Core;
using Konefeld.Kopiec.VodkaApp.Interfaces;

namespace Konefeld.Kopiec.VodkaApp.DaoSqlServer.BO
{
    public class Vodka : IVodka
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public VodkaType Type { get; set; }
        public double AlcoholPercentage { get; set; }
        public double VolumeInLiters { get; set; }
        public double Price { get; set; }
        public string? FlavourProfile { get; set; }


        public int ProducerId { get; set; }
        public IProducer Producer { get; set; }

    }
}
