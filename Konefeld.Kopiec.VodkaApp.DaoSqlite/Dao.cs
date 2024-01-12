using Konefeld.Kopiec.VodkaApp.DaoSqlite.BO;
using Konefeld.Kopiec.VodkaApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Konefeld.Kopiec.VodkaApp.DaoSqlite
{
    public class Dao : IDao
    {
        private readonly VodkaAppDbContext _context;

        public Dao()
        {
            _context = new VodkaAppDbContext();
        }

        // Create
        public int CreateVodka(IVodkaDto vodka)
        {
            var newVodka = MapVodkaDto(vodka);

            _context.Vodkas.Add(newVodka);
            _context.SaveChanges();

            return newVodka.Id;
        }

        public int CreateProducer(IProducerDto producer)
        {
            var newProducer = MapProducerDto(producer);

            _context.Producers.Add(newProducer);
            _context.SaveChanges();

            return newProducer.Id;
        }

        // Read
        public IEnumerable<IVodka> GetAllVodkas()
        {
            return _context.Vodkas
                .Include(v => v.ProducerImpl)
                .Select(v => new Vodka
                {
                    Id = v.Id,
                    Name = v.Name,
                    Producer = v.ProducerImpl,
                    Type = v.Type,
                    AlcoholPercentage = v.AlcoholPercentage,
                    VolumeInLiters = v.VolumeInLiters,
                    Price = v.Price,
                    FlavourProfile = v.FlavourProfile
                }).ToList();
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return _context.Producers.ToList();
        }

        // Update
        public bool UpdateVodka(int id, IVodkaDto vodka)
        {
            var updatedVodka = _context.Vodkas.FirstOrDefault(v => v.Id == id);

            if (updatedVodka == null)
                return false;

            UpdateVodka(updatedVodka, vodka);

            _context.SaveChanges();
            return true;
        }

        public bool UpdateProducer(int id, IProducerDto producer)
        {
            var updatedProducer = _context.Producers.FirstOrDefault(p => p.Id == id);

            if (updatedProducer == null)
                return false;

            UpdateProducer(updatedProducer, producer);

            _context.SaveChanges();
            return true;
        }

        // Delete
        public bool DeleteVodka(int id)
        {
            var vodkaToDelete = _context.Vodkas.FirstOrDefault(v => v.Id == id);
            
            if (vodkaToDelete == null)
                return false;

            _context.Vodkas.Remove(vodkaToDelete);
            _context.SaveChanges();

            return true;
        }

        public bool DeleteProducer(int id)
        {
            var producerToDelete = _context.Producers.FirstOrDefault(p => p.Id == id);

            if (producerToDelete == null)
                return false;

            _context.Producers.Remove(producerToDelete);
            _context.SaveChanges();

            return true;
        }

        // Mappings
        private Vodka MapVodkaDto(IVodkaDto vodka)
        {
            return new Vodka
            {
                Name = vodka.Name,
                Producer = GetProducer(vodka.ProducerId),
                Type = vodka.Type,
                AlcoholPercentage = vodka.AlcoholPercentage,
                VolumeInLiters = vodka.VolumeInLiters,
                Price = vodka.Price,
                FlavourProfile = vodka.FlavourProfile
            };
        }

        private IProducer GetProducer(int id)
        {
            var producer = _context.Producers.FirstOrDefault(p => p.Id == id);

            return producer ?? throw new NullReferenceException("Producer with given ID does not exist.");
        }

        private Producer MapProducerDto(IProducerDto producer)
        {
            return new Producer
            {
                Name = producer.Name,
                Description = producer.Description,
                Address = producer.Address,
                CountryOfOrigin = producer.CountryOfOrigin,
                EstablishmentYear = producer.EstablishmentYear,
                ExportStatus = producer.ExportStatus
            };
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
            producer.Address = producerDto.Address;
            producer.CountryOfOrigin = producerDto.CountryOfOrigin;
            producer.EstablishmentYear = producerDto.EstablishmentYear;
            producer.ExportStatus = producerDto.ExportStatus;
        }
    }
}
