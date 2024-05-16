namespace Infrastructure.Services.Options
{
    public class GigaChatOptions
    {
        public string AuthData { get; set; } = string.Empty;
        public bool IsCommercial { get; set; } = false;
        public bool IgnoreTLS { get; set; } = true;
        public bool SaveImage { get; set; } = true;
    }
}
