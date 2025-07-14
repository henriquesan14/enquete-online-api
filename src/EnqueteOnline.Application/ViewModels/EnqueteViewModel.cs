namespace EnqueteOnline.Application.ViewModels
{
    public record EnqueteViewModel(Guid Id, string Titulo, string Descricao, DateTime Encerramento, List<OpcaoEnqueteViewModel> Opcoes, Guid CreatedBy);
}
