using System.Text.Json;

namespace Logic;

public class Config
{
    public Browser[] Browsers { get; set; }

    public Browser? FindBrowser(string parentProcessProcessName)
    {
        return Browsers?.FirstOrDefault(x => x.Applications?.Contains(parentProcessProcessName) == true);
    }

    public void Deserialize(string configurationFilePath)
    {
        using var configurationFile = File.Create(configurationFilePath);
        JsonSerializer.Serialize(
            configurationFile, 
            this, 
            new JsonSerializerOptions { WriteIndented = true }
            );
    }
}
