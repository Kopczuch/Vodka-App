using Konefeld.Kopiec.VodkaApp.Core;
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
        public int CreateVodka(string name, int producerId, VodkaType vodkaType, double alcoholPercentage, double volumeInLiters,
            double price, string? flavourProfile)
        {
            var newVodka = new Vodka
            {
                Name = name,
                Producer = GetProducer(producerId),
                Type = vodkaType,
                AlcoholPercentage = alcoholPercentage,
                VolumeInLiters = volumeInLiters,
                Price = price,
                FlavourProfile = flavourProfile
            };

            _context.Vodkas.Add(newVodka);
            _context.SaveChanges();

            return newVodka.Id;
        }

        public int CreateProducer(string name, string description, string address, string countryOfOrigin, int establishmentYear,
            ProducerExportStatus producerExportStatus)
        {
            var newProducer = new Producer
            {
                Name = name,
                Description = description,
                Address = address,
                CountryOfOrigin = countryOfOrigin,
                EstablishmentYear = establishmentYear,
                ExportStatus = producerExportStatus
            };

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
        public bool UpdateVodka(int id, string vodkaSnapshot)
        {
            var updatedVodka = _context.Vodkas.FirstOrDefault(v => v.Id == id);

            if (updatedVodka == null)
                return false;

            updatedVodka.Name += "New"; // todo: do zmiany
            _context.SaveChanges();
            return true;
        }

        public bool UpdateProducer(int id, string producerSnapshot)
        {
            var updatedProducer = _context.Producers.FirstOrDefault(p => p.Id == id);

            if (updatedProducer == null)
                return false;

            updatedProducer.Name += "New"; // todo: do zmiany
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


        private IProducer GetProducer(int id)
        {
            var producer = _context.Producers.FirstOrDefault(p => p.Id == id);

            return producer ?? throw new NullReferenceException("Producer with given ID does not exist.");
        }
    }
}
