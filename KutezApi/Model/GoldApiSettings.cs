namespace KutezApi.Model
{
    public class GoldApiSettings
    {
        public string ApiKey { get; set; } = string.Empty; // Güvenlik açısından appsettings üzerinden alınması doğru
        public string Endpoint { get; set; } = string.Empty; // Flexible ve test edilebilir bir yapı
    }
}
