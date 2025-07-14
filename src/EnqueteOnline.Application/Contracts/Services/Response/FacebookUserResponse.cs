namespace EnqueteOnline.Application.Contracts.Services.Response
{
    public class FacebookUserResponse
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public FacebookPictureData Picture { get; set; } = null!;
    }

    public class FacebookPictureData
    {
        public FacebookPictureInfo Data { get; set; } = null!;
    }

    public class FacebookPictureInfo
    {
        public string Url { get; set; } = null!;
    }
}
