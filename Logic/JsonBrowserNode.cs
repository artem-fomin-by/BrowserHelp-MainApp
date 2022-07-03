using System.Diagnostics;

namespace Logic;

public class Config
{
    public Browser[] Browsers { get; set; }

    public Browser? FindBrowser(string parentProcessProcessName)
    {
        return Browsers?.FirstOrDefault(x => x.Applications?.Contains(parentProcessProcessName) == true);
    }
}
