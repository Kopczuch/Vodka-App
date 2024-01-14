namespace Konefeld.Kopiec.VodkaApp.Interfaces
{
    public interface IProducerFilter
    {
        string SearchTerm { get; set; }
        string Country { get; set; }
        int MinYear { get; set; }
        int MaxYear { get; set; }
        public string ExportStatus { get; set; }
    }
}
