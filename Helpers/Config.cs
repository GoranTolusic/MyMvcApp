using Microsoft.Extensions.Configuration;

public class Config
{
    private readonly IConfiguration _configuration;

    private static Config config = Config.GetInstance();

    public Config(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string findByProp(string prop)
    {
        return _configuration[prop];
    }

    public static Config GetInstance()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) 
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        IConfigurationRoot configuration = builder.Build();
        return new Config(configuration);
    }

    public static string GetEnv(string prop)
    {
        return Config.config.findByProp(prop);
    }
}