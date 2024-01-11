using System.Reflection;
using Konefeld.Kopiec.VodkaApp.Core;
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
        public int CreateVodka(string name, int producerId, VodkaType vodkaType, double alcoholPercentage,
            double volumeInLiters, double price, string? flavourProfile) => Dao.CreateVodka(name, producerId,
            vodkaType, alcoholPercentage, volumeInLiters, price,
            flavourProfile);

        public int CreateProducer(string name, string description, string address, string countryOfOrigin,
            int establishmentYear, ProducerExportStatus producerExportStatus) => Dao.CreateProducer(name, description,
            address, countryOfOrigin, establishmentYear, producerExportStatus);
        
        // Read
        public IEnumerable<IVodka> GetVodkas() => Dao.GetAllVodkas();
        public IEnumerable<IProducer> GetProducers() => Dao.GetAllProducers();

        // Update
        public bool UpdateVodka(int id, string vodkaSnapshot) => Dao.UpdateVodka(id, vodkaSnapshot); // todo: do przemyślenia xd
        public bool UpdateProducer(int id, string producerSnapshot) => Dao.UpdateProducer(id, producerSnapshot);

        // Delete
        public bool DeleteVodka(int id) => Dao.DeleteVodka(id);
        public bool DeleteProducer(int id) => Dao.DeleteProducer(id);
    }
}
