﻿using Konefeld.Kopiec.VodkaApp.DaoSqlite.BO;
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
        public IVodka GetVodka(int id)
        {
            var vodka = _context.Vodkas.FirstOrDefault(v => v.Id == id);

            return vodka;
        }

        public IProducer GetProducer(int id)
        {
            var producer = _context.Producers.FirstOrDefault(p => p.Id == id);

            return producer;
        }

        public IEnumerable<IVodka> GetAllVodkas()
        {
            var vodkas = new List<IVodka>();

            vodkas.AddRange(_context.Vodkas
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
                }).ToList());

            return vodkas;
        }

        public IEnumerable<IVodka> GetFilteredVodkas(IVodkaFilter filter)
        {
            var vodkas = GetAllVodkas().ToList();

            var filteredVodkas = vodkas.Where(vodka =>
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
            var producers = new List<IProducer>();
            producers.AddRange(_context.Producers.ToList());
            return producers;
        }

        public IEnumerable<IProducer> GetFilteredProducers(IProducerFilter filter)
        {
            var producers = GetAllProducers().ToList();

            var filteredProducers = producers.Where(producer =>
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

            var vodkasFromProducer = _context.Vodkas
                .Include(v => v.ProducerImpl)
                .Where(v => v.ProducerImpl.Id == producerToDelete.Id).ToList();

            if (vodkasFromProducer.Any())
                _context.Vodkas.RemoveRange(vodkasFromProducer);

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

        private Producer MapProducerDto(IProducerDto producer)
        {
            return new Producer
            {
                Name = producer.Name,
                Description = producer.Description,
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
            producer.CountryOfOrigin = producerDto.CountryOfOrigin;
            producer.EstablishmentYear = producerDto.EstablishmentYear;
            producer.ExportStatus = producerDto.ExportStatus;
        }
    }
}
