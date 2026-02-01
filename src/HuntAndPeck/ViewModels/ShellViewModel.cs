using System;
using HuntAndPeck.NativeMethods;
using HuntAndPeck.Services.Interfaces;
using Application = System.Windows.Application;
using HuntAndPeck.Models;

namespace HuntAndPeck.ViewModels
{
    internal class ShellViewModel
    {
        private readonly Action<OverlayViewModel> _showOverlay;
        private readonly Action<DebugOverlayViewModel> _showDebugOverlay;
        private readonly Action<OptionsViewModel> _showOptions;
        private readonly IHintLabelService _hintLabelService;
        private readonly IHintProviderService _hintProviderService;
        private readonly IDebugHintProviderService _debugHintProviderService;
        private readonly IKeyListenerService _keyListener;

        public ShellViewModel(
            Action<OverlayViewModel> showOverlay,
            Action<DebugOverlayViewModel> showDebugOverlay,
            Action<OptionsViewModel> showOptions,
            IHintLabelService hintLabelService,
            IHintProviderService hintProviderService,
            IDebugHintProviderService debugHintProviderService,
            IKeyListenerService keyListener)
        {
            _showOverlay = showOverlay;
            _showDebugOverlay = showDebugOverlay;
            _showOptions = showOptions;
            _hintLabelService = hintLabelService;
            _hintProviderService = hintProviderService;
            _debugHintProviderService = debugHintProviderService;
            _keyListener = keyListener;
            ShowOptionsCommand = new DelegateCommand(ShowOptions);
            ExitCommand = new DelegateCommand(Exit);
        }

        public void InitHotKeys()
        {
            if (HotKey.TryParse(Properties.Settings.Default.HotKeyText, out HotKey activeWindowHotKey)
                && activeWindowHotKey.IsValid())
            {
                _keyListener.HotKey = activeWindowHotKey;
            }

            if (HotKey.TryParse(Properties.Settings.Default.TaskbarHotKeyText, out HotKey taskbarHotKey)
                && taskbarHotKey.IsValid())
            {
                _keyListener.TaskbarHotKey = taskbarHotKey;
            }
#if DEBUG
            if (HotKey.TryParse(Properties.Settings.Default.DebugHotKeyText, out HotKey debugHotKey)
                && debugHotKey.IsValid())
            {
                _keyListener.DebugHotKey = debugHotKey;
            }
#endif
            _keyListener.OnHotKeyActivated += _keyListener_OnHotKeyActivated;
            _keyListener.OnTaskbarHotKeyActivated += _keyListener_OnTaskbarHotKeyActivated;
            _keyListener.OnDebugHotKeyActivated += _keyListener_OnDebugHotKeyActivated;
        }

        public DelegateCommand ShowOptionsCommand { get; }
        public DelegateCommand ExitCommand { get; }

        private void _keyListener_OnHotKeyActivated(object sender, EventArgs e)
        {
            var session = _hintProviderService.EnumHints();
            if (session != null)
            {
                var vm = new OverlayViewModel(session, _hintLabelService);
                _showOverlay(vm);
            }
        }

        private void _keyListener_OnTaskbarHotKeyActivated(object sender, EventArgs e)
        {
            var taskbarHWnd = User32.FindWindow("Shell_traywnd", "");
            var session = _hintProviderService.EnumHints(taskbarHWnd);
            if (session != null)
            {
                var vm = new OverlayViewModel(session, _hintLabelService);
                _showOverlay(vm);
            }
        }

        private void _keyListener_OnDebugHotKeyActivated(object sender, EventArgs e)
        {
            var session = _debugHintProviderService.EnumDebugHints();
            if (session != null)
            {
                var vm = new DebugOverlayViewModel(session);
                _showDebugOverlay(vm);
            }
        }

        public void Exit()
        {
            Application.Current.Shutdown();
        }

        public void ShowOptions()
        {
            var vm = new OptionsViewModel();
            _showOptions(vm);
        }
    }
}
