using System;
using System.Numerics;
using Dalamud.Interface.Internal;
using Dalamud.Interface.Utility;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin.Services;
using Dalamud.Logging;
using ImGuiNET;

namespace DutyPopNotifier.Windows;

public class MainWindow : Window, IDisposable
{
    private Plugin Plugin;

    // We give this window a hidden ID using ##
    // So that the user will see "My Amazing Window" as window title,
    // but for ImGui the ID is "My Amazing Window##With a hidden ID"
    public MainWindow(Plugin plugin)
        : base("DutyPopNotifier##dpn_main", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(150, 150),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };

        Plugin = plugin;
    }

    public void Dispose() { }

    public override void Draw()
    {
        if (String.IsNullOrEmpty(Plugin.Configuration.DaemonURL) || String.IsNullOrEmpty(Plugin.Configuration.Passkey))
        {
            ImGui.Text("You need to have your config filled!");
        }
        else
        {
            if (Plugin.Configuration.OnState)
            {
                ImGui.Text("The service is running.");
            }
            else
            {
                ImGui.Text("The service is not running.");
            }

            if (ImGui.Button("Start Service"))
            {
                if (!Plugin.Configuration.OnState)
                {
                    Plugin.Configuration.OnState = true;
                    Plugin.Log.Debug("start");
                }
            }

            if (ImGui.Button("Stop Service"))
            {
                if (Plugin.Configuration.OnState)
                {
                    Plugin.Configuration.OnState = false;
                    Plugin.Log.Debug("stop");
                }
            }
        }

        ImGui.Spacing();

        if (ImGui.Button("Config"))
        {
            Plugin.ToggleConfigUI();
        }

    }
}
