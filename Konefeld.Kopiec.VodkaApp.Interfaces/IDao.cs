using Konefeld.Kopiec.VodkaApp.Core;

namespace Konefeld.Kopiec.VodkaApp.Interfaces
{
    public interface IDao
    {
        // Create
        int CreateVodka(IVodkaDto vodka);
        int CreateProducer(IProducerDto producer);

        // Read
        IVodka GetVodka(int id);
        IProducer GetProducer(int id);
        IEnumerable<IVodka> GetAllVodkas();
        IEnumerable<IVodka> GetFilteredVodkas(IVodkaFilter filter);
        IEnumerable<IProducer> GetAllProducers();

        // Update
        bool UpdateVodka(int id, IVodkaDto vodka);
        bool UpdateProducer(int id, IProducerDto producer);

        // Delete
        bool DeleteVodka(int id);
        bool DeleteProducer(int id);
    }
}