using CommunityToolkit.Mvvm.ComponentModel;
using ZippyLibrary.Interfaces;

namespace ZippyLibrary.ViewModels;


public class ZipViewModel(IZip Zip) :  ObservableObject
{
    public string FileName => Zip.FileName.Substring(Zip.FileName.LastIndexOf('\\')+1);


    public string Action => "Ready";

    private double maxValue;

    public double MaxValue { get => maxValue; set => SetProperty(ref maxValue, value); }

    private double actionValue;

    public double ActionValue { get => actionValue; set => SetProperty(ref actionValue, value); }

    public bool IsDirty => Zip.IsDirty;
}
