namespace ZippyLibrary.Interfaces;
public interface IViewDialog
{
    string GetOpenFile();
    string GetSaveFile();
    bool ShouldSave();
    void ShowAdd();
    void ShowAbout();
    void ShowError(string message);
}
