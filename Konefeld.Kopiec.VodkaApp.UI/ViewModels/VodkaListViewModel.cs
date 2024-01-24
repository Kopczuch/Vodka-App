using Konefeld.Kopiec.VodkaApp.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Konefeld.Kopiec.VodkaApp.UI.Dto;
using Konefeld.Kopiec.VodkaApp.UI.Models;
using Konefeld.Kopiec.VodkaApp.UI.FilterObjects;

namespace Konefeld.Kopiec.VodkaApp.UI.ViewModels
{
    public class VodkaListViewModel : INotifyPropertyChanged
    {
        private readonly Blc.Blc _blc;

        public VodkaListViewModel()
        {
            _blc = Blc.Blc.Instance;

            Vodkas = new ObservableCollection<VodkaModel>();
            NewVodka = new VodkaDto();
            Filter = new VodkaFilter();

            LoadVodkas();
            LoadProducersData();
        }
        public ObservableCollection<VodkaModel> Vodkas { get; set; }

        public IList<ProducerData> Producers { get; set; }
        public IList<ProducerData> FilterProducers { get; set; }

        public IVodkaDto NewVodka { get; set; }

        public IVodkaFilter Filter { get; set; }

        public void ApplyFilter()
        {
            Vodkas.Clear();

            var vodkas = MapToViewModel(_blc.GetFilteredVodkas(Filter));

            foreach (var vodka in vodkas)
            {
                Vodkas.Add(vodka);
            }
        }

        public void ClearFilters()
        {
            LoadVodkas();
            Filter = new VodkaFilter();
            OnPropertyChanged(nameof(Filter));
        }

        private void LoadVodkas()
        {
            Vodkas.Clear();
            var vodkas = MapToViewModel(_blc.GetVodkas());
            foreach (var vodka in vodkas)
            {
                Vodkas.Add(vodka);
            }
        }

        private void LoadProducersData()
        {
            var producers = _blc.GetProducers().Select(p => new ProducerData
            {
                Id = p.Id,
                Name = p.Name
            }).OrderBy(p => p.Name).ToList();
            Producers = producers;
            FilterProducers = new List<ProducerData>(producers);

            var emptyProducer = new ProducerData { Id = 0, Name = ""};
            FilterProducers.Insert(0, emptyProducer);
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
            }).OrderByDescending(v => v.Id);
        }

        public void CreateVodka()
        {
            var id = _blc.CreateVodka(NewVodka);
            LoadVodkas();
        }

        public void DeleteVodka(int id)
        {
            var result = _blc.DeleteVodka(id);
            LoadVodkas();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
