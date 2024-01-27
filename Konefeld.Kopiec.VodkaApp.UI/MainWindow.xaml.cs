using Konefeld.Kopiec.VodkaApp.Interfaces;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Konefeld.Kopiec.VodkaApp.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<IProducer> Help = new ObservableCollection<IProducer>();
        public MainWindow()
        {
            InitializeComponent();
            foreach (var x in VodkaListViewModel.Producers)
            {
                Help.Add(x.Producer);
            }

            ProducerComboBox.ItemsSource = Help;
            FilterProducerComboBox.ItemsSource = Help;
            Tc.SelectionChanged += Tabcontrol_SelectionChanged;
        }

        private void Tabcontrol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                VodkaListViewModel.GetAllVodkas();
                Help.Clear();
                foreach (var x in VodkaListViewModel.Producers)
                {
                    Help.Add(x.Producer);
                }
            }
        }
    }
}