using System.Collections.ObjectModel;
using System.Windows.Data;

namespace Konefeld.Kopiec.VodkaApp.UI.ViewModels
{
    public class ProducerListViewModel : ViewModelBase
    {
        public ObservableCollection<ProducerViewModel> Producers { get; set; } = new ObservableCollection<ProducerViewModel>();
        private readonly ListCollectionView _view;

        private readonly RelayCommand _filterDataCommand;
        public RelayCommand FilterDataCommand => _filterDataCommand;
        public string FilterValue { get; set; }

        private readonly Blc.Blc _blc;
        public ProducerListViewModel()
        {
            _blc = Blc.Blc.Instance;
            //try
            //{
            //    dataAccess = Singleton.Instance;
            //}
            //catch (NullReferenceException)
            //{
            //    Console.WriteLine("Creating DAO Failed!");
            //}
            OnPropertyChanged("Producers");
            GetAllProducers();

            _view = (ListCollectionView)CollectionViewSource.GetDefaultView(Producers);
            _filterDataCommand = new RelayCommand(param => FilterData());
            _addProducerCommand = new RelayCommand(param => AddProducer(), param => CanAddProducer());
            _saveProducerCommand = new RelayCommand(param => SaveProducer(), param => CanSaveProducer());
            _deleteProducerCommand = new RelayCommand(param => DeleteProducer(), param => CanDeleteProducer());
            
            UpdatedProducer = null;
            SelectedProducer = UpdatedProducer;
        }
        private void GetAllProducers()
        {
            var producers = _blc.GetProducers().ToList();
            foreach (var producer in producers)
            {
                Producers.Add(new ProducerViewModel(producer));
            }
        }

        private void FilterData()
        {
            if (string.IsNullOrEmpty(FilterValue))
            {
                _view.Filter = null;
            }
            else
            {
                _view.Filter = (p) => ((ProducerViewModel)p).Name.Contains(FilterValue);
            }
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
            if (!Producers.Contains(UpdatedProducer))
            {
                Producers.Add(UpdatedProducer);
                //_blc.UpdateProducer(UpdatedProducer.Producer);
                UpdatedProducer = null;
            }

            UpdatedProducer = null;
        }

        private bool CanSaveProducer()
        {
            return UpdatedProducer is { HasErrors: false };
        }

        private readonly RelayCommand _addProducerCommand;
        public RelayCommand AddProducerCommand => _addProducerCommand;

        private void AddProducer()
        {
            //IProducer newProducer = dataAccess.AddProducer();
            //EditedProducer = new ProducerViewModel(newProducer);
            //EditedProducer.Validate();
        }

        private bool CanAddProducer()
        {
            return UpdatedProducer == null;
        }

        private readonly RelayCommand _deleteProducerCommand;
        public RelayCommand DeleteProducerCommand => _deleteProducerCommand;

        private void DeleteProducer()
        {
            if (Producers.Contains(SelectedProducer))
            {
                //_blc.DeleteProducer(SelectedProducer.Producer);
                Producers.Remove(SelectedProducer);
            }

            UpdatedProducer = null;
        }

        private bool CanDeleteProducer()
        {
            return UpdatedProducer is { HasErrors: false };
        }
    }
}
