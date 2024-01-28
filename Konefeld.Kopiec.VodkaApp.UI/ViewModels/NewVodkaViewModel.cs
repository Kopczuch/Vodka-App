using Konefeld.Kopiec.VodkaApp.Core;
using Konefeld.Kopiec.VodkaApp.Interfaces;
using System.ComponentModel.DataAnnotations;
using Konefeld.Kopiec.VodkaApp.UI.Dto;
using System.Collections.ObjectModel;

namespace Konefeld.Kopiec.VodkaApp.UI.ViewModels
{
    public class NewVodkaViewModel : ViewModelBase
    {
        public ObservableCollection<ProducerViewModel> Producers { get; set; } = new ObservableCollection<ProducerViewModel>();
        private readonly Blc.Blc _blc;

        private IVodkaDto _vodka;

        public IVodkaDto Vodka
        {
            get => _vodka;
            set
            {
                _vodka = value;
                OnPropertyChanged(nameof(Vodka));
            }
        }

        public NewVodkaViewModel()
        {
            _vodka = new VodkaDto();
            _blc = Blc.Blc.Instance;
            GetAllProducers();

            _addVodkaCommand = new RelayCommand(param => AddVodka(), param => CanAddVodka());
        }

        private void GetAllProducers()
        {
            Producers.Clear();
            var producers = _blc.GetProducers().ToList();

            foreach (var producer in producers)
            {
                Producers.Add(new ProducerViewModel(producer));
            }
        }

        private readonly RelayCommand _addVodkaCommand;
        public RelayCommand AddVodkaCommand => _addVodkaCommand;

        private void AddVodka()
        {
            var validationResults = Validate();
            
            if (validationResults.Any())
                return;

            _blc.CreateVodka(Vodka);
            ClearForm();
        }

        private void ClearForm()
        {
            Vodka = new VodkaDto();
            Name = string.Empty;
            VolumeInLiters = 0;
            AlcoholPercentage = 0;
            FlavourProfile = null;
            NewVodkaProducer = null;
            Type = default;
        }

        private bool CanAddVodka()
        {
            return !Vodka.Equals(null);
        }

        [Required(ErrorMessage = "Name is required")]
        public string Name
        {
            get => _vodka.Name;
            set
            {
                _vodka.Name = value;
                OnPropertyChanged("Name");
            }
        }

        private IProducer _newVodkaProducer;

        [Required(ErrorMessage = "Producer is required")]
        public IProducer NewVodkaProducer
        {
            get => _newVodkaProducer;
            set
            {
                _newVodkaProducer = value;
                if (value != null)
                {
                    _vodka.ProducerId = value.Id;
                }
                OnPropertyChanged("NewVodkaProducer");
            }
        }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public double Price
        {
            get => _vodka.Price;
            set
            {
                _vodka.Price = value;
                OnPropertyChanged("Price");
            }
        }

        [Required(ErrorMessage = "Alcohol percentage is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public double AlcoholPercentage
        {
            get => _vodka.AlcoholPercentage;
            set
            {
                _vodka.AlcoholPercentage = value;
                OnPropertyChanged("AlcoholPercentage");
            }
        }

        [Required(ErrorMessage = "Volume is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Volume must be greater than 0")]
        public double VolumeInLiters
        {
            get => _vodka.VolumeInLiters;
            set
            {
                _vodka.VolumeInLiters = value;
                OnPropertyChanged("VolumeInLiters");
            }
        }

        public string? FlavourProfile
        {
            get => _vodka.FlavourProfile;
            set
            {
                _vodka.FlavourProfile = value;
                OnPropertyChanged("FlavourProfile");
            }
        }

        public VodkaType Type
        {
            get => _vodka.Type;
            set
            {
                _vodka.Type = value;
                OnPropertyChanged("Type");
            }
        }

        public IList<ValidationResult> Validate()
        {
            var valContext = new ValidationContext(this, null, null);
            var valResults = new List<ValidationResult>();

            Validator.TryValidateObject(this, valContext, valResults, true);

            foreach (var x in Errors.ToList().Where(x => valResults.All(r => r.MemberNames.All(m => m != x.Key))))
            {
                Errors.Remove(x.Key);
                OnErrorChanged(x.Key);
            }

            var query = from f1 in valResults
                        from f2 in f1.MemberNames
                        group f1 by f2 into g
                        select g;
            foreach (var x in query)
            {
                var messages = x.Select(r => r.ErrorMessage).ToList();
                if (Errors.ContainsKey(x.Key))
                {
                    Errors.Remove(x.Key);
                }

                Errors.Add(x.Key, messages);
                OnErrorChanged(x.Key);
            }

            return valResults;
        }
    }
}
