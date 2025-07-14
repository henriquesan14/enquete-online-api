namespace EnqueteOnline.Application.Contracts.Services.Response
{
    public class GoogleUserResponse
    {
        public string Sub { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Picture { get; set; } = null!;
    }
}
