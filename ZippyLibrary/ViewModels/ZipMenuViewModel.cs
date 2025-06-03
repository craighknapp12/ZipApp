using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using ZippyLibrary.Interfaces;

namespace ZippyLibrary.ViewModels;

public class ZipMenuViewModel(IZip zip, CloseMethod<int> closeMethod) 
{

    public ICommand NewCommand => new RelayCommand(ExecuteNew);
    public ICommand OpenCommand => new RelayCommand(ExecuteOpen);
    public ICommand SaveCommand => new RelayCommand(ExecuteSave);
    public ICommand SaveAsCommand => new RelayCommand(ExecuteSaveAs);
    public ICommand ExitCommand => new RelayCommand(ExecuteExit);
    public ICommand AboutCommand => new RelayCommand(ExecuteAbout);

    private void ExecuteNew()
    {
        zip.ExecuteNew();
    }

    private void ExecuteOpen()
    {
        zip.ExecuteOpen();
    }

    private void ExecuteSave()
    {
        zip.ExecuteSave(true);
    }

    private void ExecuteSaveAs()
    {
        zip.ExecuteSaveAs();
    }

    private void ExecuteExit()
    {
        zip.ExecuteSave();
        closeMethod.Invoke(0);
    }

    private void ExecuteAbout()
    {
        zip.ShowAbout();
    }
}
