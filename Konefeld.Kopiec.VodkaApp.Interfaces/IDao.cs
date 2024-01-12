using Konefeld.Kopiec.VodkaApp.Core;

namespace Konefeld.Kopiec.VodkaApp.Interfaces
{
    public interface IDao
    {
        // Create
        int CreateVodka(IVodkaDto vodka);
        int CreateProducer(IProducerDto producer);

        // Read
        IEnumerable<IVodka> GetAllVodkas();
        IEnumerable<IProducer> GetAllProducers();

        // Update
        bool UpdateVodka(int id, IVodkaDto vodka);
        bool UpdateProducer(int id, IProducerDto producer);

        // Delete
        bool DeleteVodka(int id);
        bool DeleteProducer(int id);
    }
}