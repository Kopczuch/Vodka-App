using Konefeld.Kopiec.VodkaApp.Interfaces;
using Konefeld.Kopiec.VodkaApp.UI.WEB.Models.Dto;
using Konefeld.Kopiec.VodkaApp.UI.WEB.Models.ViewModels;

namespace Konefeld.Kopiec.VodkaApp.UI.WEB.Services
{
    public interface IProducerService
    {
        int CreateProducer(IProducerDto newProducer);
        ProducerDto GetProducer(int id);
        IList<ProducerViewModel> GetProducers();
        IList<ProducerViewModel> GetFilteredProducers(IProducerFilter filter);
        bool UpdateProducer(int id, IProducerDto updatedProducer);
        bool DeleteProducer(int id);
        (bool IsSuccess, string Message) Validate(IProducerDto producer);
    }
}
