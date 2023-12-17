namespace Konefeld.Kopiec.VodkaApp.Interfaces
{
    public interface IDao
    {
        IVodka CreateVodka();
        IEnumerable<IVodka> GetAllVodkas();
        IProducer CreateProducer();
        IEnumerable<IProducer> GetAllProducers();
    }
}