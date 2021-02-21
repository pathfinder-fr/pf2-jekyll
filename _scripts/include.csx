using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

public static string GetScriptFolder([CallerFilePath] string path = null) => Path.GetDirectoryName(path);

Environment.CurrentDirectory = GetScriptFolder();

/// <summary>Contient les données de traduction française d'une entrée du compendium anglais.</summary>
public record TradDataEntry(string Id, string Group, string French, string English, string FrenchDescription, string EnglishDescription, string Status, string OldStatus);

/// <summary>Lit un fichier de traduction FR et exporte les données de l'entête et les descriptions dans une classe TradDataEntry.</summary>
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

/// <summary>Transforme un nom d'élément (action, sort, objet) en un nom unique standard, pouvant être utilisé pour un nom de fichier.</summary>
public static string AsNameId(string name)
{
    if (name == null) return null;

    return name
        .Replace(' ', '-')
        .Replace('\'', '-')
        .Replace('’', '\'')
        .Replace("(", string.Empty)
        .Replace(")", string.Empty)
        .ToLowerInvariant();
}

/// <summary>Passe la première lettre du nom indiqué en majusscules.</summary>
public static string FirstCharUpper(string name)
{
    if (name == null) return null;
    if (name.Length < 2) return name.ToUpperInvariant();
    return char.ToUpperInvariant(name[0]) + name.Substring(1);
}

public static string CleanupDescription(string description)
{
    // paragraphes <p>
    description = Regex.Replace(description, @"<p>", string.Empty);
    description = Regex.Replace(description, @"</p>", Environment.NewLine);

    // sauts de ligne <br>
    description = Regex.Replace(description, @"<br( ?/)?>", "  " + Environment.NewLine);

    // icones fontawesome
    description = Regex.Replace(description, @"<i class=""fas fa-suitcase""></i>", "");

    // span inutiles
    description = Regex.Replace(description, @"<span id=""ctl00_MainContent_DetailedOutput"">", "");
    description = Regex.Replace(description, @"<span style=""float: right;"">", " ");
    description = Regex.Replace(description, @"</span>", "");
    
    //<span id="ctl00_MainContent_DetailedOutput">

    // séparateurs <hr>
    description = Regex.Replace(description, @"<hr( /)?>(\r\n)?", string.Empty);

    // gras <strong>
    description = Regex.Replace(description, @"</?strong>", "**");

    // espaces insécables &nbsp;
    description = Regex.Replace(description, @"&nbsp;", " ");

    // listes <ul> / <li>
    description = Regex.Replace(description, @"</li></ul>(\r\n)?", Environment.NewLine);
    description = Regex.Replace(description, @"</li>", string.Empty);
    description = Regex.Replace(description, @"</?ul>(\r\n)?", Environment.NewLine);
    description = Regex.Replace(description, @"<li>", "- ");
    description = Regex.Replace(description, @"</li>", string.Empty);

    // titres <h1>, <h2>, etc.
    description = Regex.Replace(description, @"<h1 class=""title"">", "# ");
    description = Regex.Replace(description, @"<h2 class=""title"">", "## ");
    description = Regex.Replace(description, @"<h3 class=""title"">", "### ");
    description = Regex.Replace(description, @"</h\d>", Environment.NewLine);

    // liens compendium
    // format @Compendium[pf2e.actionspf2e.Bcxarzksqt9ezrs6]{Marchez rapidement}
    description = Regex.Replace(description, @"@Compendium\[(?<link>[a-zA-Z0-9\.\-]+)\]{(?<text>[^}]+)}", new MatchEvaluator(ReplaceCompendiumMatch));

    // format <a class="entity-link" draggable="true" data-pack="pf2e.equipment-srd" data-id="4ftXXUCBHcf4b0MH"><i class="fas fa-suitcase"></i>outils alchimiques</a>
    description = Regex.Replace(description, @"<a class=""entity-link"" draggable=""true"" data-pack=""(?<pack>[a-zA-Z0-9\.\-]+)"" data-id=""(?<id>[a-zA-Z0-9]+)"">(?<text>[^<]+)</a>", new MatchEvaluator(ReplaceCompendiumMatch));

    // liens de jet de dé
    // <a class="inline-roll roll" title="1d6" data-mode="roll" data-flavor="" data-formula="1d6">d6</a>
    description = Regex.Replace(description, @"<a class=""inline-roll roll"" title=""[^""]*"" data-mode=""roll"" data-flavor=""[^""]*"" data-formula=""[\dd+\-]+"">([\dd+\-]+)</a>", "$1");

    // liens externes
    description = Regex.Replace(description, @"<a (?:style=""text-decoration: underline;"" )?href=""([^""]+)"">([^<]+)</a>", @"<a href=""$1"">$2</a>");

    return description;
}

