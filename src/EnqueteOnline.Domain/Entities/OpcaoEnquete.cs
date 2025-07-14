using EnqueteOnline.Domain.Abstractions;
using EnqueteOnline.Domain.ValueObjects;

namespace EnqueteOnline.Domain.Entities
{
    public class OpcaoEnquete : Aggregate<OpcaoEnqueteId>
    {
        public EnqueteId EnqueteId { get; private set; } = default!;
        public Enquete Enquete { get; private set; } = default!;

        public string Descricao { get; set; } = default!;

        public static OpcaoEnquete Create(string descricao)
        {
            return new OpcaoEnquete
            {
                Id = OpcaoEnqueteId.Of(Guid.NewGuid()),
                Descricao = descricao
            };
        }
    }
}
