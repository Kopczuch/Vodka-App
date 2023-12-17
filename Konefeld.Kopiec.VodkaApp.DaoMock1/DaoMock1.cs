using Konefeld.Kopiec.VodkaApp.Core;
using Konefeld.Kopiec.VodkaApp.DaoMock1.BO;
using Konefeld.Kopiec.VodkaApp.Interfaces;

namespace Konefeld.Kopiec.VodkaApp.DaoMock1
{
    public class DaoMock1 : IDao
    {
        private readonly IList<IVodka> _vodkas;
        private readonly IList<IProducer> _producers;

        public DaoMock1()
        {
            _producers = new List<IProducer>
            {
                new Producer() { Id = 1, Name = "Polmos Białystok", Description = "We made vodkas before it was popular" },
                new Producer() { Id = 2, Name = "Bacardi", Description = "Silky smooth - all of them" }
            };

            _vodkas = new List<IVodka>
            {
                new Vodka() { Id = 1, Producer = _producers[0], Name = "Żubrówka", Type = VodkaType.Grain, AlcoholPercentage = 40.0},
                new Vodka() { Id = 2, Producer = _producers[1], Name = "Belveder", Type = VodkaType.Grain, AlcoholPercentage = 38.0},
                new Vodka() { Id = 3, Producer = _producers[1], Name = "Gray Goose", Type = VodkaType.Grain, AlcoholPercentage = 35.0}
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