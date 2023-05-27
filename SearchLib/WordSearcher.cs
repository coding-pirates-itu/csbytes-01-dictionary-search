using System.Text.RegularExpressions;
using System.Text;

namespace SearchLib;


public class WordSearcher
{
    public IReadOnlyCollection<string> Search(string language, uint minLength, uint maxLength, string? pattern)
    {
        var res = new List<string>();
        var words = File.ReadAllLines($"Data/{language}.txt", Encoding.Latin1).Where(w => !string.IsNullOrWhiteSpace(w)).ToList();

        var rx = MaskToRegex(pattern ?? ".+");

        foreach (var word in words)
        {
            if (word.Length < minLength) continue;
            if (word.Length > maxLength) continue;
            if (!rx.Match(word).Success) continue;

            res.Add(word);
        }

        return res;
    }


    private static Regex MaskToRegex(string mask)
    {
        var rx = mask.Replace("-", ".").Replace("*", ".*");
        return new Regex($"^{rx}$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    }
}
