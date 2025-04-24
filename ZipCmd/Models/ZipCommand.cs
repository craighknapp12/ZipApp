using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipCmd.Models;
internal class ZipCommand(IEnumerable<IZipAction> zipActions) : IZipCommand
{
    public void Execute(string actionName)
    {
        var action = zipActions.FirstOrDefault (a => a.ActionName.Equals(actionName, StringComparison.OrdinalIgnoreCase));
        action?.Execute();
    }
}
