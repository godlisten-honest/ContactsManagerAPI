namespace ContactsManagerAPI
{
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; } = new();
        public Jwt Jwt { get; set; } = new();
    }

    public class ConnectionStrings
    {
        public string ContactsDbContext { get; set; } 
    }

    public class Jwt
    {
        public string IdentityBaseUrl { get; set; }
        public string Audience { get; set; }
    }
}
