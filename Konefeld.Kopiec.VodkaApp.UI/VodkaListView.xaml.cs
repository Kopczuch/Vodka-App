using System.Windows.Controls;
using Konefeld.Kopiec.VodkaApp.UI.ViewModels;
using System.Text.RegularExpressions;
using System.Windows;

namespace Konefeld.Kopiec.VodkaApp.UI
{
    /// <summary>
    /// Interaction logic for VodkaListView.xaml
    /// </summary>
    public partial class VodkaListView
    {
        public VodkaListViewModel ViewModel { get; set; }

        public VodkaListView()
        {
            InitializeComponent();
            ViewModel = new VodkaListViewModel();
            DataContext = ViewModel;
        }

        private void NumericInput(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            var text = textBox.Text;
            e.Handled = !IsTextAllowed(text);
        }

        private static bool IsTextAllowed(string text)
        {
            var regex = new Regex(@"^[0-9]*(\.[0-9]{0,2})?$");
            return regex.IsMatch(text);
        }

        private void AddVodka(object sender, RoutedEventArgs e)
        {
            ViewModel.CreateVodka();
        }

        private void DeleteVodka(object sender, RoutedEventArgs e)
        {
            if (sender is Button { CommandParameter: int vodkaId })
                ViewModel.DeleteVodka(vodkaId);
        }

        private void FilterVodkas(object sender, RoutedEventArgs e)
        {
            ViewModel.ApplyFilter();
        }

        private void ClearFilters(object sender, RoutedEventArgs e)
        {
            ViewModel.ClearFilters();
        }

    }
}
