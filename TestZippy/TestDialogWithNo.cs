using ZippyLibrary.Interfaces;

namespace TestZippy;

public class TestDialogWithNo : IViewDialog
{
    public int GetOpenFileCount { get; set; } = 0;
    public int GetSaveFileCount { get; set; } = 0;
    public int ShowAboutCount { get; set; } = 0;
    public int ShowErrorCount { get; set; } = 0;
    public int ShouldSaveCount { get; set; } = 0;
    public string GetOpenFile()
    {
        GetOpenFileCount++;
        return Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "test.zip"; ;
    }

    public string GetSaveFile()
    {
        GetSaveFileCount++;
        return Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "test.zip"; ;
    }

    public bool ShouldSave()
    {
        ShouldSaveCount++;
        return false;
    }

    public void ShowAbout()
    {
        ShowAboutCount++;
    }

    public void ShowError(string message)
    {
        ShowErrorCount++;
    }
}
