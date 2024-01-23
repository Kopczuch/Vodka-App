using Konefeld.Kopiec.VodkaApp.Core;
using Konefeld.Kopiec.VodkaApp.Interfaces;
using Konefeld.Kopiec.VodkaApp.UI.WEB.Models.Dto;
using Konefeld.Kopiec.VodkaApp.UI.WEB.Models.ViewModels;

namespace Konefeld.Kopiec.VodkaApp.UI.WEB.Services
{
    public interface IProducerService
    {
        int CreateProducer(IProducerDto newProducer);
        ProducerDto GetProducer(int id);
        IList<ProducerViewModel> GetProducers();
        IList<ProducerViewModel> GetFilteredProducers(IProducerFilter filter);
        bool UpdateProducer(int id, IProducerDto updatedProducer);
        bool DeleteProducer(int id);
        (bool IsSuccess, string Message) Validate(IProducerDto producer);
    }

    public class ProducerService : IProducerService
    {
        private readonly Blc.Blc _blc;

        public ProducerService()
        {
            _blc = Blc.Blc.Instance;
        }

        public int CreateProducer(IProducerDto newProducer)
        {
            return _blc.CreateProducer(newProducer);
        }

        public ProducerDto GetProducer(int id)
        {
            var producer = _blc.GetProducer(id);

            if (producer == null)
                return null;

            return new ProducerDto
            {
                Name = producer.Name,
                Description = producer.Description,
                CountryOfOrigin = producer.CountryOfOrigin,
                EstablishmentYear = producer.EstablishmentYear,
                ExportStatus = producer.ExportStatus
            };
        }

        public IList<ProducerViewModel> GetProducers()
        {
            var producers = _blc.GetProducers().ToList();

            return producers.Select(p => new ProducerViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CountryOfOrigin = p.CountryOfOrigin,
                EstablishmentYear = p.EstablishmentYear,
                ExportStatus = p.ExportStatus.ToPrettyString()
            }).ToList();
        }

        public IList<ProducerViewModel> GetFilteredProducers(IProducerFilter filter)
        {
            var producers = _blc.GetFilteredProducers(filter).ToList();

            return producers.Select(p => new ProducerViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CountryOfOrigin = p.CountryOfOrigin,
                EstablishmentYear = p.EstablishmentYear,
                ExportStatus = p.ExportStatus.ToPrettyString()
            }).ToList();
        }

        public bool UpdateProducer(int id, IProducerDto updatedProducer)
        {
            return _blc.UpdateProducer(id, updatedProducer);
        }

        public bool DeleteProducer(int id)
        {
            return _blc.DeleteProducer(id);
        }

        public (bool IsSuccess, string Message) Validate(IProducerDto producer)
        {
            if (string.IsNullOrWhiteSpace(producer.Name))
                return (false, "Producer's name is required.");
            if (string.IsNullOrWhiteSpace(producer.Description))
                return (false, "Producer's description is required.");
            if (producer.Description.Length > 500)
                return (false, "Description exceeds the maximum length of 500 characters.");
            if (string.IsNullOrEmpty(producer.CountryOfOrigin))
                return (false, "Producer's country of origin must be specified.");

            return (true, "Success");
        }
    }
}
