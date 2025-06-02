using System.Windows.Controls;
using ZippyLibrary.ViewModels;

namespace Zipper.Views;
/// <summary>
/// Interaction logic for ZipMenu.xaml
/// </summary>
public partial class ZipMenu : UserControl
{
    public ZipMenu()
    {
        InitializeComponent();
    }

    internal void SetViewModel(ZipMenuViewModel zipMenuViewModel)
    {
        DataContext = zipMenuViewModel;
    }


}
