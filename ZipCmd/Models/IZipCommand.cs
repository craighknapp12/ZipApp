using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipCmd.Models;
public interface IZipCommand
{
    void Execute(string actionName);
}
