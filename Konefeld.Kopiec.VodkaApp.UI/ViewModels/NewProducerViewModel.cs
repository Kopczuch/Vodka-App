using Konefeld.Kopiec.VodkaApp.Core;
using Konefeld.Kopiec.VodkaApp.Interfaces;
using System.ComponentModel.DataAnnotations;
using Konefeld.Kopiec.VodkaApp.UI.Dto;

namespace Konefeld.Kopiec.VodkaApp.UI.ViewModels
{
    public class NewProducerViewModel : ViewModelBase
    {
        private readonly Blc.Blc _blc;

        private IProducerDto _producer;

        public IProducerDto Producer
        {
            get => _producer;
            set
            {
                _producer = value;
                OnPropertyChanged(nameof(Producer));
            }
        }

        public NewProducerViewModel()
        {
            _producer = new ProducerDto();
            _blc = Blc.Blc.Instance;

            _addProducerCommand = new RelayCommand(param => AddProducer(), param => CanAddProducer());
        }

        private readonly RelayCommand _addProducerCommand;
        public RelayCommand AddProducerCommand => _addProducerCommand;

        private void AddProducer()
        {
            var validationResults = Validate();

            if (validationResults.Any())
                return;

            _blc.CreateProducer(Producer);
            ClearForm();
        }

        private void ClearForm()
        {
            Producer = new ProducerDto();
            Name = string.Empty;
            Description = string.Empty;
            CountryOfOrigin = string.Empty;
            EstablishmentYear = 0;
            ExportStatus = default;
        }

        private bool CanAddProducer()
        {
            return !Producer.Equals(null);
        }

        [Required(ErrorMessage = "Name is required")]
        public string Name
        {
            get => _producer.Name;
            set
            {
                _producer.Name = value;
                OnPropertyChanged("Name");
            }
        }

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(300, ErrorMessage = "Description cannot exceed 300 characters")]
        public string Description
        {
            get => _producer.Description;
            set
            {
                _producer.Description = value;
                OnPropertyChanged("Description");
            }
        }

        [Required(ErrorMessage = "Country of origin is required")]
        public string CountryOfOrigin
        {
            get => _producer.CountryOfOrigin;
            set
            {
                _producer.CountryOfOrigin = value;
                OnPropertyChanged("CountryOfOrigin");
            }
        }


        [Required(ErrorMessage = "Year of establishment is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Year must be greater than 0")]
        public int EstablishmentYear
        {
            get => _producer.EstablishmentYear;
            set
            {
                _producer.EstablishmentYear = value;
                OnPropertyChanged("EstablishmentYear");
            }
        }

        public ProducerExportStatus ExportStatus
        {
            get => _producer.ExportStatus;
            set
            {
                _producer.ExportStatus= value;
                OnPropertyChanged("ExportStatus");
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
