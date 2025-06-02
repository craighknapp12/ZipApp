using System.Windows;
using System.Windows.Interop;
using Zipper.ViewModels;
using Zipper.Views;
using ZippyLibrary;
using ZippyLibrary.Interfaces;
using ZippyLibrary.ViewModels;

namespace Zipper;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

    private IZip _zip = null!;

    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
        var viewDialog = new ViewDialog(this);

        _zip = new Zip(viewDialog);
        _zip.Load(Environment.GetCommandLineArgs().Skip(1).ToArray());

        var zipMenuViewModel = new ZipMenuViewModel (_zip, () => 
        {
            this.Close(); 
        });
        var zipViewModel = new ZipViewModel(_zip);
        menu.SetViewModel(zipMenuViewModel);
        status.SetViewModel(zipViewModel);
    }

    protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
    {
        _zip.ExecuteSave();

        base.OnClosing(e);
    }


}