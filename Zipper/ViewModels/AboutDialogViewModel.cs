using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using ZippyLibrary.Interfaces;
using ZippyLibrary.ViewModels;

namespace Zipper.ViewModels;
public class AboutDialogViewModel(CloseMethod<bool> close) : ObservableObject
{
    public string Title { get; } = "About Zipper";

    public ICommand OkCommand =>  new RelayCommand(Ok);

    private void Ok()
    {
        close.Invoke (true);
    }
}
