using HuntAndPeck.Properties;
using System.ComponentModel;

namespace HuntAndPeck.ViewModels
{
    internal class OptionsViewModel : INotifyPropertyChanged
    {
        public OptionsViewModel()
        {
            DisplayName = "Options";
            FontSize = Settings.Default.FontSize;
            HotKeyText = Settings.Default.HotKeyText;
            TaskbarHotKeyText = Settings.Default.TaskbarHotKeyText;
            DebugHotKeyText = Settings.Default.DebugHotKeyText;
            
            Settings.Default.PropertyChanged += OnSettingsPropertyChanged;
        }

        public string DisplayName { get; set; }

        private string _fontSize;
        public string FontSize
        // Assign the font size value to a variable and update it every time user 
        // changes the option in tray menu
        {
            get { return _fontSize; }
            set
            {
                if (_fontSize != value)
                {
                    _fontSize = value;
                    OnPropertyChanged("FontSize");
                    Settings.Default.FontSize = value;
                    Settings.Default.Save();
                }
            }
        }

        private string _HotKeyText; 
        public string HotKeyText
        {
            get { return _HotKeyText; }
            set
            {
                if (_HotKeyText != value)
                {
                    _HotKeyText = value;
                    OnPropertyChanged("HotKeyText");
                    Settings.Default.HotKeyText = value;
                    Settings.Default.Save();
                }
            }
        }

        private string _TaskbarHotKeyText;
        public string TaskbarHotKeyText
        {
            get { return _TaskbarHotKeyText; }
            set
            {
                if (_TaskbarHotKeyText != value)
                {
                    _TaskbarHotKeyText = value;
                    OnPropertyChanged("TaskbarHotKeyText");
                    Settings.Default.TaskbarHotKeyText = value;
                    Settings.Default.Save();
                }
            }
        }   

        private string _DebugHotKeyText;    
        public string DebugHotKeyText
        {
            get { return _DebugHotKeyText; }
            set
            {
                if (_DebugHotKeyText != value)
                {
                    _DebugHotKeyText = value;
                    OnPropertyChanged("DebugHotKeyText");
                    Settings.Default.DebugHotKeyText = value;
                    Settings.Default.Save();
                }
            }
        }


        private void OnSettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "FontSize")
            {
                FontSize = Settings.Default.FontSize;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}