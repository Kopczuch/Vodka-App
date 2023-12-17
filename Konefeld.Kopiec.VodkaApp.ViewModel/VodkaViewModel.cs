using Konefeld.Kopiec.VodkaApp.Interfaces;

namespace Konefeld.Kopiec.VodkaApp.ViewModel
{
    public class VodkaViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProducerName { get; set; }
        public string Type { get; set; }
        public double AlcoholPercentage { get; set; }
    }
}
