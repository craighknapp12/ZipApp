using System.IO.Compression;
using ZippyLibrary.Models;
using ZippyLibrary.ViewModels;
namespace TestZippy;

public class ZipViewModelTests
{
    [Fact]
    public void TestAbout()
    {
        var closeCount = 0;
        var testDialog = new TestDialog();

        var zipViewModel = new ZipViewModel(testDialog, (in int result) => { closeCount++; });
        zipViewModel.AboutCommand.Execute(null);

        Assert.Equal(1, testDialog.ShowAboutCount);
    }

    [Fact]
    public void TestCloseWithCleanState()
    {
        var closeCount = 0;

        var testDialog = new TestDialog();

        var zipViewModel = new ZipViewModel(testDialog, (in int result) => { closeCount++; });
        zipViewModel.ExitCommand.Execute(null);

        Assert.Equal(0, testDialog.GetSaveFileCount);
        Assert.Equal(0, testDialog.ShouldSaveCount);
        Assert.Equal(1, closeCount);
    }

    [Fact]
    public void TestCloseWithDirtyState()
    {
        var closeCount = 0;

        var testDialog = new TestDialog();

        var zipViewModel = new ZipViewModel(testDialog, (in int result) =>
        {
            closeCount++;
        });

        zipViewModel.Add(new AddZipContent
        {
            Filename = "*.dll",
            Override = true,
            Compression = CompressionLevel.SmallestSize,
            EntryLevel = 0,
            Directory = string.Empty
        });

        zipViewModel.ExitCommand.Execute(null);

        Assert.Equal(1, testDialog.GetSaveFileCount);
        Assert.Equal(1, testDialog.ShouldSaveCount);
        Assert.Equal(1, closeCount);
    }


    [Fact]
    public void TestCloseWithDirtyStateAndCancel()
    {
        var closeCount = 0;
        var testDialog = new TestDialogWithCancel();

        var zipViewModel = new ZipViewModel(testDialog, (in int result) => { closeCount++; });
        zipViewModel.ExitCommand.Execute(null);
        zipViewModel.Add(new AddZipContent
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

        var zipViewModel = new ZipViewModel(testDialog, (in int result) => { closeCount++; });
        Assert.NotNull(zipViewModel);
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

        var zipViewModel = new ZipViewModel(testDialog, (in int result) => { closeCount++; });
        zipViewModel.NewCommand.Execute(null);
        zipViewModel.Add(new AddZipContent
        {
            Filename = "*.dll",
            Override = true,
            Compression = CompressionLevel.SmallestSize,
            EntryLevel = 0,
            Directory = string.Empty
        });


        Assert.True(zipViewModel.IsDirty);

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

        var zipViewModel = new ZipViewModel(testDialog, (in int result) => { closeCount++; });
        zipViewModel.OpenCommand.Execute(null);
        Assert.NotNull(zipViewModel);

        Assert.False(zipViewModel.IsDirty);
        Assert.Equal(1, testDialog.GetOpenFileCount);
    }

    [Fact]
    public void TestOpenWithDirtyState()
    {
        var closeCount = 0;
        var testDialog = new TestDialog();

        var zipViewModel = new ZipViewModel(testDialog, (in int result) => { closeCount++; });
        zipViewModel.Add(new AddZipContent
        {
            Filename = "*.dll",
            Override = true,
            Compression = CompressionLevel.SmallestSize,
            EntryLevel = 0,
            Directory = string.Empty
        });
        zipViewModel.OpenCommand.Execute(null);
        Assert.NotNull(zipViewModel);

        Assert.Equal(1, testDialog.GetSaveFileCount);
        Assert.Equal(1, testDialog.GetOpenFileCount);
        Assert.Equal(1, testDialog.ShouldSaveCount);
    }

    [Fact]
    public void TestOpenWithDirtyStateCancel()
    {
        var closeCount = 0;
        var testDialog = new TestDialogWithCancel();

        var zipViewModel = new ZipViewModel(testDialog, (in int result) => { closeCount++; });
        zipViewModel.Add(new AddZipContent
        {
            Filename = "*.dll",
            Override = true,
            Compression = CompressionLevel.SmallestSize,
            EntryLevel = 0,
            Directory = string.Empty
        });
        zipViewModel.OpenCommand.Execute(null);
        Assert.NotNull(zipViewModel);

        Assert.Equal(1, testDialog.GetSaveFileCount);
        Assert.Equal(1, testDialog.ShouldSaveCount);
        Assert.Equal(0, closeCount);
    }


    [Fact]
    public void TestSaveWithCleanState()
    {
        var closeCount = 0;
        var testDialog = new TestDialog();

        var zipViewModel = new ZipViewModel(testDialog, (in int result) => { closeCount++; });
        // Set zip change
        zipViewModel.SaveCommand.Execute(null);
        Assert.NotNull(zipViewModel);

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

        var zipViewModel = new ZipViewModel(testDialog, (in int result) => { closeCount++; });
        zipViewModel.Add(new AddZipContent
        {
            Filename = "*.dll",
            Override = true,
            Compression = CompressionLevel.SmallestSize,
            EntryLevel = 0,
            Directory = string.Empty
        });
        // Set zip change
        Assert.True(zipViewModel.IsDirty);
        zipViewModel.SaveCommand.Execute(null);
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

        var zipViewModel = new ZipViewModel(testDialog, (in int result) => { closeCount++; });
        Assert.False(zipViewModel.IsDirty);
        zipViewModel.SaveAsCommand.Execute(null);
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

        var zipViewModel = new ZipViewModel(testDialog, (in int result) => { closeCount++; });
        zipViewModel.Add(new AddZipContent
        {
            Filename = "*.dll",
            Override = true,
            Compression = CompressionLevel.SmallestSize,
            EntryLevel = 0,
            Directory = string.Empty
        });
        Assert.True(zipViewModel.IsDirty);
        zipViewModel.SaveAsCommand.Execute(null);
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

        var zipViewModel = new ZipViewModel(testDialog, (in int result) => { closeCount++; });
        zipViewModel.Add(new AddZipContent
        {
            Filename = "*.dll",
            Override = true,
            Compression = CompressionLevel.SmallestSize,
            EntryLevel = 0,
            Directory = string.Empty
        });
        Assert.True(zipViewModel.IsDirty);
        zipViewModel.SaveCommand.Execute(null);
        Assert.False(zipViewModel.IsDirty);
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
        var zipViewModel = new ZipViewModel(testDialog, (in int result) => { closeCount++; });
        zipViewModel.Add(new AddZipContent
        {
            Filename = "*.dll",
            Override = true,
            Compression = CompressionLevel.SmallestSize,
            EntryLevel = 0,
            Directory = string.Empty
        });
        Assert.True(zipViewModel.IsDirty);
        zipViewModel.Remove(new RemoveZipContent (new List<string> { "*.dll" }));
        Assert.True(zipViewModel.IsDirty);
        zipViewModel.SaveCommand.Execute(null);

        Assert.Equal(1, testDialog.GetSaveFileCount);
        Assert.Equal(0, testDialog.GetOpenFileCount);
        Assert.Equal(0, testDialog.ShowAboutCount);
        Assert.Equal(0, testDialog.ShouldSaveCount);
        Assert.Equal(0, testDialog.ShowErrorCount);
        Assert.Equal(0, closeCount);
    }

}
