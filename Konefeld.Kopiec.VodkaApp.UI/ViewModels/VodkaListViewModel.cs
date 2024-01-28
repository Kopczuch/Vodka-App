using System.Collections.ObjectModel;
using Konefeld.Kopiec.VodkaApp.Core;
using Konefeld.Kopiec.VodkaApp.Interfaces;
using Konefeld.Kopiec.VodkaApp.UI.Dto;

namespace Konefeld.Kopiec.VodkaApp.UI.ViewModels
{
    public class VodkaListViewModel : ViewModelBase
    {
        public ObservableCollection<VodkaViewModel> Vodkas { get; set; } = new ObservableCollection<VodkaViewModel>();
        public ObservableCollection<ProducerViewModel> Producers { get; set; } = new ObservableCollection<ProducerViewModel>();

        private readonly RelayCommand _filterDataCommand;
        private readonly RelayCommand _clearFiltersCommand;

        public RelayCommand FilterDataCommand => _filterDataCommand;
        public RelayCommand ClearFiltersCommand => _clearFiltersCommand;

        public IVodkaFilter FilterValue { get; set; }
        public IList<ProducerData> FilterProducers { get; set; }

        private readonly Blc.Blc _blc;
        public VodkaListViewModel()
        {
            _blc = Blc.Blc.Instance;
            FilterValue = new VodkaFilter();
            GetAllVodkas();
            GetAllProducers();
            OnPropertyChanged("Vodkas");

            _filterDataCommand = new RelayCommand(param => FilterData());
            _clearFiltersCommand = new RelayCommand(param => ClearFilters());
            
            _saveVodkaCommand = new RelayCommand(param => SaveVodka(), param => CanSaveVodka());
            _deleteVodkaCommand = new RelayCommand(param => DeleteVodka(), param => CanDeleteVodka());
            
        }
        public void GetAllVodkas()
        {
            Vodkas.Clear();
            var vodkas = _blc.GetVodkas().ToList();

            foreach (var vodka in vodkas)
            {
                Vodkas.Add(new VodkaViewModel(vodka));
            }
        }

        public void GetAllProducers()
        {
            Producers.Clear();
            var producers = _blc.GetProducers().ToList();

            var producersData = producers.Select(p => new ProducerData
            {
                Id = p.Id,
                Name = p.Name
            });

            var emptyProducer = new ProducerData { Id = 0, Name = "All producers" };

            FilterProducers = new List<ProducerData>(producersData);
            FilterProducers.Insert(0, emptyProducer);

            foreach (var producer in producers)
            {
                Producers.Add(new ProducerViewModel(producer));
            }
        }
        
        private void FilterData()
        {
            // note: hack
            FilterValue.Type = FilterValue.Type.Equals("System.Windows.Controls.ComboBoxItem: All types") ? string.Empty : FilterValue.Type;

            Vodkas.Clear();
            var filteredVodkas = _blc.GetFilteredVodkas(FilterValue).ToList();

            foreach (var vodka in filteredVodkas)
            {
                Vodkas.Add(new VodkaViewModel(vodka));
            }
        }

        private void ClearFilters()
        {
            GetAllVodkas();
            FilterValue = new VodkaFilter();
            OnPropertyChanged(nameof(FilterValue));
        }

        private VodkaViewModel _updatedVodka;

        public VodkaViewModel UpdatedVodka
        {
            get => _updatedVodka;
            set
            {
                _updatedVodka = value;
                OnPropertyChanged(nameof(UpdatedVodka));
            }
        }

        private VodkaViewModel _selectedVodka;

        public VodkaViewModel SelectedVodka
        {
            get => _selectedVodka;
            set
            {
                _selectedVodka = value;
                UpdatedVodka = value;
                OnPropertyChanged(nameof(UpdatedVodka));
            }
        }

        private RelayCommand _saveVodkaCommand;
        public RelayCommand SaveVodkaCommand => _saveVodkaCommand;

        private void SaveVodka()
        {
            var id = UpdatedVodka.Vodka.Id;
            var updatedVodka = new VodkaDto(
                UpdatedVodka.Vodka.Name,
                UpdatedVodka.Vodka.Producer.Id,
                UpdatedVodka.Vodka.Type,
                UpdatedVodka.Vodka.AlcoholPercentage,
                UpdatedVodka.Vodka.VolumeInLiters,
                UpdatedVodka.Vodka.Price,
                UpdatedVodka.Vodka.FlavourProfile
            );

            _blc.UpdateVodka(id, updatedVodka);
        }

        private bool CanSaveVodka()
        {
            return UpdatedVodka is { HasErrors: false };
        }

        private readonly RelayCommand _deleteVodkaCommand;
        public RelayCommand DeleteVodkaCommand => _deleteVodkaCommand;

        private void DeleteVodka()
        {
            if (!Vodkas.Contains(SelectedVodka))
                return;

            _blc.DeleteVodka(SelectedVodka.Vodka.Id);
            Vodkas.Remove(SelectedVodka);
            SelectedVodka = null;
            UpdatedVodka = null;
        }

        private bool CanDeleteVodka()
        {
            return UpdatedVodka is { HasErrors: false };
        }
    }

}
