using System.Windows.Controls;
using ZippyLibrary.ViewModels;

namespace Zipper.Views;
public partial class ZipMenu : UserControl
{
    public ZipMenu()
    {
        InitializeComponent();
    }

    internal void SetViewModel(ZipViewModel zipViewModel)
    {
        this.DataContext = zipViewModel;
    }


}
