#load "include.csx"

using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;

public record StatusEntry(string Group, string French, string English, string Status, string OldStatus/* on ne stocke plus les trads FR et EN , string FrenchDescription, string EnglishDescription */);

Dictionary<string, Dictionary<string, StatusEntry>> data = new();
var files = Directory.GetFiles("../_ext/trads/data", "*.htm", new EnumerationOptions { RecurseSubdirectories = true });

foreach (var file in files)
{
    // data/backgrounds/0EIhRniun8jfdPeN.htm => 0EIhRniun8jfdPeN
    var match = Regex.Match(file, @"(\w{16})\.htm");
    if (!match.Success) continue;

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
            enDesc += line;
        }
        else if (isFrDesc)
        {
            frDesc += line;
        }
    }

    if(!data.TryGetValue(group, out var ids))
    {
        data[group] = new Dictionary<string, StatusEntry>();
    }

    data[group].Add(id, new StatusEntry(group, frName, enName, status, oldStatus));
}

Directory.CreateDirectory("../_data");
using (var jsonFile = File.Create("../_data/ids.json"))
{
    await System.Text.Json.JsonSerializer.SerializeAsync(jsonFile, data, new JsonSerializerOptions() { WriteIndented = true });
}
