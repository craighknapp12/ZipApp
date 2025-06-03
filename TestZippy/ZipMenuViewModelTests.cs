using System.IO.Compression;
using ZippyLibrary;
using ZippyLibrary.Interfaces;
using ZippyLibrary.Models;
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

        var zipMenuViewModel = new ZipMenuViewModel(zip, (in int result) => { closeCount++; });
        zipMenuViewModel.AboutCommand.Execute(null);

        Assert.Equal(1, testDialog.ShowAboutCount);
    }

    [Fact]
    public void TestCloseWithCleanState()
    {
        var closeCount = 0;

        var testDialog = new TestDialog();
        IZip zip = new Zip(testDialog);

        var zipMenuViewModel = new ZipMenuViewModel(zip, (in int result) => { closeCount++; });
        zipMenuViewModel.ExitCommand.Execute(null);

        Assert.Equal(0, testDialog.GetSaveFileCount);
        Assert.Equal(0, testDialog.ShouldSaveCount);
        Assert.Equal(1, closeCount);
    }

    [Fact]
    public void TestCloseWithDirtyState()
    {
        var closeCount = 0;

        var testDialog = new TestDialog();
        IZip zip = new Zip(testDialog);

        var zipMenuViewModel = new ZipMenuViewModel(zip, (in int result) =>
        {
            closeCount++;
        });
        zip.Add(new AddZipContent
        {
            Filename = "*.cs",
            Override = true,
            Compression = CompressionLevel.SmallestSize,
            EntryLevel = 0,
            Directory = string.Empty
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

        var zipMenuViewModel = new ZipMenuViewModel(zip, (in int result) => { closeCount++; });
        zipMenuViewModel.ExitCommand.Execute(null);
        zip.Add(new AddZipContent
        {
            Filename = "*.cs",
            Override = true,
            Compression = CompressionLevel.SmallestSize,
            EntryLevel = 0,
            Directory = string.Empty
        });


        Assert.Equal(0, testDialog.GetSaveFileCount);
        Assert.Equal(0, testDialog.ShouldSaveCount);
        Assert.Equal(0, testDialog.ShowErrorCount);
        Assert.Equal(1, closeCount);
    }


    [Fact]
    public void TestNewWithCleanState()
    {
        var closeCount = 0;
        var testDialog = new TestDialog();
        IZip zip = new Zip(testDialog);

        var zipMenuViewModel = new ZipMenuViewModel(zip, (in int result) => { closeCount++; });
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

        var zipMenuViewModel = new ZipMenuViewModel(zip, (in int result) => { closeCount++; });
        zipMenuViewModel.NewCommand.Execute(null);
        zip.Add(new AddZipContent
        {
            Filename = "*.cs",
            Override = true,
            Compression = CompressionLevel.SmallestSize,
            EntryLevel = 0,
            Directory = string.Empty
        });


        Assert.NotNull(zipMenuViewModel);
        Assert.True(zip.IsDirty);

        Assert.Equal(0, testDialog.GetSaveFileCount);
        Assert.Equal(0, testDialog.GetOpenFileCount);
        Assert.Equal(0, testDialog.ShowAboutCount);
        Assert.Equal(0, testDialog.ShouldSaveCount);
        Assert.Equal(0, testDialog.ShowErrorCount);
        Assert.Equal(0, closeCount);
    }

    [Fact]
    public void TestOpenWithCleanState()
    {
        var closeCount = 0;
        var testDialog = new TestDialog();
        IZip zip = new Zip(testDialog);

        var zipMenuViewModel = new ZipMenuViewModel(zip, (in int result) => { closeCount++; });
        zipMenuViewModel.OpenCommand.Execute(null);
        Assert.NotNull(zipMenuViewModel);

        Assert.False(zip.IsDirty);
        Assert.Equal(1, testDialog.GetOpenFileCount);
    }

    [Fact]
    public void TestOpenWithDirtyState()
    {
        var closeCount = 0;
        var testDialog = new TestDialog();
        IZip zip = new Zip(testDialog);

        var zipMenuViewModel = new ZipMenuViewModel(zip, (in int result) => { closeCount++; });
        zip.Add(new AddZipContent
        {
            Filename = "*.cs",
            Override = true,
            Compression = CompressionLevel.SmallestSize,
            EntryLevel = 0,
            Directory = string.Empty
        });
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

        var zipMenuViewModel = new ZipMenuViewModel(zip, (in int result) => { closeCount++; });
        zip.Add(new AddZipContent
        {
            Filename = "*.cs",
            Override = true,
            Compression = CompressionLevel.SmallestSize,
            EntryLevel = 0,
            Directory = string.Empty
        });
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

        var zipMenuViewModel = new ZipMenuViewModel(zip, (in int result) => { closeCount++; });
        // Set zip change
        zipMenuViewModel.SaveCommand.Execute(null);
        Assert.NotNull(zipMenuViewModel);

        Assert.Equal(0, testDialog.GetSaveFileCount);
        Assert.Equal(0, testDialog.ShouldSaveCount);
        Assert.Equal(0, testDialog.ShowErrorCount);
        Assert.Equal(0, closeCount);
    }

    [Fact]
    public void TestSaveWithDirtyState()
    {
        var closeCount = 0;
        var testDialog = new TestDialog();
        IZip zip = new Zip(testDialog);

        var zipMenuViewModel = new ZipMenuViewModel(zip, (in int result) => { closeCount++; });
        zip.Add(new AddZipContent
        {
            Filename = "*.cs",
            Override = true,
            Compression = CompressionLevel.SmallestSize,
            EntryLevel = 0,
            Directory = string.Empty
        });
        // Set zip change
        Assert.True(zip.IsDirty);
        zipMenuViewModel.SaveCommand.Execute(null);
        Assert.NotNull(zipMenuViewModel);
        Assert.Equal(1, testDialog.GetSaveFileCount);
        Assert.Equal(0, testDialog.ShowAboutCount);
        Assert.Equal(0, testDialog.ShouldSaveCount);
        Assert.Equal(0, testDialog.ShowErrorCount);
        Assert.Equal(0, closeCount);
    }


    [Fact]
    public void TestSaveAsWithCleanState()
    {
        var closeCount = 0;
        var testDialog = new TestDialog();
        IZip zip = new Zip(testDialog);

        var zipMenuViewModel = new ZipMenuViewModel(zip, (in int result) => { closeCount++; });
        Assert.False(zip.IsDirty);
        zipMenuViewModel.SaveAsCommand.Execute(null);
        Assert.NotNull(zipMenuViewModel);
        Assert.Equal(1, testDialog.GetSaveFileCount);
        Assert.Equal(0, testDialog.ShouldSaveCount);
        Assert.Equal(0, testDialog.ShowErrorCount);
        Assert.Equal(0, closeCount);
    }

    [Fact]
    public void TestSaveAsWithDirtyState()
    {
        var closeCount = 0;
        var testDialog = new TestDialog();
        IZip zip = new Zip(testDialog);

        var zipMenuViewModel = new ZipMenuViewModel(zip, (in int result) => { closeCount++; });
        zip.Add(new AddZipContent
        {
            Filename = "*.cs",
            Override = true,
            Compression = CompressionLevel.SmallestSize,
            EntryLevel = 0,
            Directory = string.Empty
        });
        Assert.True(zip.IsDirty);
        zipMenuViewModel.SaveAsCommand.Execute(null);
        Assert.Equal(1, testDialog.GetSaveFileCount);
        Assert.Equal(0, testDialog.ShouldSaveCount);
        Assert.Equal(0, testDialog.ShowErrorCount);
        Assert.Equal(0, closeCount);
    }

    [Fact]
    public void TestSaveAsWithDirtyStateCancel()
    {
        var closeCount = 0;
        var testDialog = new TestDialog();
        IZip zip = new Zip(testDialog);

        var zipMenuViewModel = new ZipMenuViewModel(zip, (in int result) => { closeCount++; });
        zip.Add(new AddZipContent
        {
            Filename = "*.cs",
            Override = true,
            Compression = CompressionLevel.SmallestSize,
            EntryLevel = 0,
            Directory = string.Empty
        });
        Assert.True(zip.IsDirty);
        zipMenuViewModel.SaveCommand.Execute(null);
        Assert.False(zip.IsDirty);
        Assert.Equal(1, testDialog.GetSaveFileCount);
        Assert.Equal(0, testDialog.ShouldSaveCount);
        Assert.Equal(0, testDialog.ShowErrorCount);
        Assert.Equal(0, closeCount);
    }

    [Fact]
    public void TestRemoveFileTopLevel()
    {
        var closeCount = 0;
        var testDialog = new TestDialog();
        IZip zip = new Zip(testDialog);
        var zipMenuViewModel = new ZipMenuViewModel(zip, (in int result) => { closeCount++; });
        zip.Add(new AddZipContent
        {
            Filename = "*.cs",
            Override = true,
            Compression = CompressionLevel.SmallestSize,
            EntryLevel = 0,
            Directory = string.Empty
        });
        Assert.True(zip.IsDirty);
        zip.Remove(new RemoveZipContent (new List<string> { "*.cs" }));
        Assert.True(zip.IsDirty);
        zipMenuViewModel.SaveCommand.Execute(null);

        Assert.Equal(1, testDialog.GetSaveFileCount);
        Assert.Equal(0, testDialog.GetOpenFileCount);
        Assert.Equal(0, testDialog.ShowAboutCount);
        Assert.Equal(0, testDialog.ShouldSaveCount);
        Assert.Equal(0, testDialog.ShowErrorCount);
        Assert.Equal(0, closeCount);
    }

}
