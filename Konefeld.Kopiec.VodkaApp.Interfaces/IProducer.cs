using Konefeld.Kopiec.VodkaApp.Core;

namespace Konefeld.Kopiec.VodkaApp.Interfaces
{
    public interface IProducer
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string Address { get; set; }
        string CountryOfOrigin { get; set; }
        int EstablishmentYear { get; set; }
        ProducerExportStatus ExportStatus { get; set; }
    }
}
