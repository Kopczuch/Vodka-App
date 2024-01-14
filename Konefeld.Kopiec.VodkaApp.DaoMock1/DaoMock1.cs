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
        public int CreateVodka(IVodkaDto vodka)
        {
            var newVodka = MapVodkaDto(vodka);
            _vodkas.Add(newVodka);

            return newVodka.Id;
        }

        public int CreateProducer(IProducerDto producer)
        {
            var newProducer = MapProducerDto(producer);
            _producers.Add(newProducer);

            return newProducer.Id;
        }

        // Read
        public IVodka GetVodka(int id)
        {
            var vodka = _vodkas.FirstOrDefault(v => v.Id == id);

            return vodka ?? throw new NullReferenceException("Vodka with given ID does not exist");
        }

        public IProducer GetProducer(int id)
        {
            var producer = _producers.FirstOrDefault(p => p.Id == id);

            return producer ?? throw new NullReferenceException("Producer with given ID does not exist");
        }


        public IEnumerable<IVodka> GetAllVodkas()
        {
            return _vodkas;
        }

        public IEnumerable<IVodka> GetFilteredVodkas(IVodkaFilter filter)
        {
            var filteredVodkas = _vodkas.Where(vodka =>
                (string.IsNullOrWhiteSpace(filter.SearchTerm) ||
                 vodka.Name.Contains(filter.SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                 (vodka.FlavourProfile != null && vodka.FlavourProfile.Contains(filter.SearchTerm, StringComparison.OrdinalIgnoreCase))) &&
                (filter.Volume == 0 || vodka.VolumeInLiters == filter.Volume) &&
                (filter.Alcohol == 0 || vodka.AlcoholPercentage == filter.Alcohol) &&
                (filter.PriceLowerBound == 0 || (filter.PriceLowerBound <= vodka.Price && vodka.Price <= filter.PriceUpperBound)) &&
                (string.IsNullOrWhiteSpace(filter.Type) || vodka.Type.ToString().Equals(filter.Type, StringComparison.OrdinalIgnoreCase)) &&
                 (filter.ProducerId == 0 || vodka.Producer.Id == filter.ProducerId)
            ).ToList();

            return filteredVodkas;
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return _producers;
        }

        // Update
        public bool UpdateVodka(int id, IVodkaDto vodka)
        {
            var updatedVodka = _vodkas.FirstOrDefault(v => v.Id == id);

            if (updatedVodka == null)
                return false;

            UpdateVodka(updatedVodka, vodka);

            return true;
        }
        public bool UpdateProducer(int id, IProducerDto producer)
        {
            var updatedProducer = _producers.FirstOrDefault(p => p.Id == id);

            if (updatedProducer == null)
                return false;

            UpdateProducer(updatedProducer, producer);

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

        private Vodka MapVodkaDto(IVodkaDto vodka)
        {
            return new Vodka
            {
                Id = GetLatestVodkaId(),
                Name = vodka.Name,
                Producer = GetProducer(vodka.ProducerId),
                Type = vodka.Type,
                AlcoholPercentage = vodka.AlcoholPercentage,
                VolumeInLiters = vodka.VolumeInLiters,
                Price = vodka.Price,
                FlavourProfile = vodka.FlavourProfile
            };
        }

        private int GetLatestVodkaId()
        {
            if (_vodkas.Count == 0)
                return 1;

            var latestVodka = _vodkas.MaxBy(v => v.Id);
            return latestVodka?.Id + 1 ?? 1;
        }

        private Producer MapProducerDto(IProducerDto producer)
        {
            return new Producer
            {
                Id = GetLatestProducerId(),
                Name = producer.Name,
                Description = producer.Description,
                CountryOfOrigin = producer.CountryOfOrigin,
                EstablishmentYear = producer.EstablishmentYear,
                ExportStatus = producer.ExportStatus
            };
        }

        private int GetLatestProducerId()
        {
            if (_producers.Count == 0)
                return 1;

            var latestProducer = _producers.MaxBy(v => v.Id);
            return latestProducer?.Id + 1 ?? 1;
        }

        private void UpdateVodka(IVodka vodka, IVodkaDto vodkaDto)
        {
            vodka.Name = vodkaDto.Name;
            vodka.Producer = GetProducer(vodkaDto.ProducerId);
            vodka.Type = vodkaDto.Type;
            vodka.AlcoholPercentage = vodkaDto.AlcoholPercentage;
            vodka.VolumeInLiters = vodkaDto.VolumeInLiters;
            vodka.Price = vodkaDto.Price;
            vodka.FlavourProfile = vodkaDto.FlavourProfile;
        }

        private void UpdateProducer(IProducer producer, IProducerDto producerDto)
        {
            producer.Name = producerDto.Name;
            producer.Description = producerDto.Description;
            producer.CountryOfOrigin = producerDto.CountryOfOrigin;
            producer.EstablishmentYear = producerDto.EstablishmentYear;
            producer.ExportStatus = producerDto.ExportStatus;
        }
    }
}