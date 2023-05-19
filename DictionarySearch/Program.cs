using System.Text;
using System.Text.RegularExpressions;
using CommandLine;
using DictionarySearch;


var opt = Parser.Default.ParseArguments<AppOptions>(args);
if (opt.Errors.Any()) return;

var words = File.ReadAllLines($"Data/{opt.Value.Language}.txt", Encoding.Latin1).Where(w => !string.IsNullOrWhiteSpace(w)).ToList();

var rx = MaskToRegex(opt.Value.Pattern ?? ".+");

foreach (var word in words)
{
    if (word.Length < opt.Value.MinLength) continue;
    if (word.Length > opt.Value.MaxLength) continue;
    if (! rx.Match(word).Success) continue;

    Console.WriteLine(word);
}


static Regex MaskToRegex(string mask)
{
    var rx = mask.Replace("*", ".*");
    return new Regex($"^{rx}$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
}
