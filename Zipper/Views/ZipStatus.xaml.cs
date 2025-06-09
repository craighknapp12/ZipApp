using System.Windows.Controls;
using ZippyLibrary.ViewModels;

namespace Zipper.Views;
/// <summary>
/// Interaction logic for ZipStatus.xaml
/// </summary>
public partial class ZipStatus : UserControl
{
    public ZipStatus()
    {
        InitializeComponent();
    }

    internal void SetViewModel(ZipViewModel zipViewModel)
    {
        this.DataContext = zipViewModel;
    }
}
