using EnqueteOnline.Domain.Abstractions;
using EnqueteOnline.Domain.ValueObjects;

namespace EnqueteOnline.Domain.Entities
{
    public class Voto : Aggregate<VotoId>
    {
        public EnqueteId EnqueteId { get; private set; } = default!;
        public Enquete Enquete { get; private set; } = default!;

        public OpcaoEnqueteId OpcaoEnqueteId { get; private set; } = default!;
        public OpcaoEnquete Opcao { get; private set; }  = default!;

        public string Ip { get; private set; } = default!;

        public static Voto Create(EnqueteId EnqueteId, OpcaoEnqueteId OpcaoEnqueteId, string Ip)
        {
            return new Voto
            {
                Id = VotoId.Of(Guid.NewGuid()),
                EnqueteId = EnqueteId,
                OpcaoEnqueteId  = OpcaoEnqueteId,
                Ip = Ip
            };
        }

    }
}
