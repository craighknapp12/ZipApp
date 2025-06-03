using CommunityToolkit.Mvvm.ComponentModel;

namespace Zipper.ViewModels;
public class AddDialogViewModel : ObservableObject
{

    private bool? overwrite;

    public bool? Overwrite { get => overwrite; set => SetProperty(ref overwrite, value); }

    public string Title => "Add Files to the archive";
}
