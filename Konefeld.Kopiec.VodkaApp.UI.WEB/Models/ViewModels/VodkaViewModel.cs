namespace Konefeld.Kopiec.VodkaApp.UI.WEB.Models.ViewModels
{
    public class VodkaViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProducerName { get; set; }
        public string Type { get; set; }
        public double AlcoholPercentage { get; set; }
        public double VolumeInLiters { get; set; }
        public double Price { get; set; }
        public string? FlavourProfile { get; set; }
    }
}
