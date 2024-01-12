using Konefeld.Kopiec.VodkaApp.Core;
using Konefeld.Kopiec.VodkaApp.UI.WEB.Models;
using Konefeld.Kopiec.VodkaApp.UI.WEB.Models.Dto;

namespace Konefeld.Kopiec.VodkaApp.UI.WEB.Services
{
    public class ProducerService
    {
        private readonly Blc.Blc _blc;

        public ProducerService()
        {
            _blc = Blc.Blc.Instance;
        }

        public int CreateProducer(string name, string description, string countryOfOrigin,
            int establishmentYear, ProducerExportStatus producerExportStatus)
        {
            var newProducer = new ProducerDto(name, description, countryOfOrigin, establishmentYear, producerExportStatus);

            return _blc.CreateProducer(newProducer);
        }

        public IList<ProducerViewModel> GetProducers()
        {
            var producers = _blc.GetProducers().ToList();

            return producers.Select(p => new ProducerViewModel
            {
                Name = p.Name,
                Description = p.Description,
                CountryOfOrigin = p.CountryOfOrigin,
                EstablishmentYear = p.EstablishmentYear,
                ExportStatus = p.ExportStatus.ToPrettyString()
            }).ToList();
        }

        public bool UpdateProducer(int id, string name, string description, string countryOfOrigin,
            int establishmentYear, ProducerExportStatus producerExportStatus)
        {
            var updatedProducer = new ProducerDto(name, description,
                countryOfOrigin, establishmentYear, producerExportStatus);

            return _blc.UpdateProducer(id, updatedProducer);
        }

        public bool DeleteProducer(int id)
        {
            return _blc.DeleteProducer(id);
        }
    }
}
