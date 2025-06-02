using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using ZippyLibrary.Interfaces;

namespace ZippyLibrary.ViewModels;


public delegate void CloseMethod();

public class ZipMenuViewModel(IZip zip, CloseMethod closeMethod) 
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
        closeMethod.Invoke();
    }

    private void ExecuteAbout()
    {
        zip.ShowAbout();
    }
}
