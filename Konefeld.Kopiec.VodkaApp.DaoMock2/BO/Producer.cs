using Konefeld.Kopiec.VodkaApp.Core;
using Konefeld.Kopiec.VodkaApp.Interfaces;

namespace Konefeld.Kopiec.VodkaApp.DaoMock2.BO
{
    public class Producer : IProducer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string CountryOfOrigin { get; set; }
        public int EstablishmentYear { get; set; }
        public ProducerExportStatus ExportStatus { get; set; }
    }
}
