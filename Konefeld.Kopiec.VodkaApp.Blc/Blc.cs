using System.Reflection;
using Konefeld.Kopiec.VodkaApp.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Konefeld.Kopiec.VodkaApp.Blc
{
    public class Blc
    {
        private static Blc instance;
        private static readonly object lockObject = new object();

        private IDao Dao;

        private Blc(string libraryName)
        {
            var assembly = Assembly.UnsafeLoadFrom(libraryName);
            var typeToCreate = assembly.GetTypes().FirstOrDefault(type => type.IsAssignableTo(typeof(IDao)));

            try
            {
                Dao = (IDao)Activator.CreateInstance(typeToCreate, null);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create instance of IDao: {typeToCreate}\n{ex.Message}");
            }
        }

        public static Blc Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            var configuration = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json")
                                .Build();
                            var libraryName = configuration["AppSettings:DaoLibraryName"];

                            instance = new Blc(libraryName);
                        }
                    }
                }

                return instance;
            }
        }


        // Create
        public int CreateVodka(IVodkaDto vodka) => Dao.CreateVodka(vodka);

        public int CreateProducer(IProducerDto producer) => Dao.CreateProducer(producer);
        
        // Read
        public IVodka GetVodka(int id) => Dao.GetVodka(id);
        public IProducer GetProducer(int id) => Dao.GetProducer(id);
        public IEnumerable<IVodka> GetVodkas() => Dao.GetAllVodkas();
        public IEnumerable<IVodka> GetFilteredVodkas(IVodkaFilter filter) => Dao.GetFilteredVodkas(filter);
        public IEnumerable<IProducer> GetProducers() => Dao.GetAllProducers();

        // Update
        public bool UpdateVodka(int id, IVodkaDto vodka) => Dao.UpdateVodka(id, vodka);
        public bool UpdateProducer(int id, IProducerDto producer) => Dao.UpdateProducer(id, producer);

        // Delete
        public bool DeleteVodka(int id) => Dao.DeleteVodka(id);
        public bool DeleteProducer(int id) => Dao.DeleteProducer(id);
    }
}
