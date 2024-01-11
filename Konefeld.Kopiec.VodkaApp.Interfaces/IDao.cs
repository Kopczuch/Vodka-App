using Konefeld.Kopiec.VodkaApp.Core;

namespace Konefeld.Kopiec.VodkaApp.Interfaces
{
    public interface IDao
    {
        // Create
        int CreateVodka(string name, int producerId, VodkaType vodkaType, double alcoholPercentage,
            double volumeInLiters, double price, string? flavourProfile);
        int CreateProducer(string name, string description, string address, string countryOfOrigin,
            int establishmentYear, ProducerExportStatus producerExportStatus);

        // Read
        IEnumerable<IVodka> GetAllVodkas();
        IEnumerable<IProducer> GetAllProducers();

        // Update
        bool UpdateVodka(int id, string vodkaSnapshot); // todo: do przemyślenia xd
        bool UpdateProducer(int id, string producerSnapshot);

        // Delete
        bool DeleteVodka(int id);
        bool DeleteProducer(int id);
    }
}