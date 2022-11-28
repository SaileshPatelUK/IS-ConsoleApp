using CommandLine;

namespace ISConsoleApp.Options
{
    public class TableOptions
    {
        [Option('i', "tableItem", Required = false, ResourceType = typeof(int), Default = 1)]
        public int TableItem { get; set; }

        [Option('f', "file", Required = false, ResourceType = typeof(string), Default = "commands.json")]
        public string FilePath { get; set; }
    }
}
