using System;

namespace DScriptEngine {
    public static class Ext {
        public static void Emit<TEventArgs>(this EventHandler<TEventArgs> eventHandler, object sender, TEventArgs e)
            where TEventArgs : EventArgs {
            eventHandler?.Invoke(sender, e);
        }
    }
}
