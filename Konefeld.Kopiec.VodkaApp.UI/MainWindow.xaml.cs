﻿using Konefeld.Kopiec.VodkaApp.Interfaces;
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
        private readonly ObservableCollection<IProducer> _producersData = new();
        public MainWindow()
        {
            InitializeComponent();
            foreach (var x in VodkaListViewModel.Producers)
            {
                _producersData.Add(x.Producer);
            }

            ProducerComboBox.ItemsSource = _producersData;
            Tc.SelectionChanged += TabControl_SelectionChanged;
        }

        private void CreateVodka(object sender, RoutedEventArgs e)
        {
            var createVodkaWindow = new CreateVodkaWindow
            {
                Owner = this
            };

            createVodkaWindow.Closed += (s, args) => VodkaListViewModel.GetAllVodkas();
            createVodkaWindow.ShowDialog();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is not TabControl)
                return;
            
            VodkaListViewModel.GetAllVodkas();
            _producersData.Clear();
            foreach (var x in VodkaListViewModel.Producers)
            {
                _producersData.Add(x.Producer);
            }
        }
    }
}