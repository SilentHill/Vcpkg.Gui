

using System.Text.Json;
using Vcpkg.Core;

class Program
{
    public static async Task Main(String[] args)
    {
		var cli = new VcpkgSession();
        cli.CmdletPath = @"C:\Users\stdcp\source\repos\vcpkg-2022.04.12\vcpkg.exe";
        // var searchResult = cli.Search("");
        var listResult = await cli.ForceListAsync();
        return;
    }
}
