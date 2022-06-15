using System.Diagnostics;

namespace Logic;

public class Browser{
    public readonly string Name;
    public readonly string LaunchCommand;

    public Browser(string name, string launchCommand){
        Name = name;
        LaunchCommand = launchCommand;
    }

    public void Launch(){
        Process.Start(LaunchCommand);
    }

    public void Launch(string inp){
        Process.Start(LaunchCommand, inp);
    }
}
