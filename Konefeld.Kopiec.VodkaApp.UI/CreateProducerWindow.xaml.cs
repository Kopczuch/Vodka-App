using System.Windows;

namespace Konefeld.Kopiec.VodkaApp.UI
{
    /// <summary>
    /// Interaction logic for CreateProducerWindow.xaml
    /// </summary>
    public partial class CreateProducerWindow : Window
    {
        public CreateProducerWindow()
        {
            InitializeComponent();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
