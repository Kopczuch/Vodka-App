using System.Collections.ObjectModel;
using Konefeld.Kopiec.VodkaApp.Interfaces;
using Konefeld.Kopiec.VodkaApp.UI.Dto;
using Konefeld.Kopiec.VodkaApp.UI.Models;

namespace Konefeld.Kopiec.VodkaApp.UI.Services
{
    public interface IVodkaService
    {
        int CreateVodka(IVodkaDto newVodka);
        IVodkaDto GetVodka(int id);
        ObservableCollection<VodkaModel> GetVodkas();
        IEnumerable<VodkaModel> GetFilteredVodkas(IVodkaFilter filter);
        bool UpdateVodka(int id, IVodkaDto updatedVodka);
        bool DeleteVodka(int id);
        IEnumerable<ProducerData> GetProducersData();
        (bool IsSuccess, string Message) Validate(IVodkaDto vodka);
    }
    public class VodkaService : IVodkaService
    {
        private readonly Blc.Blc _blc;

        public VodkaService()
        {
            _blc = Blc.Blc.Instance;
        }

        public int CreateVodka(IVodkaDto newVodka)
        {
            throw new NotImplementedException();
        }

        public IVodkaDto GetVodka(int id)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<VodkaModel> GetVodkas()
        {
            var vodkas = _blc.GetVodkas();

            return new ObservableCollection<VodkaModel>(vodkas.Select(v => new VodkaModel
            {
                Id = v.Id,
                Name = v.Name,
                ProducerName = v.Producer.Name,
                Type = v.Type.ToString(),
                AlcoholPercentage = v.AlcoholPercentage,
                VolumeInLiters = v.VolumeInLiters,
                Price = v.Price,
                FlavourProfile = v.FlavourProfile
            }));
        }

        public IEnumerable<VodkaModel> GetFilteredVodkas(IVodkaFilter filter)
        {
            throw new NotImplementedException();
        }

        public bool UpdateVodka(int id, IVodkaDto updatedVodka)
        {
            throw new NotImplementedException();
        }

        public bool DeleteVodka(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProducerData> GetProducersData()
        {
            throw new NotImplementedException();
        }

        public (bool IsSuccess, string Message) Validate(IVodkaDto vodka)
        {
            throw new NotImplementedException();
        }
    }
}
