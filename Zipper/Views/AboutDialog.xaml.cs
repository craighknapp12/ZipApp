using System.Windows;
using Zipper.ViewModels;

namespace Zipper.Views;
/// <summary>
/// Interaction logic for AboutDialog.xaml
/// </summary>
public partial class AboutDialog : Window
{
    public AboutDialog()
    {
        InitializeComponent();
        DataContext = new AboutDialogViewModel((in bool result) => CloseIt(result));
    }

    public void CloseIt (bool result)
    {
        this.DialogResult = result; 
        this.Close();
    }
}
