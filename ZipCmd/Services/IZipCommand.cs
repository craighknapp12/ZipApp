using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipCmd.Services;
public interface IZipCommand
{
    void Execute(string actionName);
}
