using System;
using Dalamud.Hooking;

namespace DutyPopNotifier;

public class DutyPopHook : System.IDisposable
{

    private Plugin Plugin;
    unsafe private delegate void SetSavePendingDelegate(object* self, byte needsSave, uint set);

    private readonly Hook<SetSavePendingDelegate>? _CfUpdateHook;

    public DutyPopHook(Plugin plugin) {
        this.Plugin = plugin;
        this._CfUpdateHook = Plugin.GameInteropProvider.HookFromAddress<>;
    }
    
    

    public void Dispose()
    {
    }

    public void Watch() {
    }
}