/// <summary>Renvoie le nom du dossier du projet de traduction contenant les fichiers pour le groupe de données indiqué.</summary>
public static string AsTradFolderName(this DataGroup @this)
{
    switch (@this)
    {
        case DataGroup.Ancestry_Features: return "ancestryfeatures";
        case DataGroup.Bestiary_Ability: return "bestiary-ability-glossary-srd";
        case DataGroup.Class_Features: return "classfeatures";
        case DataGroup.Condition_Items: return "conditionitems";
        case DataGroup.Conditions: return "conditionspf2e";
        case DataGroup.GameMaster_Guide: return "gmg-srd";
        case DataGroup.Bestiary: return "pathfinder-bestiary";
        case DataGroup.Bestiary_2: return "pathfinder-bestiary-2";
        case DataGroup.Pathfinder_Society_Boons: return "pathfinder-society-boons";
        default: return @this.ToString().ToLowerInvariant().Replace("_", "-");
    }
}

public static DataGroup? FromTradFolderName(string tradFolderName)
{
    switch (tradFolderName)
    {
        case "ancestryfeatures": return DataGroup.Ancestry_Features;
        case "bestiary-ability-glossary-srd": return DataGroup.Bestiary_Ability;
        case "classfeatures": return DataGroup.Class_Features;
        case "conditionitems": return DataGroup.Condition_Items;
        case "conditionspf2e": return DataGroup.Conditions;
        case "gmg-srd": return DataGroup.GameMaster_Guide;
        case "pathfinder-bestiary": return DataGroup.Bestiary;
        case "pathfinder-bestiary-2": return DataGroup.Bestiary_2;
        case "pathfinder-society-boons": return DataGroup.Pathfinder_Society_Boons;
        default:
            if (!Enum.TryParse<DataGroup>(tradFolderName.Replace("-", "_"), true, out var result))
            {
                return null;
            }
            return result;
    }
}

public static string AsDataFolderName(this DataGroup @this) => @this.ToString().ToLowerInvariant().Replace("_", "-");

public enum DataGroup
{
    Actions,
    Ancestries,
    Ancestry_Features,
    Archetypes,
    Backgrounds,
    Bestiary_Ability,
    Boons_And_Curses,
    Classes,
    Class_Features,
    Condition_Items,
    Conditions,
    Equipment,
    Familiar_Abilities,
    Feats,
    GameMaster_Guide,
    Hazards,
    Bestiary_2,
    Bestiary,
    Pathfinder_Society_Boons,
    Spell_Effects,
    Spells
}

public record StatusEntry(string French, string English);

public static class Ids
{
    private static Dictionary<string, Dictionary<string, StatusEntry>> entries;

    public static async Task EnsureIds()
    {
        if (entries != null)
        {
            return;
        }

        using (var jsonFile = File.OpenRead("../_data/ids.json"))
        {
            entries = await System.Text.Json.JsonSerializer.DeserializeAsync<Dictionary<string, Dictionary<string, StatusEntry>>>(jsonFile);
        }
    }

    public static string ResolveFrenchNameId(DataGroup group, string id)
    {
        if (entries == null) throw new InvalidOperationException("Vous devez appeller Ids.EnsureIds() avant de pouvoir utiliser le dictionnaire des Ids");

        var groupId = group.AsTradFolderName();

        if (!entries.TryGetValue(groupId, out var groupEntries))
        {
            throw new ArgumentOutOfRangeException(nameof(group), group, $"Il n'existe pas d'entrées pour le groupe {group} (clé {groupId})");
        }

        if (!groupEntries.TryGetValue(id, out var status))
        {
            throw new ArgumentOutOfRangeException(nameof(group), group, $"Il n'existe pas d'entrées avec l'id {id} pour le groupe {group} (clé {groupId})");
        }

        return AsNameId(status.French);
    }
}

public static DataGroup? CurrentGroup;

public static string ReplaceCompendiumMatch(Match match)
{
    string link;
    if (match.Groups.ContainsKey("link"))
    {
        // format @Compendium[pf2e.actionspf2e.Bcxarzksqt9ezrs6]{Marchez rapidement}    
        link = match.Groups["link"].Value;
    }
    else
    {
        // format <a class="entity-link" draggable="true" data-pack="pf2e.equipment-srd" data-id="4ftXXUCBHcf4b0MH"><i class="fas fa-suitcase"></i>outils alchimiques</a>
        var pack = match.Groups["pack"].Value;
        var dataId = match.Groups["id"].Value;
        link = pack + "." + dataId;
    }

    var text = match.Groups["text"].Value;

    var linkParts = link.Split('.');
    var groupName = linkParts[1]; // actionspf2e

    switch (groupName)
    {
        case "actionspf2e": groupName = "actions"; break;
        case "equipment-srd": groupName = "equipment"; break;
        case "feats-srd": groupName = "feats"; break;
        case "spells-srd": groupName = "spells"; break;
    }

    var group = FromTradFolderName(groupName); // DataGroup.Actions
    if (group == null)
    {
        WriteLine($"[WARNING] Impossible de trouver le groupe de données {linkParts[1]} pour le lien {link}");
        return text;
    }

    var groupFolder = group.Value.AsDataFolderName(); // _actions

    var id = linkParts[2]; // Bcxarzksqt9ezrs6
    var frName = Ids.ResolveFrenchNameId(group.Value, id);

    if (CurrentGroup == group)
    {
        return $"[{text}]({frName}.md)";
    }
    else
    {
        return $"[{text}](../{groupFolder}/{frName}.md)";
    }
}