using Konefeld.Kopiec.VodkaApp.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Konefeld.Kopiec.VodkaApp.UI.Dto;
using Konefeld.Kopiec.VodkaApp.UI.Models;

namespace Konefeld.Kopiec.VodkaApp.UI.ViewModels
{
    public class VodkaListViewModel : INotifyPropertyChanged
    {
        private readonly Blc.Blc _blc;

        public VodkaListViewModel()
        {
            _blc = Blc.Blc.Instance;
            LoadVodkas();
            LoadProducersData();
        }

        private ObservableCollection<VodkaModel> _vodkas;
        public ObservableCollection<VodkaModel> Vodkas
        {
            get => _vodkas;
            set
            {
                if (_vodkas == value) return;
                _vodkas = value;
                OnPropertyChanged(nameof(Vodkas));
            }
        }

        public IList<ProducerData> Producers { get; set; }

        private IVodkaDto _newVodka;
        public IVodkaDto NewVodka
        {
            get => _newVodka;
            set
            {
                if (_newVodka == value) return;
                _newVodka = value;
                OnPropertyChanged(nameof(NewVodka));
            }
        }

        private void LoadVodkas()
        {
            Vodkas = new ObservableCollection<VodkaModel>(MapToViewModel(_blc.GetVodkas()));
        }

        private void LoadProducersData()
        {
            Producers = _blc.GetProducers().Select(p => new ProducerData
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();
        }

        private IEnumerable<VodkaModel> MapToViewModel(IEnumerable<IVodka> vodkas)
        {
            return vodkas.Select(v => new VodkaModel
            {
                Id = v.Id,
                Name = v.Name,
                ProducerName = v.Producer.Name,
                Type = v.Type.ToString(),
                AlcoholPercentage = v.AlcoholPercentage,
                VolumeInLiters = v.VolumeInLiters,
                Price = v.Price,
                FlavourProfile = v.FlavourProfile
            });
        }

        private void CreateVodka()
        {
            var id = _blc.CreateVodka(NewVodka);
            LoadVodkas();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
