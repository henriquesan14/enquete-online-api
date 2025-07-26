using EnqueteOnline.Application.ViewModels;
using EnqueteOnline.Domain.Entities;
using EnqueteOnline.Domain.ValueObjects;

namespace EnqueteOnline.Application.Extensions
{
    public static class EnqueteExtensions
    {
        public static IEnumerable<EnqueteViewModel> ToViewModelList(this IEnumerable<Enquete> enquetes)
        {
            return enquetes.Select(enquete => EntityToViewModel(enquete!));
        }

        public static EnqueteViewModel ToViewModel(this Enquete enquete)
        {
            return EntityToViewModel(enquete);
        }

        private static EnqueteViewModel EntityToViewModel(Enquete enquete)
        {
            var totalVotos = enquete.Votos.Count;

            var votosPorOpcao = enquete.Votos
                .GroupBy(v => v.OpcaoEnqueteId)
                .ToDictionary(g => g.Key, g => g.Count());

            var opcoes = enquete.Opcoes
                .Select(opcao =>
                {
                    var votos = votosPorOpcao.TryGetValue(OpcaoEnqueteId.Of(opcao.Id.Value), out var count) ? count : 0;
                    var porcentagem = totalVotos == 0 ? 0 : Math.Round((double)votos / totalVotos * 100, 2);

                    return new OpcaoEnqueteViewModel(
                        Id: opcao.Id.Value,
                        Descricao: opcao.Descricao,
                        QuantidadeVotos: votos,
                        Porcentagem: porcentagem
                    );
                })
                .OrderByDescending(o => o.QuantidadeVotos) // Ordenação aplicada aqui
                .ToList();

            return new EnqueteViewModel
            (
                Id: enquete.Id.Value,
                Titulo: enquete.Titulo,
                Descricao: enquete.Descricao,
                Encerramento: enquete.Encerramento,
                Opcoes: opcoes,
                CreatedBy: enquete.CreatedBy!.Value
            );
        }
    }
}
