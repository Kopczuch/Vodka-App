using Konefeld.Kopiec.VodkaApp.Interfaces;
using System.Collections.ObjectModel;
using System.Windows;

namespace Konefeld.Kopiec.VodkaApp.UI
{
    /// <summary>
    /// Interaction logic for CreateVodkaWindow.xaml
    /// </summary>
    public partial class CreateVodkaWindow : Window
    {
        private readonly ObservableCollection<IProducer> _producersData = new ObservableCollection<IProducer>();
        public CreateVodkaWindow()
        {
            InitializeComponent();

            foreach (var x in NewVodkaViewModel.Producers)
            {
                _producersData.Add(x.Producer);
            }

            ProducerComboBox.ItemsSource = _producersData;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
