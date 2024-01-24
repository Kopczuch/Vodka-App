using Konefeld.Kopiec.VodkaApp.Interfaces;
using System.Windows.Controls;
using Konefeld.Kopiec.VodkaApp.UI.ViewModels;

namespace Konefeld.Kopiec.VodkaApp.UI
{
    /// <summary>
    /// Interaction logic for VodkaListView.xaml
    /// </summary>
    public partial class VodkaListView : UserControl
    {
        public IVodkaFilter Filter;
        public IVodkaDto NewVodka;
        public VodkaListViewModel ViewModel { get; set; }

        public VodkaListView()
        {
            InitializeComponent();
            ViewModel = new VodkaListViewModel();
            DataContext = ViewModel;
            LvVodkas.ItemsSource = ViewModel.Vodkas;
        }
    }
}
