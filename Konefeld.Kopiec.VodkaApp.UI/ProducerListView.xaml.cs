using System.Windows.Controls;
using Konefeld.Kopiec.VodkaApp.Interfaces;
using Konefeld.Kopiec.VodkaApp.ViewModel;

namespace Konefeld.Kopiec.VodkaApp.UI
{
    /// <summary>
    /// Interaction logic for ProducerListView.xaml
    /// </summary>
    public partial class ProducerListView : UserControl
    {
        public ProducerListView()
        {
            InitializeComponent();

            InitializeComponent();

            var blc = Blc.Blc.Instance;

            LvProducers.ItemsSource = MapToViewModel(blc.GetProducers());
        }

        private IEnumerable<ProducerViewModel> MapToViewModel(IEnumerable<IProducer> producers)
        {
            return producers.Select(p => new ProducerViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description
            });
        }
    }
}
