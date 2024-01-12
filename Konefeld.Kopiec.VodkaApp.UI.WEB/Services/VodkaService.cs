using Konefeld.Kopiec.VodkaApp.Core;
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

        public int CreateVodka(string name, int producerId, VodkaType vodkaType, double alcoholPercentage,
            double volumeInLiters, double price, string? flavourProfile)
        {
            var newVodka = new VodkaDto(name, producerId, vodkaType, alcoholPercentage, volumeInLiters, price,
                flavourProfile);

            return _blc.CreateVodka(newVodka);
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

        public bool UpdateVodka(int id, string name, int producerId, VodkaType vodkaType, double alcoholPercentage,
            double volumeInLiters, double price, string? flavourProfile)
        {
            var updatedVodka = new VodkaDto(name, producerId, vodkaType, alcoholPercentage, volumeInLiters, price,
                flavourProfile);

            return _blc.UpdateVodka(id, updatedVodka);
        }

        public bool DeleteVodka(int id)
        {
            return _blc.DeleteVodka(id);
        }

    }
}
