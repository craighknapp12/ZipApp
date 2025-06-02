using ZippyLibrary;
using ZippyLibrary.Interfaces;
using ZippyLibrary.ViewModels;
namespace TestZippy;

public class ZipMenuViewModelTests
{
    [Fact]
    public void TestAbout()
    {
        var closeCount = 0;
        var testDialog = new TestDialog();
        IZip zip = new Zip(testDialog);

        var zipMenuViewModel = new ZipMenuViewModel(zip, () => { closeCount++; });
        zipMenuViewModel.AboutCommand.Execute(null);

        Assert.Equal(1, testDialog.ShowAboutCount);
    }

    [Fact]
    public void TestCloseWithCleanState()
    {
        var closeCount = 0;

        var testDialog = new TestDialog();
        IZip zip = new Zip(testDialog);

        var zipMenuViewModel = new ZipMenuViewModel(zip, () => { closeCount++; });
        zipMenuViewModel.ExitCommand.Execute(null);

        Assert.Equal(1, testDialog.GetSaveFileCount);
        Assert.Equal(1, testDialog.ShouldSaveCount);
        Assert.Equal(1, closeCount);

    }
    [Fact]
    public void TestCloseWithDirtyState()
    {
        var closeCount = 0;

        var testDialog = new TestDialog();
        IZip zip = new Zip(testDialog);

        var zipMenuViewModel = new ZipMenuViewModel(zip, () =>
        {
            closeCount++;
        });
        zipMenuViewModel.ExitCommand.Execute(null);

        Assert.Equal(1, testDialog.GetSaveFileCount);
        Assert.Equal(1, testDialog.ShouldSaveCount);
        Assert.Equal(1, closeCount);
    }
    [Fact]
    public void TestCloseWithDirtyStateAndCancel()
    {
        var closeCount = 0;
        var testDialog = new TestDialogWithCancel();
        IZip zip = new Zip(testDialog);

        var zipMenuViewModel = new ZipMenuViewModel(zip, () => { closeCount++; });
        zipMenuViewModel.ExitCommand.Execute(null);

        Assert.Equal(1, testDialog.GetSaveFileCount);
        Assert.Equal(0, testDialog.GetOpenFileCount);
        Assert.Equal(0, testDialog.ShowAboutCount);
        Assert.Equal(1, testDialog.ShouldSaveCount);
        Assert.Equal(0, testDialog.ShowErrorCount);
        Assert.Equal(1, closeCount);
    }
    [Fact]
    public void TestNewWithCleanState()
    {
        var closeCount = 0;
        var testDialog = new TestDialog();
        IZip zip = new Zip(testDialog);

        var zipMenuViewModel = new ZipMenuViewModel(zip, () => { closeCount++; });
        Assert.NotNull(zipMenuViewModel);
        Assert.Equal(0, testDialog.GetSaveFileCount);
        Assert.Equal(0, testDialog.GetOpenFileCount);
        Assert.Equal(0, testDialog.ShowAboutCount);
        Assert.Equal(0, testDialog.ShouldSaveCount);
        Assert.Equal(0, closeCount);
    }
    [Fact]
    public void TestNewWithDirtyState()
    {
        var closeCount = 0;
        var testDialog = new TestDialog();
        IZip zip = new Zip(testDialog);

        var zipMenuViewModel = new ZipMenuViewModel(zip, () => { closeCount++; });
        zipMenuViewModel.NewCommand.Execute(null);

        Assert.NotNull(zipMenuViewModel);

        Assert.Equal(1, testDialog.GetSaveFileCount);
        Assert.Equal(0, testDialog.GetOpenFileCount);
        Assert.Equal(0, testDialog.ShowAboutCount);
        Assert.Equal(1, testDialog.ShouldSaveCount);
        Assert.Equal(0, testDialog.ShowErrorCount);
        Assert.Equal(0, closeCount);
    }
    [Fact]
    public void TestOpenWithCleanState()
    {
        var closeCount = 0;
        var testDialog = new TestDialog();
        IZip zip = new Zip(testDialog);

        var zipMenuViewModel = new ZipMenuViewModel(zip, () => { closeCount++; });
        zipMenuViewModel.OpenCommand.Execute(null);
        Assert.NotNull(zipMenuViewModel);

        Assert.Equal(1, testDialog.GetSaveFileCount);
        Assert.Equal(1, testDialog.GetOpenFileCount);
    }
    [Fact]
    public void TestOpenWithDirtyState()
    {
        var closeCount = 0;
        var testDialog = new TestDialog();
        IZip zip = new Zip(testDialog);

        var zipMenuViewModel = new ZipMenuViewModel(zip, () => { closeCount++; });
        zipMenuViewModel.OpenCommand.Execute(null);
        Assert.NotNull(zipMenuViewModel);

        Assert.Equal(1, testDialog.GetSaveFileCount);
        Assert.Equal(1, testDialog.GetOpenFileCount);
        Assert.Equal(1, testDialog.ShouldSaveCount);
    }
    [Fact]
    public void TestOpenWithDirtyStateCancel()
    {
        var closeCount = 0;
        var testDialog = new TestDialogWithCancel();
        IZip zip = new Zip(testDialog);

        var zipMenuViewModel = new ZipMenuViewModel(zip, () => { closeCount++; });
        // Set zip change
        zipMenuViewModel.OpenCommand.Execute(null);
        Assert.NotNull(zipMenuViewModel);

        Assert.Equal(1, testDialog.GetSaveFileCount);
        Assert.Equal(1, testDialog.ShouldSaveCount);
        Assert.Equal(0, closeCount);
    }
    [Fact]
    public void TestSaveWithCleanState()
    {
        var closeCount = 0;
        var testDialog = new TestDialog();
        IZip zip = new Zip(testDialog);

        var zipMenuViewModel = new ZipMenuViewModel(zip, () => { closeCount++; });
        // Set zip change
        zipMenuViewModel.SaveCommand.Execute(null);
        Assert.NotNull(zipMenuViewModel);

        Assert.Equal(1, testDialog.GetSaveFileCount);
        //Assert.Equal(0, testDialog.ShouldSaveCount);
        //Assert.Equal(0, testDialog.ShowErrorCount);
        //Assert.Equal(0, closeCount);
    }
    [Fact]
    public void TestSaveWithDirtyState()
    {
        var closeCount = 0;
        var testDialog = new TestDialog();
        IZip zip = new Zip(testDialog);

        var zipMenuViewModel = new ZipMenuViewModel(zip, () => { closeCount++; });
        // Set zip change
        zipMenuViewModel.SaveCommand.Execute(null);
        Assert.NotNull(zipMenuViewModel);
        Assert.Equal(1, testDialog.GetSaveFileCount);
        //Assert.Equal(0, testDialog.ShowAboutCount);
        //Assert.Equal(0, testDialog.ShouldSaveCount);
        //Assert.Equal(0, testDialog.ShowErrorCount);
        //Assert.Equal(0, closeCount);
    }
    [Fact]
    public void TestSaveAsWithCleanState()
    {
        var closeCount = 0;
        var testDialog = new TestDialog();
        IZip zip = new Zip(testDialog);

        var zipMenuViewModel = new ZipMenuViewModel(zip, () => { closeCount++; });
        // Set zip change
        zipMenuViewModel.SaveAsCommand.Execute(null);
        Assert.NotNull(zipMenuViewModel);
        Assert.Equal(1, testDialog.GetSaveFileCount);
        //Assert.Equal(0, testDialog.ShouldSaveCount);
        //Assert.Equal(0, testDialog.ShowErrorCount);
        //Assert.Equal(0, closeCount);
    }
    [Fact]
    public void TestSaveAsWithDirtyState()
    {
        var closeCount = 0;
        var testDialog = new TestDialog();
        IZip zip = new Zip(testDialog);

        var zipMenuViewModel = new ZipMenuViewModel(zip, () => { closeCount++; });
        // Set zip change
        zipMenuViewModel.SaveAsCommand.Execute(null);
        Assert.Equal(1, testDialog.GetSaveFileCount);
        //Assert.Equal(0, testDialog.ShouldSaveCount);
        //Assert.Equal(0, testDialog.ShowErrorCount);
        //Assert.Equal(0, closeCount);
    }
    [Fact]
    public void TestSaveAsWithDirtyStateCancel()
    {
        var closeCount = 0;
        var testDialog = new TestDialog();
        IZip zip = new Zip(testDialog);

        var zipMenuViewModel = new ZipMenuViewModel(zip, () => { closeCount++; });
        // Set zip change
        zipMenuViewModel.SaveCommand.Execute(null);
        Assert.Equal(1, testDialog.GetSaveFileCount);
        //Assert.Equal(0, testDialog.ShouldSaveCount);
        //Assert.Equal(0, testDialog.ShowErrorCount);
        //Assert.Equal(0, closeCount);
    }
    //[Fact]
    //public void TestAddFileToTopLevel()
    //{
    //    var closeCount = 0;
    //    var testDialog = new TestDialog();
    //    var zipViewModel = new ZipViewModel(testDialog, () => { closeCount++; });
    //    Assert.False(true);
    //    Assert.Equal(0, testDialog.GetSaveFileCount);
    //    Assert.Equal(0, testDialog.GetOpenFileCount);
    //    Assert.Equal(0, testDialog.ShowAboutCount);
    //    Assert.Equal(0, testDialog.ShouldSaveCount);
    //    Assert.Equal(0, testDialog.ShowErrorCount);
    //    Assert.Equal(1, closeCount);
    //}
    //[Fact]
    //public void TestAddFileToLowerLevel()
    //{
    //    var closeCount = 0;
    //    var testDialog = new TestDialog();
    //    var zipViewModel = new ZipViewModel(testDialog, () => { closeCount++; });
    //    Assert.False(true);
    //    Assert.Equal(0, testDialog.GetSaveFileCount);
    //    Assert.Equal(0, testDialog.GetOpenFileCount);
    //    Assert.Equal(0, testDialog.ShowAboutCount);
    //    Assert.Equal(0, testDialog.ShouldSaveCount);
    //    Assert.Equal(0, testDialog.ShowErrorCount);
    //    Assert.Equal(1, closeCount);
    //}
    //[Fact]
    //public void TestDeleteFileTopLevel()
    //{
    //    var closeCount = 0;
    //    var testDialog = new TestDialog();
    //    var zipViewModel = new ZipViewModel(testDialog, () => { closeCount++; });
    //    Assert.False(true);
    //    Assert.Equal(0, testDialog.GetSaveFileCount);
    //    Assert.Equal(0, testDialog.GetOpenFileCount);
    //    Assert.Equal(0, testDialog.ShowAboutCount);
    //    Assert.Equal(0, testDialog.ShouldSaveCount);
    //    Assert.Equal(0, testDialog.ShowErrorCount);
    //    Assert.Equal(1, closeCount);
    //}
    //[Fact]
    //public void TestDeleteFileLowerLevel()
    //{
    //    var closeCount = 0;
    //    var testDialog = new TestDialog();
    //    var zipViewModel = new ZipViewModel(testDialog, () => { closeCount++; });
    //    Assert.False(true);
    //    Assert.Equal(0, testDialog.GetSaveFileCount);
    //    Assert.Equal(0, testDialog.GetOpenFileCount);
    //    Assert.Equal(0, testDialog.ShowAboutCount);
    //    Assert.Equal(0, testDialog.ShouldSaveCount);
    //    Assert.Equal(0, testDialog.ShowErrorCount);
    //    Assert.Equal(1, closeCount);
    //}

