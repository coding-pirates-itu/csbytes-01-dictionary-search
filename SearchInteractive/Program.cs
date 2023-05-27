using SearchLib;

var exit = false;
var minLength = 5u;
var maxLength = 6u;
var lang = "ukenglish";
var ws = new WordSearcher();

while (!exit)
{
    Console.Write("> ");
    var cmd = Console.ReadLine();
    if (cmd is null) break;

    try
    {
        var output = ProcessCommand(cmd);

        foreach (var line in output)
            Console.WriteLine(line);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}


IEnumerable<string> ProcessCommand(string cmd)
{
    if (cmd == "?") return CreateHelp();

    if (cmd.Contains(":"))
    {
        var cmdSplit = cmd.Split(':');
        switch (cmdSplit[0]) {
            case "l": lang = cmdSplit[1]; return new[] { $"Language set to '{lang}'" };
            case "n": minLength = uint.Parse(cmdSplit[1]); return new[] { $"Min length set to {minLength}" };
            case "x": maxLength = uint.Parse(cmdSplit[1]); return new[] { $"Max length set to {maxLength}" };
            case "q": exit = true; return new[] { "Exiting." };
            default: return CreateHelp();
        }
    }

    return ws.Search(lang, minLength, maxLength, cmd);
}


IEnumerable<string> CreateHelp()
{
    return new[]
    {
        "Status:",
        $"  Language (l): {lang}",
        $"  Min word length (n): {minLength}",
        $"  Max word length (x): {maxLength}",
        "Enter a command (letter:value, 'q:' for exit) or a pattern.",
    };
}
