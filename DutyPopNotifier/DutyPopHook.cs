using System;
using Dalamud.Hooking;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Client.UI.Misc;
using static FFXIVClientStructs.FFXIV.Client.Game.Event.EventFramework.Delegates;
using FFXIVClientStructs.FFXIV.Client.Game.UI;

namespace DutyPopNotifier;

unsafe public class MyHook : IDisposable {
    private delegate void SetSavePendingDelegate(AddonContentsFinder* self, byte needsSave, uint set);

    private readonly Hook<SetSavePendingDelegate>? _macroUpdateHook;

    public MyHook() {

        var cfqi = ContentsFinderQueueInfo.QueusState;

        this._macroUpdateHook = Plugin.GameInteropProvider.HookFromAddress<AddonContentsFinder>(
            (nint) ContentsFinderQueueInfo.,
            this.DetourSetSavePending
        );

        this._macroUpdateHook.Enable();
    }

    public void Dispose() {
        if (this._macroUpdateHook) this._macroUpdateHook.Dispose();
    }

    private nint DetourSetSavePending(AddonContentsFinder* self, byte needsSave, uint set) {
        Plugin.Log.Information("A macro save happened!");

        try {
            // your plugin logic goes here.
        } catch (Exception ex) {
            Plugin.Log.Error(ex, "An error occured when handling a macro save event.");
        }

        return this._macroUpdateHook.Original(self, needsSave, set);
    }
}
