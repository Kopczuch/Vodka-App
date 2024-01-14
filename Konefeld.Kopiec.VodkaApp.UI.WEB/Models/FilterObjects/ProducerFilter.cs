using Konefeld.Kopiec.VodkaApp.Interfaces;

namespace Konefeld.Kopiec.VodkaApp.UI.WEB.Models.FilterObjects
{
    public class ProducerFilter : IProducerFilter
    {
        public string SearchTerm { get; set; }
        public string Country { get; set; }
        public int MinYear { get; set; }
        public int MaxYear { get; set; }
        public string ExportStatus { get; set; }
    }
}
