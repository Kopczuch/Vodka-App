using Konefeld.Kopiec.VodkaApp.Core;
using Konefeld.Kopiec.VodkaApp.Interfaces;

namespace Konefeld.Kopiec.VodkaApp.UI.WEB.Models.Dto
{
    public class ProducerDto : IProducerDto
    {
        public ProducerDto()
        {
            Name = string.Empty;
            Description = string.Empty;
            CountryOfOrigin = string.Empty;
        }

        public ProducerDto(string name, string description, string countryOfOrigin,
            int establishmentYear, ProducerExportStatus producerExportStatus)
        {
            Name = name;
            Description = description;
            CountryOfOrigin = countryOfOrigin;
            EstablishmentYear = establishmentYear;
            ExportStatus = producerExportStatus;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string CountryOfOrigin { get; set; }
        public int EstablishmentYear { get; set; }
        public ProducerExportStatus ExportStatus { get; set; }
    }
}
