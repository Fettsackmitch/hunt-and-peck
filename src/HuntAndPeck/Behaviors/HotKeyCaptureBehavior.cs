using HuntAndPeck.NativeMethods;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HuntAndPeck.Models;

namespace HuntAndPeck.Behaviors
{
    public static class HotKeyCaptureBehavior
    {
        #region IsEnabled Attached Property
        
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsEnabled",                 
                typeof(bool),                       
                typeof(HotKeyCaptureBehavior),       
                new PropertyMetadata(false, OnIsEnabledChanged));
        
        public static bool GetIsEnabled(DependencyObject dependencyObject)
        {
            return (bool)dependencyObject.GetValue(IsEnabledProperty);
        }

        public static void SetIsEnabled(DependencyObject dependencyObject, bool value)
        {
            dependencyObject.SetValue(IsEnabledProperty, value);
        }

        private static void OnIsEnabledChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs changedEvent)
        {
            if (dependencyObject is TextBox textBox)
            {
                if ((bool)changedEvent.NewValue) 
                {
                    textBox.PreviewKeyDown += TextBox_PreviewKeyDown;
                    textBox.Cursor = Cursors.Hand;
                }
                else 
                {
                    textBox.PreviewKeyDown -= TextBox_PreviewKeyDown;
                }
            }
        }

        #endregion

        #region HotKeyText Attached Property

        public static readonly DependencyProperty HotKeyTextProperty =
            DependencyProperty.RegisterAttached(
                "HotKeyText",
                typeof(string),
                typeof(HotKeyCaptureBehavior),
                new FrameworkPropertyMetadata(
                    string.Empty,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnHotKeyTextChanged));

        public static string GetHotKeyText(DependencyObject dependencyObject)
        {
            return (string)dependencyObject.GetValue(HotKeyTextProperty);
        }

        public static void SetHotKeyText(DependencyObject dependencyObject, string value)
        {
            dependencyObject.SetValue(HotKeyTextProperty, value);
        }

        private static void OnHotKeyTextChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs changedEvent)
        {
            if (dependencyObject is TextBox textBox && changedEvent.NewValue is string newText)
            {
                textBox.Text = newText;
            }
        }

        #endregion

        #region Event Handlers - DIE EIGENTLICHE LOGIK

        private static void TextBox_PreviewKeyDown(object sender, KeyEventArgs keyDownEvent)
        {
            keyDownEvent.Handled = true;
            
            var textBox = (TextBox)sender;
            Key key = keyDownEvent.Key == Key.System ? keyDownEvent.SystemKey : keyDownEvent.Key;
            if (IsModifierKey(key))
            {
                return;
            }
            KeyModifier modifiers = GetCurrentModifiers();
            
            string hotKeyString = "";
            if (modifiers.HasFlag(KeyModifier.Control))
                hotKeyString += "Ctrl + ";
            if (modifiers.HasFlag(KeyModifier.Alt))
                hotKeyString += "Alt + ";
            if (modifiers.HasFlag(KeyModifier.Shift))
                hotKeyString += "Shift + ";
            if (modifiers.HasFlag(KeyModifier.Windows))
                hotKeyString += "Win + ";
            hotKeyString += key.ToString();
            
            textBox.Text = hotKeyString;
            
            if (!HotKey.TryParse(hotKeyString, out HotKey hotKey))
                return;

            SetHotKeyText(textBox, hotKey.ToString()); 
        }

        #endregion

        #region Helper Methods

        private static KeyModifier GetCurrentModifiers()
        {
            KeyModifier modifiers = 0;
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                modifiers |= KeyModifier.Control;
            if (Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt))
                modifiers |= KeyModifier.Alt;
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                modifiers |= KeyModifier.Shift;
            if (Keyboard.IsKeyDown(Key.LWin) || Keyboard.IsKeyDown(Key.RWin))
                modifiers |= KeyModifier.Windows;
            
            return modifiers;
        }

        private static bool IsModifierKey(Key key)
        {
            return key == Key.LeftCtrl || key == Key.RightCtrl ||
                   key == Key.System || key == Key.RightAlt ||
                   key == Key.LeftShift || key == Key.RightShift ||
                   key == Key.LWin || key == Key.RWin;
        }

        #endregion
    }
}