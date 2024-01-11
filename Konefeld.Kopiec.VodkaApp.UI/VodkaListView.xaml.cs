using Konefeld.Kopiec.VodkaApp.Interfaces;
using Konefeld.Kopiec.VodkaApp.ViewModel;
using System.Windows.Controls;
using Konefeld.Kopiec.VodkaApp.Core;

namespace Konefeld.Kopiec.VodkaApp.UI
{
    /// <summary>
    /// Interaction logic for VodkaListView.xaml
    /// </summary>
    public partial class VodkaListView : UserControl
    {
        private readonly Blc.Blc _blc;
        public VodkaListView()
        {
            InitializeComponent();

            _blc = Blc.Blc.Instance;

            LvVodkas.ItemsSource = MapToViewModel(_blc.GetVodkas());
        }

        private IEnumerable<VodkaViewModel> MapToViewModel(IEnumerable<IVodka> vodkas)
        {
            return vodkas.Select(v => new VodkaViewModel
            {
                Id = v.Id,
                Name = v.Name,
                ProducerName = v.Producer.Name,
                Type = v.Type.ToString(),
                AlcoholPercentage = v.AlcoholPercentage,
                VolumeInLiters = v.VolumeInLiters,
                Price = v.Price,
                FlavourProfile = v.FlavourProfile
            });
        }

        public int CreateVodka(string name, int producerId, VodkaType vodkaType, double alcoholPercentage,
            double volumeInLiters, double price, string? flavourProfile)
        {
            return _blc.CreateVodka(name, producerId, vodkaType, alcoholPercentage, volumeInLiters, price, flavourProfile);
        }

        // vodkaSnapshot to na razie koncepcja tylko będzie zmieniane
        public bool UpdateVodka(int id, string vodkaSnapshot)
        {
            return _blc.UpdateVodka(id, vodkaSnapshot);
        }

        public bool DeleteVodka(int id)
        {
            return _blc.DeleteVodka(id);
        }
    }
}
