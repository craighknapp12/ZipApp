using CommunityToolkit.Mvvm.ComponentModel;

namespace Zipper.ViewModels;

public class MainWindowViewModel : ObservableObject
{
    public string Title { get; } = "Zipper";
}
