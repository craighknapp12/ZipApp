namespace ZippyLibrary.Interfaces;
public interface IViewDialog
{
    string GetOpenFile();
    string GetSaveFile();
    bool ShouldSave();
    void ShowAbout();
    void ShowError(string message);
}
