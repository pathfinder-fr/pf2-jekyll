using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

public static string GetScriptFolder([CallerFilePath] string path = null) => Path.GetDirectoryName(path);

Environment.CurrentDirectory = GetScriptFolder();


public record TradDataEntry(string Id, string Group, string French, string English, string FrenchDescription, string EnglishDescription, string Status, string OldStatus);

/// <summary>
/// Lit un fichier de traduction FR et exporte les données de l'entête et les descriptions dans une classe TradDataEntry.
/// </summary>
public TradDataEntry ReadTradDataEntry(string file)
{
    // data/backgrounds/0EIhRniun8jfdPeN.htm => 0EIhRniun8jfdPeN
    var match = Regex.Match(file, @"(\w{16})\.htm");
    if (!match.Success) return null;

    // ex: 0EIhRniun8jfdPeN
    var id = match.Groups[1].Value;

    // ex: backgrounds
    var group = Path.GetFileName(Path.GetDirectoryName(file));

    // content
    var enName = string.Empty;
    var frName = string.Empty;
    var status = string.Empty;
    var oldStatus = string.Empty;
    var isEnDesc = false;
    var isFrDesc = false;
    var enDesc = string.Empty;
    var frDesc = string.Empty;

    foreach (var line in File.ReadAllLines(file, Encoding.UTF8))
    {
        if (line.StartsWith("Name:"))
            enName = line.Substring(5).TrimStart();
        else if (line.StartsWith("Nom:"))
            frName = line.Substring(4).TrimStart();
        else if (line.StartsWith("État:"))
            status = line.Substring(5).TrimStart();
        else if (line.StartsWith("État d'origine:"))
            oldStatus = line.Substring(15).TrimStart();
        else if (line.StartsWith("------ Description (en) ------"))
        {
            isEnDesc = true;
            isFrDesc = false;
            continue;
        }
        else if (line.StartsWith("------ Description (fr) ------"))
        {
            isFrDesc = true;
            isEnDesc = false;
            continue;
        }

        if (isEnDesc)
        {
            enDesc += line + Environment.NewLine;
        }
        else if (isFrDesc)
        {
            frDesc += line + Environment.NewLine;
        }
    }

    return new TradDataEntry(id, group, frName, enName, frDesc, enDesc, status, oldStatus);
}

/// <summary>
/// Transforme un nom d'élément (action, sort, objet) en un nom unique standard, pouvant être utilisé pour un nom de fichier.
/// </summary>
public string AsNameId(string name)
{
    if (name == null) return null;

    return name
        .Replace(' ', '-')
        .Replace('\'', '-')
        .Replace("(", string.Empty)
        .Replace(")", string.Empty)
        .ToLowerInvariant();
}

/// <summary>
/// Passe la première lettre du nom indiqué en majusscules.
/// </summary>
public string FirstCharUpper(string name)
{
    if(name == null) return null;

    if(name.Length < 2) return name.ToUpperInvariant();

    return char.ToUpperInvariant(name[0]) + name.Substring(1);
}