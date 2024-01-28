using System.Collections.ObjectModel;
using System.Windows.Data;
using Konefeld.Kopiec.VodkaApp.Core;
using Konefeld.Kopiec.VodkaApp.Interfaces;
using Konefeld.Kopiec.VodkaApp.UI.Dto;

namespace Konefeld.Kopiec.VodkaApp.UI.ViewModels
{
    public class ProducerListViewModel : ViewModelBase
    {
        public ObservableCollection<ProducerViewModel> Producers { get; set; } = new ObservableCollection<ProducerViewModel>();

        private readonly RelayCommand _filterDataCommand;
        private readonly RelayCommand _clearFiltersCommand;

        public RelayCommand FilterDataCommand => _filterDataCommand;
        public RelayCommand ClearFiltersCommand => _clearFiltersCommand;

        public IProducerFilter FilterValue { get; set; }
        public IList<string> FilterExportStatuses { get; set; }


        private readonly Blc.Blc _blc;
        public ProducerListViewModel()
        {
            _blc = Blc.Blc.Instance;
            FilterValue = new ProducerFilter();

            OnPropertyChanged(nameof(Producers));
            GetAllProducers();

            _filterDataCommand = new RelayCommand(param => FilterData());
            _clearFiltersCommand = new RelayCommand(param => ClearFilters());
            _saveProducerCommand = new RelayCommand(param => SaveProducer(), param => CanSaveProducer());
            _deleteProducerCommand = new RelayCommand(param => DeleteProducer(), param => CanDeleteProducer());
        }

        public void GetAllProducers()
        {
            Producers.Clear();

            var producers = _blc.GetProducers().ToList();
            foreach (var producer in producers)
            {
                Producers.Add(new ProducerViewModel(producer));
            }

            var enumValues = Enum.GetValues(typeof(ProducerExportStatus)).Cast<ProducerExportStatus>();
            var prettyStrings = enumValues.Select(value => value.ToPrettyString());
            FilterExportStatuses = new List<string>(prettyStrings);
            FilterExportStatuses.Insert(0, "All");
        }

        private void FilterData()
        {
            FilterValue.ExportStatus = string.Equals(FilterValue.ExportStatus, "All") ? string.Empty : FilterValue.ExportStatus.Replace(" ", "");

            Producers.Clear();
            var filteredProducers = _blc.GetFilteredProducers(FilterValue).ToList();

            foreach (var producer in filteredProducers)
            {
                Producers.Add(new ProducerViewModel(producer));
            }
        }

        private void ClearFilters()
        {
            GetAllProducers();
            FilterValue = new ProducerFilter();
            OnPropertyChanged(nameof(FilterValue));
        }

        private ProducerViewModel _updatedProducer;
        public ProducerViewModel UpdatedProducer
        {
            get => _updatedProducer;
            set
            {
                _updatedProducer = value;
                OnPropertyChanged(nameof(UpdatedProducer));
            }
        }

        private ProducerViewModel _selectedProducer;
        public ProducerViewModel SelectedProducer
        {
            get => _selectedProducer;
            set
            {
                _selectedProducer = value;
                UpdatedProducer = value;
                OnPropertyChanged(nameof(SelectedProducer));
            }
        }

        private RelayCommand _saveProducerCommand;
        public RelayCommand SaveProducerCommand => _saveProducerCommand;

        private void SaveProducer()
        {
            var id = UpdatedProducer.Producer.Id;
            var updatedProducer = new ProducerDto
            (
                UpdatedProducer.Producer.Name,
                UpdatedProducer.Producer.Description,
                UpdatedProducer.Producer.CountryOfOrigin,
                UpdatedProducer.Producer.EstablishmentYear,
                UpdatedProducer.Producer.ExportStatus
            );

            _blc.UpdateProducer(id, updatedProducer);
        }

        private bool CanSaveProducer()
        {
            return UpdatedProducer is { HasErrors: false };
        }


        private readonly RelayCommand _deleteProducerCommand;
        public RelayCommand DeleteProducerCommand => _deleteProducerCommand;

        private void DeleteProducer()
        {
            if (!Producers.Contains(SelectedProducer))
                return;

            _blc.DeleteProducer(SelectedProducer.Producer.Id);
            Producers.Remove(SelectedProducer);
            SelectedProducer = null;
            UpdatedProducer = null;
        }

        private bool CanDeleteProducer()
        {
            return UpdatedProducer is { HasErrors: false };
        }
    }
}
