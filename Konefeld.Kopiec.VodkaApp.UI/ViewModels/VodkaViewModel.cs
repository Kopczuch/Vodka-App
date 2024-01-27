using Konefeld.Kopiec.VodkaApp.Interfaces;
using System.ComponentModel.DataAnnotations;
using Konefeld.Kopiec.VodkaApp.Core;

namespace Konefeld.Kopiec.VodkaApp.UI.ViewModels
{
    public class VodkaViewModel : ViewModelBase
    {
        private readonly IVodka _vodka;
        public IVodka Vodka => _vodka;

        public VodkaViewModel(IVodka vodka)
        {
            _vodka = vodka;
        }

        [Required(ErrorMessage = "Name is required")]
        public string Name
        {
            get => _vodka.Name;
            set
            {
                _vodka.Name = value;
                Validate();
                OnPropertyChanged("Name");
            }
        }
        
        [Required(ErrorMessage = "Producer is required")]
        public IProducer Producer
        {
            get => _vodka.Producer;
            set
            {
                _vodka.Producer = value;
                Validate();
                OnPropertyChanged("Producer");
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
                Validate();
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
                Validate();
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
                Validate();
                OnPropertyChanged("VolumeInLiters");
            }
        }

        public string? FlavourProfile
        {
            get => _vodka.FlavourProfile ?? "Not specified";
            set
            {
                _vodka.FlavourProfile = value;
                Validate();
                OnPropertyChanged("FlavourProfile");
            }
        }

        public VodkaType Type
        {
            get => _vodka.Type;
            set
            {
                _vodka.Type = value;
                Validate();
                OnPropertyChanged("Type");
            }
        }

        public void Validate()
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
        }
    }
}