    //[Fact]
    //public void TestAddFileExternallyToTopLevel()
    //{
    //    var closeCount = 0;
    //    var testDialog = new TestDialog();
    //    var zipViewModel = new ZipViewModel(testDialog, () => { closeCount++; });
    //    Assert.False(true);
    //    Assert.Equal(0, testDialog.GetSaveFileCount);
    //    Assert.Equal(0, testDialog.GetOpenFileCount);
    //    Assert.Equal(0, testDialog.ShowAboutCount);
    //    Assert.Equal(0, testDialog.ShouldSaveCount);
    //    Assert.Equal(0, testDialog.ShowErrorCount);
    //    Assert.Equal(1, closeCount);
    //}
    //[Fact]
    //public void TestAddFileExternallyToLowerLevel()
    //{
    //    var closeCount = 0;
    //    var testDialog = new TestDialog();
    //    var zipViewModel = new ZipViewModel(testDialog, () => { closeCount++; });
    //    Assert.False(true);
    //    Assert.Equal(0, testDialog.GetSaveFileCount);
    //    Assert.Equal(0, testDialog.GetOpenFileCount);
    //    Assert.Equal(0, testDialog.ShowAboutCount);
    //    Assert.Equal(0, testDialog.ShouldSaveCount);
    //    Assert.Equal(0, testDialog.ShowErrorCount);
    //    Assert.Equal(1, closeCount);
    //}
    //[Fact]
    //public void TestSortingByName()
    //{
    //    var closeCount = 0;
    //    var testDialog = new TestDialog();
    //    var zipViewModel = new ZipViewModel(testDialog, () => { closeCount++; });
    //    Assert.False(true);
    //    Assert.Equal(0, testDialog.GetSaveFileCount);
    //    Assert.Equal(0, testDialog.GetOpenFileCount);
    //    Assert.Equal(0, testDialog.ShowAboutCount);
    //    Assert.Equal(0, testDialog.ShouldSaveCount);
    //    Assert.Equal(0, testDialog.ShowErrorCount);
    //    Assert.Equal(1, closeCount);
    //}
    //[Fact]
    //public void TestSortingByExtension()
    //{
    //    var closeCount = 0;
    //    var testDialog = new TestDialog();
    //    IZip zip = new Zip(testDialog);

