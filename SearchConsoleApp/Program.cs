using CommandLine;
using DictionarySearch;
using SearchLib;

var opt = Parser.Default.ParseArguments<AppOptions>(args);
if (opt.Errors.Any()) return;

var words = new WordSearcher().Search(opt.Value.Language ?? "", opt.Value.MinLength, opt.Value.MaxLength, opt.Value.Pattern);
foreach (var word in words)
{
    Console.WriteLine(word);
}
