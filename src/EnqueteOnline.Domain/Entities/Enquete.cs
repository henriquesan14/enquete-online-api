using EnqueteOnline.Domain.Abstractions;
using EnqueteOnline.Domain.Exceptions;
using EnqueteOnline.Domain.ValueObjects;

namespace EnqueteOnline.Domain.Entities
{
    public class Enquete : Aggregate<EnqueteId>
    {
        public string Titulo { get; private set; } = default!;
        public string Descricao { get; private set; } = default!;

        public DateTime Encerramento { get; private set; } = default!;

        public static Enquete Create(string titulo, string descricao, DateTime encerramento, List<string> opcoes) {
            var enquete = new Enquete {
                Id = EnqueteId.Of(Guid.NewGuid()),
                Titulo = titulo,
                Descricao = descricao,
                Encerramento = encerramento,
            };

            foreach (var opcao in opcoes)
            {
                enquete._opcoes.Add(OpcaoEnquete.Create(opcao));
            }

            return enquete;
        }

        public void Update(string titulo, string descricao, DateTime encerramento, List<string> novasOpcoes)
        {
            if (!PodeEditar)
                throw new EnqueteJaPossuiVotosException();
            Titulo = titulo;
            Descricao = descricao;
            Encerramento = encerramento;

            _opcoes.Clear();

            foreach (var opcao in novasOpcoes)
            {
                _opcoes.Add(OpcaoEnquete.Create(opcao));
            }
        }
        public bool PodeEditar => Votos.Count <= 0;

        private readonly List<OpcaoEnquete> _opcoes = new();
        public IReadOnlyCollection<OpcaoEnquete> Opcoes => _opcoes.AsReadOnly();
        public IReadOnlyCollection<Voto> Votos { get; private set; } = new List<Voto>();
    }
}
