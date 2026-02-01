# hunt-and-peck

Simple vimium/vimperator style navigation for Windows applications based on the UI Automation framework. In essence, it works the same as screen readers or accessibility programs but with the goal of making any Windows program faster to use.

It works for any Windows program (excluding Modern UI apps :))

## Screenshots

![ScreenShot](https://raw.github.com/zsims/hunt-n-peck/master/screenshots/explorer.png)
![ScreenShot](https://raw.github.com/zsims/hunt-n-peck/master/screenshots/visual-studio.png)

## Installation


### Requirements
- Windows OS
- .NET Framework

### Download & Setup

1. Download the latest release from the [Releases page](https://github.com/Fettsackmitch/hunt-and-peck/releases)
2. Extract the ZIP file
3. Run `hap.exe`

## Usage

### Basic Usage

1. Launch the executable
2. With any window focused, press `Alt + ,`
   - The tray can be highlighted with `Shift + Alt + ,`
3. An overlay window will be displayed, type any of the hint characters you see

### Options & Configuration

To access the options dialog, right-click the application icon in the system tray and select `Options`.

#### Available Settings

- **Font Size**: Adjust the size of hint labels (8-24pt) for better visibility
- **Hotkeys**: Configure keyboard shortcuts for different modes:
  - **Overlay HotKey**: Main activati on hotkey for standard windows (default: `Shift + Alt + ,`)
  - **Taskbar HotKey**: Activation hotkey for taskbar elements (default: `Ctrl + ;`)
  
Hotkeys support various modifier combinations (Alt, Ctrl, Shift, Win) and can be customized to your preference.

## Known Limitations

- Modern UI apps are not supported
- Only elements with "Invoke" patterns can be interacted with
- Functionality depends on application's UI Automation implementation

## Credits

This project is a fork of the original [hunt-and-peck](https://github.com/zsims/hunt-and-peck) by [@zsims](https://github.com/zsims).

## Disclaimer

This fork is primarily developed for **personal use and learning purposes**. I'm experimenting with the codebase to improve my skills and adapt it to my specific needs.

That said, **suggestions and feedback are always welcome!** If you have ideas for improvements or find bugs, feel free to open an issue or submit a pull request.