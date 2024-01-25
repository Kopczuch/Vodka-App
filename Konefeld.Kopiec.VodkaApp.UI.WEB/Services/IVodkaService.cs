using Konefeld.Kopiec.VodkaApp.Interfaces;
using Konefeld.Kopiec.VodkaApp.UI.WEB.Models;
using Konefeld.Kopiec.VodkaApp.UI.WEB.Models.ViewModels;

namespace Konefeld.Kopiec.VodkaApp.UI.WEB.Services
{
    public interface IVodkaService
    {
        int CreateVodka(IVodkaDto newVodka);
        IVodkaDto GetVodka(int id);
        IList<VodkaViewModel> GetVodkas();
        IList<VodkaViewModel> GetFilteredVodkas(IVodkaFilter filter);
        bool UpdateVodka(int id, IVodkaDto updatedVodka);
        bool DeleteVodka(int id);
        IList<ProducerData> GetProducersData();
        (bool IsSuccess, string Message) Validate(IVodkaDto vodka);
    }
}
