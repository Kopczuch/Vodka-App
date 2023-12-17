using Konefeld.Kopiec.VodkaApp.Core;
using Konefeld.Kopiec.VodkaApp.Interfaces;

namespace Konefeld.Kopiec.VodkaApp.DaoMock1.BO
{
    public class Vodka : IVodka
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IProducer Producer { get; set; }
        public VodkaType Type { get; set; }
        public double AlcoholPercentage { get; set; }
    }
}
