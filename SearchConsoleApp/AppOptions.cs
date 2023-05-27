using CommandLine;

namespace DictionarySearch;


public sealed class AppOptions
{
    [Option('n', "minlen", Default = 1u, HelpText = "Minimum number of characters in the word.")]
    public uint MinLength { get; set; }

    [Option('x', "maxlen", Default = 10u, HelpText = "Maximum number of characters in the word.")]
    public uint MaxLength { get; set; }

    [Option('p', "pattern", HelpText = "Pattern to search. '*' means 0+ characters. '.' means 1 character.")]
    public string? Pattern { get; set; }

    [Option('l', "lang", Default = "ukenglish", HelpText = "Language to use.")]
    public string? Language { get; set; }
}
