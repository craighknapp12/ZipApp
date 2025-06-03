using CommunityToolkit.Mvvm.ComponentModel;
using ZippyLibrary.Interfaces;

namespace ZippyLibrary.ViewModels;


public class ZipViewModel(IZip Zip) : ObservableObject
{
    public string FileName => Zip.FileName.Substring(Zip.FileName.LastIndexOf('\\') + 1);

    public string Action => string.IsNullOrEmpty(Zip.Action) ? "Ready" : Zip.Action;

    public double MaxValue => Zip.MaxValue;

    public double ActionValue => Zip.ActionValue;
    
    public bool IsDirty => Zip.IsDirty;
}
