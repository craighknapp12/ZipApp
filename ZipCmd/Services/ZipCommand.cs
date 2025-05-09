namespace ZipCmd.Services;
internal class ZipCommand(IEnumerable<IZipAction> zipActions) : IZipCommand
{
    public bool Execute(string actionName)
    {
        var action = zipActions.FirstOrDefault(a => a.CommandOption.Equals(actionName, StringComparison.OrdinalIgnoreCase));
        return action?.Execute() ?? false;
    }
}
