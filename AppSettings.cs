namespace ContactsManagerAPI
{
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; } = new();
    }

    public class ConnectionStrings
    {
        public string ContactsDbContext { get; set; } 
    }
}
