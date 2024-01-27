using System.Collections.ObjectModel;
using System.Windows.Data;
using Konefeld.Kopiec.VodkaApp.Core;
using Konefeld.Kopiec.VodkaApp.Interfaces;
using Konefeld.Kopiec.VodkaApp.UI.Dto;

namespace Konefeld.Kopiec.VodkaApp.UI.ViewModels
{
    public class VodkaListViewModel : ViewModelBase
    {
        public ObservableCollection<VodkaViewModel> Vodkas { get; set; } = new ObservableCollection<VodkaViewModel>();
        public ObservableCollection<ProducerViewModel> Producers { get; set; } = new ObservableCollection<ProducerViewModel>();

        private readonly ListCollectionView _view;
        private readonly RelayCommand _filterDataCommand;
        private readonly RelayCommand _clearFiltersCommand;
        private IProducer _filterProducer;
        private VodkaType _filterType;

        public RelayCommand FilterDataCommand => _filterDataCommand;
        public RelayCommand ClearFiltersCommand => _clearFiltersCommand;

        public IVodkaFilter FilterValue { get; set; }
        public IProducer FilterProducer
        {
            get => _filterProducer;
            set
            {
                if (_filterProducer != value)
                {
                    _filterProducer = value;

                    if (FilterValue != null)
                    {
                        FilterValue.ProducerId = _filterProducer?.Id ?? 0;
                    }

                    OnPropertyChanged("FilterProducer");
                }
            }
        }

        public VodkaType FilterType
        {
            get => _filterType;
            set
            {
                if (_filterType != value)
                {
                    _filterType = value;

                    if (FilterValue != null)
                    {
                        FilterValue.Type = _filterType.ToString();
                    }

                    OnPropertyChanged("FilterProducer");
                }
            }
        }

        private readonly Blc.Blc _blc;
        public VodkaListViewModel()
        {
            _blc = Blc.Blc.Instance;
            FilterValue = new VodkaFilter();
            GetAllVodkas();
            GetAllProducers();
            OnPropertyChanged("Vodkas");

            _view = (ListCollectionView)CollectionViewSource.GetDefaultView(Vodkas);

            _filterDataCommand = new RelayCommand(param => FilterData());
            _clearFiltersCommand = new RelayCommand(param => ClearFilters());
            _addVodkaCommand = new RelayCommand(param => AddVodka(), param => CanAddVodka());
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

            foreach (var producer in producers)
            {
                Producers.Add(new ProducerViewModel(producer));
            }
        }
        
        private void FilterData()
        {
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

            UpdatedVodka = null;
        }

        private bool CanSaveVodka()
        {
            return UpdatedVodka is { HasErrors: false };
        }

        private readonly RelayCommand _addVodkaCommand;
        public RelayCommand AddVodkaCommand => _addVodkaCommand;

        private void AddVodka()
        {
            //if (Vodkas.Contains(UpdatedVodka))
            //Vodkas.Add(UpdatedVodka);

            //IGame newGame = dataAccess.AddGame();
            UpdatedVodka.Validate();
            
            var newVodka = new VodkaDto
            {
                Name = UpdatedVodka.Vodka.Name,
                VolumeInLiters = UpdatedVodka.Vodka.VolumeInLiters,
                AlcoholPercentage = UpdatedVodka.Vodka.AlcoholPercentage,
                Price = UpdatedVodka.Vodka.Price,
                ProducerId = UpdatedVodka.Vodka.Producer.Id,
                Type = UpdatedVodka.Vodka.Type,
                FlavourProfile = UpdatedVodka.Vodka.FlavourProfile
            };

            var id = _blc.CreateVodka(newVodka);
            UpdatedVodka.Vodka.Id = id;

            if (!Vodkas.Contains(UpdatedVodka))
                Vodkas.Add(UpdatedVodka);
        }

        private bool CanAddVodka()
        {
            return UpdatedVodka == null;
        }

        private readonly RelayCommand _deleteVodkaCommand;
        public RelayCommand DeleteVodkaCommand => _deleteVodkaCommand;

        private void DeleteVodka()
        {

            //if (Games.Contains(SelectedGame))
            //{
            //    dataAccess.DeleteGame(SelectedGame.Game);
            //    Games.Remove(SelectedGame);
            //    SelectedGame = null;
            //    EditedGame = null;
            //}
            //SelectedGame = null;
            //EditedGame = null;
        }

        private bool CanDeleteVodka()
        {
            return UpdatedVodka is { HasErrors: false };
        }
    }

}
