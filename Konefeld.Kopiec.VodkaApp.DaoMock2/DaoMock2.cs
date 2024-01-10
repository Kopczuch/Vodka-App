using Konefeld.Kopiec.VodkaApp.Core;
using Konefeld.Kopiec.VodkaApp.DaoMock2.BO;
using Konefeld.Kopiec.VodkaApp.Interfaces;

namespace Konefeld.Kopiec.VodkaApp.DaoMock2
{
    public class DaoMock2 : IDao
    {
        private readonly IList<IProducer> _producers;
        private readonly IList<IVodka> _vodkas;

        public DaoMock2()
        {
            _producers = new List<IProducer>
            {
                new Producer() { Id = 1, Name = "BZK Alco", Description = "Potatoes, Grain, anything you'll list" },
                new Producer() { Id = 2, Name = "Stock Spirits Group", Description = "Best in the business" }
            };

            _vodkas = new List<IVodka>
            {
                new Vodka() { Id = 1, Producer = _producers[0], Name = "Wataha Wódka", Type = VodkaType.Grain, AlcoholPercentage = 36.5},
                new Vodka() { Id = 2, Producer = _producers[1], Name = "Stock", Type = VodkaType.Grain, AlcoholPercentage = 42.2},
                new Vodka() { Id = 3, Producer = _producers[1], Name = "Finlandia", Type = VodkaType.Grain, AlcoholPercentage = 33.3}
            };
        }

        public IVodka CreateVodka()
        {
            return new Vodka();
        }

        public IEnumerable<IVodka> GetAllVodkas()
        {
            return _vodkas;
        }

        public IProducer CreateProducer()
        {
            return new Producer();
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return _producers;
        }
    }
}