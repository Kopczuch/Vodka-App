using Konefeld.Kopiec.VodkaApp.Core;
using Konefeld.Kopiec.VodkaApp.Interfaces;
using Konefeld.Kopiec.VodkaApp.UI.WEB.Models;
using Konefeld.Kopiec.VodkaApp.UI.WEB.Models.Dto;
using Konefeld.Kopiec.VodkaApp.UI.WEB.Models.ViewModels;

namespace Konefeld.Kopiec.VodkaApp.UI.WEB.Services
{
    public class VodkaService
    {
        private readonly Blc.Blc _blc;

        public VodkaService()
        {
            _blc = Blc.Blc.Instance;
        }

        public int CreateVodka(IVodkaDto newVodka)
        {
            return _blc.CreateVodka(newVodka);
        }

        public IVodkaDto GetVodka(int id)
        {
            var vodka = _blc.GetVodka(id);

            return new VodkaDto
            {
                Name = vodka.Name,
                ProducerId = vodka.Producer.Id,
                Type = vodka.Type,
                AlcoholPercentage = vodka.AlcoholPercentage,
                VolumeInLiters = vodka.VolumeInLiters,
                Price = vodka.Price,
                FlavourProfile = vodka.FlavourProfile
            };
        }

        public IList<VodkaViewModel> GetVodkas()
        {
            var vodkas = _blc.GetVodkas().ToList();

            return vodkas.Select(v => new VodkaViewModel
            {
                Id = v.Id,
                Name = v.Name,
                ProducerName = v.Producer.Name,
                Type = v.Type.ToString(),
                AlcoholPercentage = v.AlcoholPercentage,
                VolumeInLiters = v.VolumeInLiters,
                Price = v.Price,
                FlavourProfile = v.FlavourProfile
            }).ToList();
        }

        public IList<VodkaViewModel> GetFilteredVodkas(IVodkaFilter filter)
        {
            var vodkas = _blc.GetFilteredVodkas(filter);

            return vodkas.Select(v => new VodkaViewModel
            {
                Id = v.Id,
                Name = v.Name,
                ProducerName = v.Producer.Name,
                Type = v.Type.ToString(),
                AlcoholPercentage = v.AlcoholPercentage,
                VolumeInLiters = v.VolumeInLiters,
                Price = v.Price,
                FlavourProfile = v.FlavourProfile
            }).ToList();
        }

        public bool UpdateVodka(int id, IVodkaDto updatedVodka)
        {
            return _blc.UpdateVodka(id, updatedVodka);
        }

        public bool DeleteVodka(int id)
        {
            return _blc.DeleteVodka(id);
        }

        public IList<ProducerData> GetProducersData()
        {
            var producers = _blc.GetProducers().ToList();
            return producers.Select(p => new ProducerData
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();
        }
    }
}
