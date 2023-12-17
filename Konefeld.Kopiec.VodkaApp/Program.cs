using Microsoft.Extensions.Configuration;

namespace Konefeld.Kopiec.VodkaApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Console.WriteLine("Hello, World!");
            var libraryName = configuration["AppSettings:DaoLibraryName"];
            Console.WriteLine(libraryName);

            var blc = Blc.Blc.Instance;

            foreach (var producer in blc.GetProducers())
            {
                Console.WriteLine($"{producer.Id}: {producer.Name}");
            }

            foreach (var vodka in blc.GetVodkas())
            {
                Console.WriteLine($"{vodka.Id}: {vodka.Producer.Name} {vodka.Name} {vodka.AlcoholPercentage}%");
            }
        }
    }
}