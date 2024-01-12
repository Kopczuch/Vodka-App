using Konefeld.Kopiec.VodkaApp.Core;

namespace Konefeld.Kopiec.VodkaApp.Interfaces
{
    public interface IProducerDto
    {
        string Name { get; set; }
        string Description { get; set; }
        string Address { get; set; }
        string CountryOfOrigin { get; set; }
        int EstablishmentYear { get; set; }
        ProducerExportStatus ExportStatus { get; set; }
    }
}
