using ZippyLibrary.Models;

namespace ZippyLibrary.Interfaces;
public interface IViewDialog
{
    AddZipContent GetAddInformation();
    string GetOpenFile();
    RemoveZipContent GetRemoveInformation();
    string GetSaveFile();
    bool ShouldSave();
    void ShowAbout();
    void ShowError(string message);
}
