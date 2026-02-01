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

        public void Destroy()
        {
            Settings.Default.Save();
        }

        public void SaveSettings()
        {
            Settings.Default.Save();
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
                }
            }
        }

        private string _HotKeyText; 
        public string HotKeyText
        {
            get { return _HotKeyText; }
            set
            {
                if (HotKey.TryParse(value, out HotKey hotKey))
                {
                    _HotKeyText = hotKey.ToString();
                    OnPropertyChanged("HotKeyText");
                    Settings.Default.HotKeyText = hotKey.ToString();
                }
            }
        }

        private string _TaskbarHotKeyText;
        public string TaskbarHotKeyText
        {
            get { return _TaskbarHotKeyText; }
            set
            {
                if (HotKey.TryParse(value, out HotKey hotKey))
                {
                    _TaskbarHotKeyText = hotKey.ToString();
                    OnPropertyChanged("TaskbarHotKeyText");
                    Settings.Default.TaskbarHotKeyText = hotKey.ToString();
                }
            }
        }   

        private string _DebugHotKeyText;    
        public string DebugHotKeyText
        {
            get { return _DebugHotKeyText; }
            set
            {
                if (HotKey.TryParse(value, out HotKey hotKey))
                {
                    _DebugHotKeyText = hotKey.ToString();
                    OnPropertyChanged("DebugHotKeyText");
                    Settings.Default.DebugHotKeyText = hotKey.ToString();
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