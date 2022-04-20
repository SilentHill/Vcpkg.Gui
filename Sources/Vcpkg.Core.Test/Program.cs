

using System.Text.Json;
using Vcpkg.Core;

class Program
{
    public static void Main(String[] args)
    {
		var cli = new CliSession();
        cli.VcpkgPath = @"C:\Users\stdcp\source\repos\vcpkg-2022.04.12\vcpkg.exe";
        // var searchResult = cli.Search("");
        var listResult = cli.ForceList();
        return;
    }
}
