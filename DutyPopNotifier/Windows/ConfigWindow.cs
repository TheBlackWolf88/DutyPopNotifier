using System;
using System.Numerics;
using Dalamud.Interface.Windowing;
using ImGuiNET;

namespace DutyPopNotifier.Windows;

public class ConfigWindow : Window, IDisposable
{
    private Configuration Configuration;

    // We give this window a constant ID using ###
    // This allows for labels being dynamic, like "{FPS Counter}fps###XYZ counter window",
    // and the window ID will always be "###XYZ counter window" for ImGui
    public ConfigWindow(Plugin plugin) : base("DPN Config##dpn_config")
    {
        Flags = ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar |
                ImGuiWindowFlags.NoScrollWithMouse;

        Size = new Vector2(232, 190);
        SizeCondition = ImGuiCond.Always;

        Configuration = plugin.Configuration;
    }

    public void Dispose() { }

    public override void PreDraw()
    {
        // Flags must be added or removed before Draw() is being called, or they won't apply
    }

    public override void Draw()
    {
        string url_string = Configuration.DaemonURL;
        if (ImGui.InputText("URL srting", ref url_string, 64))
        {
            Configuration.DaemonURL = url_string;
            Configuration.Save();
        }

        string pass = Configuration.Passkey;
        if (ImGui.InputText("Unique ID", ref pass, 64))
        {
            Configuration.Passkey = pass;
            Configuration.Save();
        }
    }
}
