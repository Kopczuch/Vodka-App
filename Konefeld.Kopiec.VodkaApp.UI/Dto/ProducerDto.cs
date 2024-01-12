using Konefeld.Kopiec.VodkaApp.Core;
using Konefeld.Kopiec.VodkaApp.Interfaces;

namespace Konefeld.Kopiec.VodkaApp.UI.Dto
{
    public class ProducerDto : IProducerDto
    {
        public ProducerDto(string name, string description, string address, string countryOfOrigin,
            int establishmentYear, ProducerExportStatus producerExportStatus)
        {
            Name = name;
            Description = description;
            Address = address;
            CountryOfOrigin = countryOfOrigin;
            EstablishmentYear = establishmentYear;
            ExportStatus = producerExportStatus;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string CountryOfOrigin { get; set; }
        public int EstablishmentYear { get; set; }
        public ProducerExportStatus ExportStatus { get; set; }
    }
}
