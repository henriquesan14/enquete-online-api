using EnqueteOnline.Application.ViewModels;
using EnqueteOnline.Domain.Entities;

namespace EnqueteOnline.Application.Extensions
{
    public static class VotoExtensions
    {
        public static IEnumerable<VotoViewModel> ToViewModelList(this IEnumerable<Voto> votos)
        {
            return votos.Select(voto => EntityToViewModel(voto!));
        }

        public static VotoViewModel ToViewModel(this Voto voto)
        {
            return EntityToViewModel(voto);
        }

        private static VotoViewModel EntityToViewModel(Voto voto)
        {
            return new VotoViewModel
            (
                Id: voto.Id.Value,
                EnqueteId: voto.EnqueteId.Value,
                OpcaoEnqueteId: voto.OpcaoEnqueteId.Value,
                Ip: voto.Ip
            );
        }
    }
}
