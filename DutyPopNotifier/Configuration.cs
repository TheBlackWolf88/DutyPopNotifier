using Dalamud.Configuration;
using Dalamud.Plugin;
using System;

namespace DutyPopNotifier;

[Serializable]
public class Configuration : IPluginConfiguration
{
    public int Version { get; set; } = 0;

    public string DaemonURL{get; set;} = ""; 
    public bool OnState {get;set;} = false;
    public string Passkey {get;set;} = "";
    // the below exist just to make saving less cumbersome
    public void Save()
    {
        Plugin.PluginInterface.SavePluginConfig(this);
    }
}
