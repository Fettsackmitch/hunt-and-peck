using System;
using HuntAndPeck.Models;

namespace HuntAndPeck.Services.Interfaces
{
    /// <summary>
    /// Service for listening to global keyboard shortcuts
    /// </summary>
    internal interface IKeyListenerService
    {
        event EventHandler OnHotKeyActivated;
        event EventHandler OnTaskbarHotKeyActivated;
        event EventHandler OnDebugHotKeyActivated;

        HotKey TaskbarHotKey { get; set; }
        HotKey HotKey { get; set; }
        HotKey DebugHotKey { get; set; }
    }
}
