using ZippyLibrary.Interfaces;
using ZippyLibrary;

namespace TestZippy;
public class ZipLoadTests
{
    [Fact]
    public void TestArgument()
    {
        //var testDialog = new TestDialog();
        //IZip zip = new Zip(testDialog);
        //zip.Load(new[] { ".\\testArgument" });
        //Assert.Equal(0, testDialog.ShowErrorCount);
    }

    [Fact]
    public void TestArgumentNotSpecific()
    {
        //var testDialog = new TestDialog();
        //IZip zip = new Zip(testDialog);
        //zip.Load(new[] { ".\\testArgument2" });
        //zip.Load(new[] { ".\\testArgument3" });
        //zip.Load(new[] { ".\\*" });
        //Assert.Equal(1, testDialog.ShowErrorCount);
    }

    [Fact]
    public void TestArgumentTooMany()
    {
        //var testDialog = new TestDialog();
        //IZip zip = new Zip(testDialog);
        //zip.Load(new[] { ".\\test", ".\\test" });
        //Assert.Equal(1, testDialog.ShowErrorCount);
    }

}
