using System;
using System.Linq;
using System.Windows.Forms;
using HuntAndPeck.NativeMethods;

namespace HuntAndPeck.Models
{
    internal sealed class HotKey
    {
        public KeyModifier Modifier { get; set; }
        public Keys Keys { get; set; }

        /// <summary>
        /// Id of the hot key registration
        /// </summary>
        public int RegistrationId { get; set; }

        public override string ToString()
        {
            if (Keys == Keys.None && Modifier == 0) return string.Empty;

            var parts = Enum.GetValues(typeof(KeyModifier))
                            .Cast<KeyModifier>()
                            .Where(modifier => modifier != 0 && Modifier.HasFlag(modifier))
                            .Select(modifier => modifier.ToString())
                            .ToList();

            if (Keys != Keys.None)
                parts.Add(Keys.ToString());

            return string.Join(" + ", parts);
        }

        public bool IsModifierOnly() => Keys == Keys.None && Modifier != 0;

        public bool IsValid()
        {
            if (IsModifierOnly()) 
                return false;

            return Keys != Keys.None;
        }

        public static HotKey Parse(string hotKeyString)
        {
            if (!TryParse(hotKeyString, out HotKey hotKey))
                throw new FormatException("Invalid hotkey string");
            return hotKey;
        }

        public static bool TryParse(string hotKeyString, out HotKey hotKey)
        {
            hotKey = null;
            if (string.IsNullOrWhiteSpace(hotKeyString))
                return false;

            KeyModifier hotKeyModifiers = 0;
            Keys hotKeyKeys = Keys.None;

            foreach (var part in hotKeyString.Split(new[] { '+' }, 
                StringSplitOptions.RemoveEmptyEntries).Select(hotKeyPart => hotKeyPart.Trim()))
            {
                if (Enum.TryParse<KeyModifier>(part, true, out KeyModifier keyModifier))
                {
                    hotKeyModifiers |= keyModifier;
                    continue;
                }

                switch (part.ToLowerInvariant())
                {
                    case "ctrl":
                        hotKeyModifiers |= KeyModifier.Control;
                        continue;
                    case "win":
                        hotKeyModifiers |= KeyModifier.Windows;
                        continue;
                    case "alt":
                        hotKeyModifiers |= KeyModifier.Alt;
                        continue;
                    case "shift":
                        hotKeyModifiers |= KeyModifier.Shift;
                        continue;
                }

                if (Enum.TryParse<Keys>(part, true, out Keys keys))
                {
                    hotKeyKeys = keys;
                    continue;
                }
            }

            var newHotKey = new HotKey { Keys = hotKeyKeys, Modifier = hotKeyModifiers };
            if (!newHotKey.IsValid()) 
                return false;

            hotKey = newHotKey;
            return true;
        }
    }
}
