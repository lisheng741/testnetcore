namespace ReloadTest;

public static class AppSettings
{
    public static IConfiguration Configuration { get; set; }

    public static string Test => Configuration["Test"];
}
