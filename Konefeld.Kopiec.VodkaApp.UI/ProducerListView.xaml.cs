using System.Windows.Controls;
using Konefeld.Kopiec.VodkaApp.Core;
using Konefeld.Kopiec.VodkaApp.Interfaces;
using Konefeld.Kopiec.VodkaApp.ViewModel;

namespace Konefeld.Kopiec.VodkaApp.UI
{
    /// <summary>
    /// Interaction logic for ProducerListView.xaml
    /// </summary>
    public partial class ProducerListView : UserControl
    {
        private readonly Blc.Blc _blc;
        public ProducerListView()
        {
            InitializeComponent();

            InitializeComponent();

            _blc = Blc.Blc.Instance;

            LvProducers.ItemsSource = MapToViewModel(_blc.GetProducers());
        }

        private IEnumerable<ProducerViewModel> MapToViewModel(IEnumerable<IProducer> producers)
        {
            return producers.Select(p => new ProducerViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Address = p.Address,
                CountryOfOrigin = p.CountryOfOrigin,
                EstablishmentYear = p.EstablishmentYear,
                ExportStatus = p.ExportStatus.ToPrettyString()
            });
        }

        public int CreateProducer(string name, string description, string address, string countryOfOrigin,
            int establishmentYear, ProducerExportStatus producerExportStatus)
        {
            return _blc.CreateProducer(name, description,
                address, countryOfOrigin, establishmentYear, producerExportStatus);
        }

        // producerSnapshot to na razie koncepcja tylko będzie zmieniane
        public bool UpdateProducer(int id, string producerSnapshot)
        {
            return _blc.UpdateProducer(id, producerSnapshot);
        }

        public bool DeleteProducer(int id)
        {
            return _blc.DeleteProducer(id);
        }
    }
}