    //    var zipMenuViewModel = new ZipMenuViewModel(zip, () => { closeCount++; });
    //    Assert.False(true);
    //    Assert.Equal(0, testDialog.GetSaveFileCount);
    //    Assert.Equal(0, testDialog.GetOpenFileCount);
    //    Assert.Equal(0, testDialog.ShowAboutCount);
    //    Assert.Equal(0, testDialog.ShouldSaveCount);
    //    Assert.Equal(0, testDialog.ShowErrorCount);
    //    Assert.Equal(1, closeCount);
    //}
    //[Fact]
    //public void TestSortingBySize()
    //{
    //    var closeCount = 0;
    //    var testDialog = new TestDialog();
    //    IZip zip = new Zip(testDialog);

    //    var zipMenuViewModel = new ZipMenuViewModel(zip, () => { closeCount++; });
    //    Assert.False(true);
    //    Assert.Equal(0, testDialog.GetSaveFileCount);
    //    Assert.Equal(0, testDialog.GetOpenFileCount);
    //    Assert.Equal(0, testDialog.ShowAboutCount);
    //    Assert.Equal(0, testDialog.ShouldSaveCount);
    //    Assert.Equal(0, testDialog.ShowErrorCount);
    //    Assert.Equal(0, closeCount);
    //}
    //[Fact]
    //public void TestViewList()
    //{
    //    var closeCount = 0;
    //    var testDialog = new TestDialog();
    //    IZip zip = new Zip(testDialog);

