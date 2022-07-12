using System.Text.Json;

namespace Logic;

public class Config
{
    public Browser[] Browsers { get; set; }

    public Browser? FindBrowser(string link, string? parentProcessProcessName)
    {
        var linkMatch = Browsers.FirstOrDefault(x => x.IsLinkMatch(link));
        if(linkMatch != null) return linkMatch;
        
        if(parentProcessProcessName != null)
        {
            return Browsers
                .FirstOrDefault(x => x.Applications?.Contains(parentProcessProcessName) == true);
        }

        return null;
    }

    public void Deserialize(string configurationFilePath)
    {
        using var configurationFile = File.Open(configurationFilePath, FileMode.Open, FileAccess.Write);
        JsonSerializer.Serialize(
            configurationFile, 
            this, 
            new JsonSerializerOptions { WriteIndented = true }
            );
    }
}
