namespace ZippyLibrary.Interfaces;

public interface IZip
{
    string FileName { get;  }
    bool IsDirty { get; }
    bool ExecuteNew();
    bool ExecuteOpen();
    bool ExecuteSave(bool overrideCheck = false);
    bool ExecuteSaveAs();
    void Load(string[] strings);
    void ShowAbout();
}
