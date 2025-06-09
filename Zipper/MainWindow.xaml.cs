using System.Windows;
using Zipper.ViewModels;
using Zipper.Views;
using ZippyLibrary.ViewModels;

namespace Zipper;
public partial  class MainWindow : Window
{

    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
        var viewDialog = new ViewDialog(this);
        var zipViewModel = new ZipViewModel(viewDialog, (in int result) =>
        {
            DialogResult = result == 0;
            this.Close();
        });

        menu.SetViewModel(zipViewModel);
        status.SetViewModel(zipViewModel);
    }
}