using System.Windows;
using Microsoft.Win32;
using ZippyLibrary.Interfaces;

namespace Zipper.Views;
internal class ViewDialog(Window window) : IViewDialog
{
    private const string DATE_FORMAT = "yyyy_MM_dd_HH_mm_ss";
    public string GetOpenFile()
    {
        var dialog = new OpenFileDialog();
        dialog.DefaultExt = ".zip"; 
        dialog.Filter = "zip documents (.zip)|*.zip"; 

        if (dialog.ShowDialog() == true)
        {
            return dialog.FileName;
        }

        return string.Empty;
    }

    public string GetSaveFile()
    {
        var dialog = new SaveFileDialog();
        dialog.FileName = "zip_" + DateTime.Now.ToString(DATE_FORMAT);
        dialog.DefaultExt = ".zip"; 
        dialog.Filter = "Zip documents (.zip)|*.zip"; 

        if (dialog.ShowDialog() == true)
        {
            return dialog.FileName;
        }

        return string.Empty;
    }

    public bool ShouldSave()
    {
        var result = MessageBox.Show("Do you want to save the contents of the zip file?", "Save Zip", MessageBoxButton.YesNo, MessageBoxImage.Question);
        return result == MessageBoxResult.Yes;
    }

    public void ShowAdd()
    {
        var addDialog = new AddDialog();
        addDialog.Owner = window;
        if (addDialog.ShowDialog() == true)
        {

        }
    }

    public void ShowAbout()
    {
        var aboutDialog = new AboutDialog();
        aboutDialog.Owner = window;
        aboutDialog.ShowDialog();
    }

    public void ShowError(string message)
    {
        MessageBox.Show(message, "Zippy Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
