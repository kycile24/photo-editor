[![Photo Editor — Windows workspace](docs/cover.svg)](https://github.com/kycile24/photo-editor/releases/latest/download/PhotoEditor-win-x64.zip)

A Windows desktop editor for local photo processing. Open a collection of photos, adjust color or framing, and compare the result with the original before exporting.

**[Download for Windows x64](https://github.com/kycile24/photo-editor/releases/latest/download/PhotoEditor-win-x64.zip)** · [Release history](https://github.com/kycile24/photo-editor/releases)

Extract the ZIP and open **PhotoEditor.exe**. The .NET 10 runtime is included. Select **EN** or **УКР** in the toolbar to change the interface language.

![Desktop editor with split comparison](https://github.com/kycile24/photo-editor/releases/download/v1.4.0/workspace.png)

## From original to export

1. Drop a photo into the window or press **Ctrl+O**. A [sample image](examples/preview.jpg) is included.
2. Choose **Color balance and brightness** and adjust the sliders.
3. Select **Compare with original** and move the divider. Press **F11** for fullscreen and **Esc** to restore the workspace.
4. Press **Ctrl+Shift+S** to export the edited preview as a separate PNG.

The editor also supports batch resizing and format conversion, cropping and tiling, watermarks, merging, brightness histograms and noise filter comparison with CSV export.

## Development

The Windows Forms interface lives in `src/PhotoEditor/UI`. Pixel transforms, filters and metrics live in `src/PhotoEditor/Imaging`.

With the .NET 10 SDK on Windows:

```powershell
dotnet run --project src/PhotoEditor/PhotoEditor.csproj
```

GitHub Actions verifies the workspace, builds a standalone x64 package, and publishes versioned releases.
