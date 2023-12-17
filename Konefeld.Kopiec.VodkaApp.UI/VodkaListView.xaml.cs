using Konefeld.Kopiec.VodkaApp.Interfaces;
using Konefeld.Kopiec.VodkaApp.ViewModel;
using System.Windows.Controls;

namespace Konefeld.Kopiec.VodkaApp.UI
{
    /// <summary>
    /// Interaction logic for VodkaListView.xaml
    /// </summary>
    public partial class VodkaListView : UserControl
    {
        public VodkaListView()
        {
            InitializeComponent();

            var blc = Blc.Blc.Instance;

            LvVodkas.ItemsSource = MapToViewModel(blc.GetVodkas());
        }

        private IEnumerable<VodkaViewModel> MapToViewModel(IEnumerable<IVodka> vodkas)
        {
            return vodkas.Select(v => new VodkaViewModel
            {
                Id = v.Id,
                Name = v.Name,
                ProducerName = v.Producer.Name,
                Type = v.Type.ToString(),
                AlcoholPercentage = v.AlcoholPercentage
            });
        }
    }
}
