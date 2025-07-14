using EnqueteOnline.Domain.Abstractions;
using EnqueteOnline.Domain.ValueObjects;

namespace EnqueteOnline.Domain.Entities
{
    public class Enquete : Aggregate<EnqueteId>
    {
        public string Titulo { get; private set; } = default!;
        public string Descricao { get; private set; } = default!;

        public DateTime Encerramento { get; private set; } = default!;

        public static Enquete Create(string titulo, string descricao, DateTime encerramento, List<OpcaoEnquete> opcoes) {
            return new Enquete {
                Id = EnqueteId.Of(Guid.NewGuid()),
                Titulo = titulo,
                Descricao = descricao,
                Encerramento = encerramento,
                Opcoes = opcoes
            };
        }

        public IReadOnlyCollection<OpcaoEnquete> Opcoes { get; private set; } = new List<OpcaoEnquete>();
        public IReadOnlyCollection<Voto> Votos { get; private set; } = new List<Voto>();
    }
}
