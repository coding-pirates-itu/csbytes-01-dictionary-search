using System.Text.RegularExpressions;
using System.Text;

namespace SearchLib;


public class WordSearcher
{
    private string DataPath =>
        Path.Combine(
            Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) ?? "",
            "Data");


    public IReadOnlyCollection<string> GetDictionaries()
    {
        return Directory.GetFiles(DataPath).Select(f => Path.GetFileNameWithoutExtension(f)).ToList();
    }


    /// <summary>
    /// Search for a word according to the pattern.
    /// </summary>
    /// <param name="language">One of the available languages (see <see cref="GetDictionaries"/>)</param>
    /// <param name="minLength">Minimum word length</param>
    /// <param name="maxLength">Maximum word length, 0 means no limit</param>
    /// <param name="pattern">Pattern to search</param>
    /// <returns>A collection of found words</returns>
    public IReadOnlyCollection<string> Search(
        string language, uint minLength, uint maxLength, string? pattern, string allOf = "")
    {
        var res = new List<string>();
        var words = File.ReadAllLines($"{DataPath}/{language}.txt", Encoding.Latin1).Where(w => !string.IsNullOrWhiteSpace(w)).ToList();

        var rx = MaskToRegex(pattern ?? ".+");

        foreach (var word in words)
        {
            if (word.Length < minLength) continue;
            if (maxLength > 0 && word.Length > maxLength) continue;
            if (!string.IsNullOrEmpty(pattern) && !rx.Match(word).Success) continue;
            if (!string.IsNullOrEmpty(allOf) && !CheckAllChars(word, allOf)) continue;

            res.Add(word);
        }

        return res;
    }


    private static Regex MaskToRegex(string mask)
    {
        var rx = mask.Replace("-", ".").Replace("*", ".*");
        return new Regex($"^{rx}$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    }


    /// <summary>
    /// The word may only contain the given characters, all of them exactly once,
    /// unless the word length is less than the number of characters.
    /// </summary>
    private static bool CheckAllChars(string word, string allOf)
    {
        var letters = allOf.ToCharArray().GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
        var match = 0;

        foreach (var ch in word.ToCharArray())
        {
            if (!letters.TryGetValue(ch, out var counter)) return false;
            if (counter == 0) return false;
            letters[ch] = counter - 1;
            match++;
        }

        return match == word.Length;
    }
}