    //    var zipMenuViewModel = new ZipMenuViewModel(zip, () => { closeCount++; });
    //    Assert.False(true);
    //    Assert.Equal(0, testDialog.GetSaveFileCount);
    //    Assert.Equal(0, testDialog.GetOpenFileCount);
    //    Assert.Equal(0, testDialog.ShowAboutCount);
    //    Assert.Equal(0, testDialog.ShouldSaveCount);
    //    Assert.Equal(0, testDialog.ShowErrorCount);
    //    Assert.Equal(0, closeCount);
    //}
    //[Fact]
    //public void TestViewLargeIcon()
    //{
    //    var closeCount = 0;
    //    var testDialog = new TestDialog();
    //    IZip zip = new Zip(testDialog);

    //    var zipMenuViewModel = new ZipMenuViewModel(zip, () => { closeCount++; });
    //    Assert.False(true);
    //    Assert.Equal(0, testDialog.GetSaveFileCount);
    //    Assert.Equal(0, testDialog.GetOpenFileCount);
    //    Assert.Equal(0, testDialog.ShowAboutCount);
    //    Assert.Equal(0, testDialog.ShouldSaveCount);
    //    Assert.Equal(0, testDialog.ShowErrorCount);
    //    Assert.Equal(0, closeCount);
    //}
    //[Fact]
    //public void TestViewSmallIcon()
    //{
    //    var closeCount = 0;
    //    var testDialog = new TestDialog();
    //    IZip zip = new Zip(testDialog);

    //    var zipMenuViewModel = new ZipMenuViewModel(zip, () => { closeCount++; });
    //    Assert.False(true);
    //    Assert.Equal(0, testDialog.GetSaveFileCount);
    //    Assert.Equal(0, testDialog.GetOpenFileCount);
    //    Assert.Equal(0, testDialog.ShowAboutCount);
    //    Assert.Equal(0, testDialog.ShouldSaveCount);
    //    Assert.Equal(0, testDialog.ShowErrorCount);
    //    Assert.Equal(0, closeCount);
    //}
}
