using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

public static string GetScriptFolder([CallerFilePath] string path = null) => Path.GetDirectoryName(path);

Environment.CurrentDirectory = GetScriptFolder();

public delegate void ParseActionMethod(JsonDocument jsonDoc, JsonDocument frJsonDoc, StreamWriter writer);

/// <summary>
/// Génère les pages pour le fichiers demandés.
/// </summary>
/// <remarks>
/// Le script fonctionne ainsi :
///
/// pour chaque page .htm du dossier actions du projet trads :
///
/// - on lit les données de traduction du projet trads
/// - on charge le fichier anglais pour récupérer des données utiles qui ne sont pas présentes dans le fichier de traduction (type d'action)
/// - on utilise le glossaire français pour traduire certaines infos (type d'action)
/// </remarks>
/// <param name="tradFolderName">Nom du dossier contenant les fichiers dans le projet de traduction. ex: feats.</param>
/// <param name="colDir">Nom de la collection sous jekyll, préfixée par un _. ex: _dons. Doit être une des valeurs renvoyées par <see cref="AsDataFolderName" />.</param>
/// <param name="group">Groupe de données parmi les différentes valeurs de <see cref="DataGroup" />.</param>
/// <param name="misc">Méthode complémentaire à invoquer pour chaque fichier pour ajouter des entête front matter dans le fichier généré.</param>
async static Task GenerateFiles(string tradFolderName, string colDir, DataGroup group, string enFolderName, ParseActionMethod misc = null)
{
    CurrentGroup = group;

    var files = Directory.GetFiles($"../_ext/trads/data/{tradFolderName}", "*.htm");

    // glossaire anglais
    // JsonDocument enJsonDoc;
    // using (var input = File.OpenRead("../_ext/module-en/static/lang/en.json"))
    // {
    //     enJsonDoc = JsonDocument.Parse(input);
    // }

    // glossaire fr
    JsonDocument frJsonDoc;
    using (var input = File.OpenRead("../_ext/module-fr/fr.json"))
    {
        frJsonDoc = JsonDocument.Parse(input);
    }

    // chargement mapping ids
    await Ids.EnsureIds();

    // on s'assure que le dossier existe déjà
    Directory.CreateDirectory($"../{colDir}/");

    WriteLine($"Examen de {files.Length} fichiers...");
    foreach (var file in files)
    {
        // chargement données traduction et correspondance id <=> nom
        var trad = ReadTradDataEntry(file);

        if (string.IsNullOrEmpty(trad.French))
        {
            WriteLine($"La donnée {trad.English} n'a pas été traduite en français et n'est donc pas disponible (ID {trad.Id})");
            continue;
        }

        // on détermine le nom du fichier en anglais contenant les données
        var enName = AsNameId(trad.English);

        // on génère le nom unique en français qui sera utilisé comme nom de fichier final
        var frNameId = AsNameId(trad.French);

        // chemin du fichier complet contenant les données en anglais
        var path = $"../_ext/module-en/packs/data/{enFolderName}/{enName}.json";
        if (!File.Exists(path))
        {
            WriteLine($"Impossible de trouver le fichier d'origine {enName} correspondant à {trad.English} (ID {trad.Id})");
            continue;
        }

        // chargement document json des données en anglais
        JsonDocument jsonDoc;
        using (var input = File.OpenRead(path))
        {
            jsonDoc = JsonDocument.Parse(input);
        }

        // on va adapter la description française au markdown
        var description = CleanupDescription(trad.FrenchDescription);

        // ensuite on peut générer le fichier markdown final
        var targetPath = $"../{colDir}/{frNameId}.md";
        using (var writer = new StreamWriter(targetPath))
        {
            // on suppose que le layout est le nom de la collection sans le _
            var layout = colDir.Substring(1);

            WriteFileHeader(writer, trad, tradFolderName, enFolderName, enName, layout);

            if (misc != null)
            {
                misc(jsonDoc, frJsonDoc, writer);
            }

            writer.WriteLine("---");
            writer.WriteLine(description);
        }
    }
}

/// <summary>Contient les données de traduction française d'une entrée du compendium anglais.</summary>
public record TradDataEntry(string Id, string Group, string French, string English, string FrenchDescription, string EnglishDescription, string Status, string OldStatus);

/// <summary>Lit un fichier de traduction FR et exporte les données de l'entête et les descriptions dans une classe TradDataEntry.</summary>
public static TradDataEntry ReadTradDataEntry(string file)
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
        .Replace('/', '-')
        .Replace('\\', '-')
        .Replace("(", string.Empty)
        .Replace(")", string.Empty)
        .Replace("!", string.Empty)
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

/// <summary>Ecrit la partie commune à tous les scripts générant un fichier.</summary>
public static void WriteFileHeader(StreamWriter writer, TradDataEntry trad, string tradFolderName, string enFolderName, string enName, string layout)
{
    writer.WriteLine("---");
    writer.WriteLine("# ATTENTION : Ne modifiez pas ce fichier");
    writer.WriteLine("# Ce fichier est généré automatiquement par un script d'après les données du module Foundry VTT officiel et de sa traduction");
    writer.WriteLine($"title: {trad.French}");
    writer.WriteLine($"titleEn: {trad.English}");
    writer.WriteLine($"id: {trad.Id}");
    writer.WriteLine($"urlFr: https://gitlab.com/pathfinder-fr/foundryvtt-pathfinder2-fr/-/blob/master/data/{tradFolderName}/{trad.Id}.htm");
    writer.WriteLine($"urlEn: https://gitlab.com/hooking/foundry-vtt---pathfinder-2e/-/blob/master/packs/data/{enFolderName}/{enName}.json");
    writer.WriteLine($"group: {trad.Group}");
    if (layout != null)
    {
        writer.WriteLine($"layout: {layout}");
    }
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

/// <summary>Pour un groupe de données, renvoie le nom du dossier de données où les fichiers doivent être écrits (sans le _).</summary>
public static string AsDataFolderName(this DataGroup @this)
{
    switch (@this)
    {
        case DataGroup.Feats: return "dons";
        case DataGroup.Condition_Items: return "etats";
        default: return @this.ToString().ToLowerInvariant().Replace("_", "-");
    }
}

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
    string frName;
    try
    {
        frName = Ids.ResolveFrenchNameId(group.Value, id);
    }
    catch (Exception)
    {
        WriteLine($"[WARNING] Impossible de trouver le nom français pour le lien {link} pointant vers la page {id} (groupe {group.Value})");
        return text;
    }

    if (CurrentGroup == group)
    {
        return $"[{text}]({frName}.md)";
    }
    else
    {
        return $"[{text}](../{groupFolder}/{frName}.md)";
    }
}