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

        // Create
        public int CreateVodka(string name, int producerId, VodkaType vodkaType, double alcoholPercentage, double volumeInLiters,
            double price, string? flavourProfile)
        {
            var newVodka = new Vodka
            {
                Id = GetLatestVodkaId(),
                Name = name,
                Producer = GetProducer(producerId),
                Type = vodkaType,
                AlcoholPercentage = alcoholPercentage,
                VolumeInLiters = volumeInLiters,
                Price = price,
                FlavourProfile = flavourProfile
            };

            _vodkas.Add(newVodka);
            return newVodka.Id;
        }

        public int CreateProducer(string name, string description, string address, string countryOfOrigin, int establishmentYear,
            ProducerExportStatus producerExportStatus)
        {
            var newProducer = new Producer
            {
                Id = GetLatestProducerId(),
                Name = name,
                Description = description,
                Address = address,
                CountryOfOrigin = countryOfOrigin,
                EstablishmentYear = establishmentYear,
                ExportStatus = producerExportStatus
            };

            _producers.Add(newProducer);
            return newProducer.Id;
        }

        // Read
        public IEnumerable<IVodka> GetAllVodkas()
        {
            return _vodkas;
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return _producers;
        }

        // Update
        public bool UpdateVodka(int id, string vodkaSnapshot)
        {
            var updatedVodka = _vodkas.FirstOrDefault(v => v.Id == id);

            if (updatedVodka == null)
                return false;

            updatedVodka.Name += "New"; // todo: do zmiany
            return true;
        }
        public bool UpdateProducer(int id, string producerSnapshot)
        {
            var updatedProducer = _producers.FirstOrDefault(p => p.Id == id);

            if (updatedProducer == null)
                return false;

            updatedProducer.Name += "New"; // todo: do zmiany
            return true;
        }

        // Delete
        public bool DeleteVodka(int id)
        {
            var vodkaToDelete = _vodkas.FirstOrDefault(v => v.Id == id);

            if (vodkaToDelete == null)
                return false;

            _vodkas.Remove(vodkaToDelete);
            return true;
        }

        public bool DeleteProducer(int id)
        {
            var producersToDelete = _producers.FirstOrDefault(p =>p.Id == id);

            if (producersToDelete == null)
                return false;

            _producers.Remove(producersToDelete);
            return true;
        }

        private IProducer GetProducer(int id)
        {
            var producer = _producers.FirstOrDefault(p => p.Id == id);

            return producer ?? throw new NullReferenceException("Producer with given ID does not exist.");
        }

        private int GetLatestVodkaId()
        {
            if (_vodkas.Count == 0)
                return 1;

            var latestVodka = _vodkas.MaxBy(v => v.Id);
            return latestVodka?.Id + 1 ?? 1;
        }

        private int GetLatestProducerId()
        {
            if (_producers.Count == 0)
                return 1;

            var latestProducer = _producers.MaxBy(v => v.Id);
            return latestProducer?.Id + 1 ?? 1;
        }
    }
}