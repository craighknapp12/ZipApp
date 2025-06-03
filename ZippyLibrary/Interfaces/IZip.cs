using ZippyLibrary.Models;

namespace ZippyLibrary.Interfaces;

public interface IZip
{
    string FileName { get;  }
    bool IsDirty { get; }
    public string Action { get;  }
    public double ActionValue { get; }
    public double MaxValue { get;  }
    bool ExecuteNew();
    bool ExecuteOpen();
    bool ExecuteSave(bool overrideCheck = false);
    bool ExecuteSaveAs();
    void Load(string[] strings);
    void ShowAbout();

    bool Add(AddZipContent addContent);
    bool Remove(RemoveZipContent removeContent);
}
