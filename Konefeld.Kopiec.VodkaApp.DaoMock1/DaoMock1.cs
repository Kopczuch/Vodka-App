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
                new Producer() { Id = 1, Name = "Polmos Białystok", Description = "We made vodkas before it was popular", CountryOfOrigin = "Poland", EstablishmentYear = 1928, ExportStatus = ProducerExportStatus.DomesticOnly },
                new Producer() { Id = 2, Name = "Bacardi", Description = "Silky smooth - all of them", CountryOfOrigin = "Cuba", EstablishmentYear = 1862, ExportStatus = ProducerExportStatus.GlobalExport },
                new Producer() { Id = 3, Name = "Absolut Vodka", Description = "Swedish vodka known for its purity", CountryOfOrigin = "Sweden", EstablishmentYear = 1879, ExportStatus = ProducerExportStatus.RegionalExport },
                new Producer() { Id = 4, Name = "Grey Goose", Description = "Crafted in France, made from French ingredients", CountryOfOrigin = "France", EstablishmentYear = 1997, ExportStatus = ProducerExportStatus.GlobalExport },
                new Producer() { Id = 5, Name = "Stolichnaya", Description = "Russian vodka with a rich history", CountryOfOrigin = "Russia", EstablishmentYear = 1938, ExportStatus = ProducerExportStatus.RegionalExport },
                new Producer() { Id = 6, Name = "Jack Daniel's", Description = "Famous for Tennessee whiskey, but also produces vodka", CountryOfOrigin = "United States", EstablishmentYear = 1866, ExportStatus = ProducerExportStatus.GlobalExport },
                new Producer() { Id = 7, Name = "Belvedere", Description = "Polish luxury vodka brand", CountryOfOrigin = "Poland", EstablishmentYear = 1996, ExportStatus = ProducerExportStatus.GlobalExport },
                new Producer() { Id = 8, Name = "Smirnoff", Description = "One of the world's best-selling vodka brands", CountryOfOrigin = "United Kingdom", EstablishmentYear = 1864, ExportStatus = ProducerExportStatus.GlobalExport },
            };

            _vodkas = new List<IVodka>
            {
                new Vodka
                {
                    Id = 1,
                    Name = "Żubrówka",
                    Producer = _producers[0],
                    Type = VodkaType.Potato,
                    AlcoholPercentage = 40.0,
                    VolumeInLiters = 0.75,
                    Price = 19.99,
                    FlavourProfile = "Smooth and crisp"
                },
                new Vodka
                {
                    Id = 2,
                    Name = "Belveder",
                    Producer = _producers[1],
                    Type = VodkaType.Grain,
                    AlcoholPercentage = 45.0,
                    VolumeInLiters = 1.0,
                    Price = 29.99,
                    FlavourProfile = "Bold and aromatic"
                },
                new Vodka
                {
                    Id = 3,
                    Name = "Premium Classic",
                    Producer = _producers[2],
                    Type = VodkaType.Grain,
                    AlcoholPercentage = 42.0,
                    VolumeInLiters = 0.7,
                    Price = 49.99,
                    FlavourProfile = "Rich and velvety"
                },
                new Vodka
                {
                    Id = 4,
                    Name = "Mountain Mist",
                    Producer = _producers[3],
                    Type = VodkaType.Flavoured,
                    AlcoholPercentage = 38.0,
                    VolumeInLiters = 0.75,
                    Price = 34.99,
                    FlavourProfile = "Crisp with a hint of citrus"
                },
                new Vodka
                {
                    Id = 5,
                    Name = "Golden Grain",
                    Producer = _producers[4],
                    Type = VodkaType.Potato,
                    AlcoholPercentage = 40.0,
                    VolumeInLiters = 1.0,
                    Price = 24.99,
                    FlavourProfile = "Smooth and mellow"
                },
                new Vodka
                {
                    Id = 6,
                    Name = "Arctic Chill",
                    Producer = _producers[5],
                    Type = VodkaType.Fruit,
                    AlcoholPercentage = 37.5,
                    VolumeInLiters = 0.5,
                    Price = 39.99,
                    FlavourProfile = "Refreshing mint sensation"
                }
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

            return vodka;
        }

        public IProducer GetProducer(int id)
        {
            var producer = _producers.FirstOrDefault(p => p.Id == id);

            return producer;
        }


        public IEnumerable<IVodka> GetAllVodkas()
        {
            return _vodkas;
        }

        public IEnumerable<IVodka> GetFilteredVodkas(IVodkaFilter filter)
        {
            var filteredVodkas = _vodkas.Where(vodka =>
                (string.IsNullOrWhiteSpace(filter.SearchTerm) ||
                 vodka.Name.Contains(filter.SearchTerm, StringComparison.InvariantCultureIgnoreCase) ||
                 (!string.IsNullOrWhiteSpace(vodka.FlavourProfile) && vodka.FlavourProfile.Contains(filter.SearchTerm, StringComparison.InvariantCultureIgnoreCase))) &&
                (filter.Volume == 0 || vodka.VolumeInLiters == filter.Volume) &&
                (filter.Alcohol == 0 || vodka.AlcoholPercentage == filter.Alcohol) &&
                (filter is { PriceLowerBound: 0, PriceUpperBound: 0 } || IsInRange(filter.PriceLowerBound, filter.PriceUpperBound, vodka.Price)) &&
                (string.IsNullOrWhiteSpace(filter.Type) || vodka.Type.ToString().Equals(filter.Type, StringComparison.InvariantCultureIgnoreCase)) &&
                 (filter.ProducerId == 0 || vodka.Producer.Id == filter.ProducerId)
            ).ToList();

            return filteredVodkas;
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return _producers;
        }

        public IEnumerable<IProducer> GetFilteredProducers(IProducerFilter filter)
        {
            var filteredProducers = _producers.Where(producer =>
                (string.IsNullOrWhiteSpace(filter.SearchTerm) ||
                 producer.Name.Contains(filter.SearchTerm, StringComparison.InvariantCultureIgnoreCase)) &&
                (string.IsNullOrWhiteSpace(filter.Country) ||
                 producer.CountryOfOrigin.Contains(filter.Country, StringComparison.InvariantCultureIgnoreCase)) &&
                (filter is { MinYear: 0, MaxYear: 0 } || IsInRange(filter.MinYear, filter.MaxYear, producer.EstablishmentYear)) &&
                (string.IsNullOrWhiteSpace(filter.ExportStatus) || producer.ExportStatus.ToString()
                    .Equals(filter.ExportStatus, StringComparison.InvariantCultureIgnoreCase))).ToList();

            return filteredProducers;
        }

        private bool IsInRange(double min, double max, double value)
        {
            return min switch
            {
                0 when max == 0 => true,
                0 when max > 0 => value <= max,
                > 0 when max == 0 => value >= min,
                > 0 when max > 0 => min <= value && value <= max,
                _ => false
            };
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