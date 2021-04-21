using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

public static string GetScriptFolder([CallerFilePath] string path = null) => Path.GetDirectoryName(path);

Environment.CurrentDirectory = GetScriptFolder();

public delegate void ParseActionMethod(JsonDocument jsonDoc, JsonDocument frJsonDoc, StreamWriter writer);

public delegate void ParseActionMethod2(JsonElement item, JsonDocument frJsonDoc, StreamWriter writer);

static async Task GenerateData(DataGroup group, ParseActionMethod2 misc = null)
{
    await Ids.EnsureIds();

    CurrentGroup = group;

    JsonDocument dataDoc;
    using (var input = File.OpenRead($"../_ext/data-fr/{group.FoundrySystemName}.json"))
    {
        dataDoc = JsonDocument.Parse(input);
    }

    // glossaire fr
    JsonDocument frJsonDoc;
    using (var input = File.OpenRead("../_ext/module-fr/fr.json"))
    {
        frJsonDoc = JsonDocument.Parse(input);
    }


    // on s'assure que le dossier existe déjà
    Directory.CreateDirectory($"../_data");

    var targetPath = $"../_data/{group.FrenchId}.yml";
    using (var writer = new StreamWriter(targetPath))
    {
        WriteLine($"Examen des éléments...");

        writer.WriteLine("# ATTENTION : Ne modifiez pas ce fichier");
        writer.WriteLine("# Ce fichier est généré automatiquement par un script d'après les données du module Foundry VTT officiel et de sa traduction");

        foreach (var item in dataDoc.RootElement.EnumerateArray().OrderBy(i => i.GetProperty("name").GetString()))
        {
            // chargement données traduction et correspondance id <=> nom
            var id = item.GetProperty("_id").GetString();
            var enName = item.GetProperty("name").GetString();
            var enDesc = item.GetProperty("description").GetString();

            string frName, frDesc, status;
            try
            {
                status = item.GetProperty("translations").GetProperty("fr").GetProperty("status").GetString();
                frName = item.GetProperty("translations").GetProperty("fr").GetPropertyOrDefault("name")?.GetString();
                frDesc = item.GetProperty("translations").GetProperty("fr").GetPropertyOrDefault("description")?.GetString();
            }
            catch (Exception ex)
            {
                WriteError($"Impossible de lire la traduction pour l'élément {enName} : {ex}");
                continue;
            }

            var trad = new TradDataEntry(id, frName, enName, frDesc, enDesc, status, string.Empty);

            if (string.IsNullOrEmpty(trad.French))
            {
                WriteWarning($"L'élément '{trad.English}' n'a pas été traduit en français et n'est donc pas disponible (ID {trad.Id})");
                continue;
            }

            // on détermine le nom du fichier en anglais contenant les données
            var enNameId = AsNameId(trad.English);

            // on génère le nom unique en français qui sera utilisé comme nom de fichier final
            var frNameId = AsNameId(trad.French);

            // on va adapter la description française au markdown
            var description = CleanupDescription(trad.FrenchDescription).Trim();

            writer.WriteLine($"{frNameId}:");
            writer.WriteLine($"  id: {trad.Id}");
            writer.WriteLine($"  nom: {trad.French}");
            writer.WriteLine($"  nomEn: {trad.English}");

            writer.WriteLine($"  urlFr: https://gitlab.com/pathfinder-fr/foundryvtt-pathfinder2-fr/-/blob/master/data/{group.TradFrFolderName}/{trad.Id}.htm");
            writer.WriteLine($"  urlEn: https://gitlab.com/hooking/foundry-vtt---pathfinder-2e/-/blob/master/packs/data/{group.FoundryDbName}/{enName}.json");

            if (misc != null)
            {
                misc(item, frJsonDoc, writer);
            }

            writer.WriteLine("  description: |");

            foreach (var line in description.Split(Environment.NewLine))
            {
                writer.Write("    ");
                writer.WriteLine(line);
            }

            writer.WriteLine();
        }
    }
}

/// <summary>
/// Génère les pages pour le fichiers demandés.
/// </summary>
/// <param name="group">Groupe de données parmi les différentes valeurs de <see cref="DataGroup" />.</param>
/// <param name="misc">Méthode complémentaire à invoquer pour chaque fichier pour ajouter des entête front matter dans le fichier généré.</param>
static async Task GenerateCollection(DataGroup group, ParseActionMethod2 misc = null)
{
    await Ids.EnsureIds();

    CurrentGroup = group;

    JsonDocument dataDoc;
    using (var input = File.OpenRead($"../_ext/data-fr/{group.FoundrySystemName}.json"))
    {
        dataDoc = JsonDocument.Parse(input);
    }

    // glossaire fr
    JsonDocument frJsonDoc;
    using (var input = File.OpenRead("../_ext/module-fr/fr.json"))
    {
        frJsonDoc = JsonDocument.Parse(input);
    }


    // on s'assure que le dossier existe déjà
    Directory.CreateDirectory($"../_{group.FrenchId}/");

    WriteLine($"Examen des éléments...");
    foreach (var item in dataDoc.RootElement.EnumerateArray())
    {
        // chargement données traduction et correspondance id <=> nom
        var id = item.GetProperty("_id").GetString();
        var enName = item.GetProperty("name").GetString();
        var enDesc = item.GetProperty("description").GetString();

        try
        {
            string frName, frDesc, status;
            try
            {
                status = item.GetProperty("translations").GetProperty("fr").GetProperty("status").GetString();
                frName = item.GetProperty("translations").GetProperty("fr").GetPropertyOrDefault("name")?.GetString();
                frDesc = item.GetProperty("translations").GetProperty("fr").GetPropertyOrDefault("description")?.GetString();
            }
            catch (Exception ex)
            {
                WriteError($"Impossible de lire la traduction pour l'élément {enName} : {ex}");
                continue;
            }

            var trad = new TradDataEntry(id, frName, enName, frDesc, enDesc, status, string.Empty);

            if (string.IsNullOrEmpty(trad.French))
            {
                WriteWarning($"L'élément '{trad.English}' n'a pas été traduit en français et n'est donc pas disponible (ID {trad.Id})");
                continue;
            }

            // on détermine le nom du fichier en anglais contenant les données
            var enNameId = AsNameId(trad.English);

            // on génère le nom unique en français qui sera utilisé comme nom de fichier final
            var frNameId = AsNameId(trad.French);

            // on va adapter la description française au markdown
            var description = CleanupDescription(trad.FrenchDescription);

            // ensuite on peut générer le fichier markdown final
            var targetPath = $"../_{group.FrenchId}/{frNameId}.md";
            using (var writer = new StreamWriter(targetPath))
            {
                var layout = group.FrenchId;

                WriteFileHeader(writer, trad, group, enNameId, layout);

                if (misc != null)
                {
                    try
                    {
                        misc(item, frJsonDoc, writer);
                    }
                    catch (Exception ex)
                    {
                        WriteError($"Erreur sur traitement personnalisé sur l'élément '{trad.English}' : {ex}");
                        throw;
                    }
                }

                writer.WriteLine("---");
                writer.WriteLine(description.Trim());
            }
        }
        catch (Exception ex)
        {
            WriteError($"Impossible de lire l'élément '{enName}' (id {id}) : {ex}");
        }
    }
}

/// <summary>Contient les données de traduction française d'une entrée du compendium anglais.</summary>
public record TradDataEntry(string Id, string French, string English, string FrenchDescription, string EnglishDescription, string Status, string OldStatus);

/// <summary>Lit un fichier de traduction FR et exporte les données de l'entête et les descriptions dans une classe TradDataEntry.</summary>
public static TradDataEntry ReadTradDataEntry(string file)
{
    // data/backgrounds/0EIhRniun8jfdPeN.htm => 0EIhRniun8jfdPeN
    var match = Regex.Match(file, @"(\w{16})\.htm");
    if (!match.Success) return null;

    // ex: 0EIhRniun8jfdPeN
    var id = match.Groups[1].Value;

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

    return new TradDataEntry(id, frName, enName, frDesc, enDesc, status, oldStatus);
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
        .Replace("[", string.Empty)
        .Replace("]", string.Empty)
        .Replace("!", string.Empty)
        .Replace("?", string.Empty)
        .TrimEnd('-')
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
    // passages en retours chariots windows
    description = Regex.Replace(description, @"\n", Environment.NewLine);

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

    // italique <em>
    description = Regex.Replace(description, @"</?em>", "*");

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
    description = Regex.Replace(description, @"<h1>", "# ");
    description = Regex.Replace(description, @"<h2>", "## ");
    description = Regex.Replace(description, @"<h3>", "### ");
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

    // nettoyage double retours chariots
    description = Regex.Replace(description, @"(\r\n){3,}", "\r\n\r\n");
    //description = Regex.Replace(description, @"\n{2,}", "\n");

    return description;
}

/// <summary>Ecrit la partie commune à tous les scripts générant un fichier.</summary>
public static void WriteFileHeader(StreamWriter writer, TradDataEntry trad, DataGroup group, string enName, string layout)
{
    writer.WriteLine("---");
    writer.WriteLine("# ATTENTION : Ne modifiez pas ce fichier");
    writer.WriteLine("# Ce fichier est généré automatiquement par un script d'après les données du module Foundry VTT officiel et de sa traduction");
    writer.WriteLine($"title: {trad.French}");
    writer.WriteLine($"titleEn: {trad.English}");
    writer.WriteLine($"id: {trad.Id}");
    writer.WriteLine($"urlFr: https://gitlab.com/pathfinder-fr/foundryvtt-pathfinder2-fr/-/blob/master/data/{group.TradFrFolderName}/{trad.Id}.htm");
    writer.WriteLine($"urlEn: https://gitlab.com/hooking/foundry-vtt---pathfinder-2e/-/blob/master/packs/data/{group.FoundryDbName}/{enName}.json");
    if (layout != null)
    {
        writer.WriteLine($"layout: {layout}");
    }
}

public static JsonElement? GetPropertyOrDefault(this JsonElement @this, string propertyName)
{
    if (@this.TryGetProperty(propertyName, out var property))
    {
        return property;
    }

    return null;
}

public static void WriteArray(this StreamWriter @this, string name, IEnumerable<string> items)
{
    if (items.Where(x => !string.IsNullOrWhiteSpace(x)).Count() == 0)
    {
        return;
    }

    var padding = new string(' ', name.Length - name.TrimStart().Length);

    @this.WriteLine(name);

    foreach (var item in items.OrderBy(x => x))
    {
        if (!string.IsNullOrWhiteSpace(item))
        {
            @this.WriteLine($"{padding}  - {item}");
        }
    }
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

        var groupId = group.Id;

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

public static DataGroup CurrentGroup;

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

    var dataGroup = DataGroup.FromFoundrySystemName(groupName);

    if (dataGroup == null)
    {
        WriteWarning($"Impossible de trouver le groupe de données {groupName} pour le lien {link}");
        return text;
    }

    if (dataGroup.IgnoreLink)
    {
        return text;
    }

    var groupFolder = dataGroup.FrenchId; // actions

    var id = linkParts[2]; // Bcxarzksqt9ezrs6
    string frName;
    try
    {
        frName = Ids.ResolveFrenchNameId(dataGroup, id);
    }
    catch (Exception)
    {
        WriteWarning($"Impossible de trouver la traduction pour le lien {link} pointant vers l'id {id} (groupe {dataGroup})");
        return text;
    }

    if (CurrentGroup == dataGroup)
    {
        return $"[{text}]({frName}.md)";
    }
    else
    {
        return $"[{text}](../{groupFolder}/{frName}.md)";
    }
}

public static void WriteWarning(string message)
{
    var previous = Console.ForegroundColor;
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(message);
    Console.ForegroundColor = previous;
}

public static void WriteError(string message)
{
    var previous = Console.ForegroundColor;
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(message);
    Console.ForegroundColor = previous;
}

public class DataGroup
{
    public static readonly DataGroup Actions = new DataGroup("actions", "actions", "actionspf2e", "actions.db");

    public static readonly DataGroup Ancestries = new DataGroup("ancestries", "ascendances", "ancestries", "ancestries.db");

    public static readonly DataGroup Conditions = new DataGroup("conditions", "conditions", "conditionitems", "conditionitems.db");

    public static readonly DataGroup Feats = new DataGroup("feats", "dons", "feats-srd", "feats.db");

    public static readonly DataGroup Equipments = new DataGroup("equipments", "équipements", "equipment-srd", "equipment.db");

    public static readonly DataGroup FeatEffects = new DataGroup("feat-effects", "effets-don", "feat-effects", "feat-effects.db", ignoreLink: true);

    public static readonly DataGroup ClassFeatures = new DataGroup("class-features", "capacité-classe", "classfeatures", "classfeatures.db");

    public static readonly DataGroup Spells = new DataGroup("spells", "sorts", "spells-srd", "spells.db");

    public static readonly DataGroup ActionMacros = new DataGroup("action-macros", "action-macros", "action-macros", "action-macros.db", ignoreLink: true);

    public static readonly DataGroup BestiaryAbilities = new DataGroup("bestiary-ability-glossary-srd", "capacités-monstres", "bestiary-ability-glossary-srd", "bestiary-ability-glossary-srd.db");

    public static readonly DataGroup AncestryFeatures = new DataGroup("ancestry-features", "capacités-ascendances", "ancestryfeatures", "ancestryfeatures.db");

    public static readonly DataGroup Deities = new DataGroup("deities", "divinités", "deities", "deities.db");

    public static readonly DataGroup Backgrounds = new DataGroup("backgrounds", "backgrounds", "backgrounds", "backgrounds.db");

    public static readonly DataGroup FamiliarAbilities = new DataGroup("familiar-abilities", "capacités-familiers", "familiar-abilities", "familiar-abilities.db");

    public static readonly DataGroup SpellEffects = new DataGroup("spell-effects", "effet-sorts", "spell-effects", "spell-effects.db");

    public static readonly DataGroup Archetypes = new DataGroup("archetypes", "archétypes", "archetypes", "archetypes.db");

    public static readonly DataGroup[] All = new[] {
        Actions,
        Ancestries,
        Conditions,
        Feats,
        Equipments,
        FeatEffects,
        ClassFeatures,
        Spells,
        ActionMacros,
        BestiaryAbilities,
        AncestryFeatures,
        Deities,
        Backgrounds,
        FamiliarAbilities,
        SpellEffects,
        Archetypes
    };

    /// <summary>
    /// </summary>
    private DataGroup(string id, string frenchId, string foundrySystemName, string foundryDbName, bool ignoreLink = false)
    {
        Id = id;
        FrenchId = frenchId;
        FoundrySystemName = foundrySystemName;
        FoundryDbName = foundryDbName;
        IgnoreLink = ignoreLink;
    }

    /// <summary>Identifiant unique du groupe de données, correspond au nom en anglais en minuscules.</summary>
    public string Id { get; }

    /// <summary>
    /// Identifiant unique français du groupe de données, correspond au nom en français en minuscules et au pluriel.
    /// Correspond au nom du dossier ou du fichier dans lequel les éléments doivent être stockés.
    /// </summary>
    public string FrenchId { get; }

    /// <summary>Nom système dans foundry. Correspond à la propriété "name" du fichier system.json de foundry.</summary>
    public string FoundrySystemName { get; }

    /// <summary>Nom du dossier de base de données sous foundry. Correspond au nom du dossier de la propriété "path" du fichier system.json de foundry.</summary>
    public string FoundryDbName { get; }

    public bool IgnoreLink { get; }

    string _tradFrFolderName;

    public string TradFrFolderName => _tradFrFolderName ?? (_tradFrFolderName = FoundryDbName.Substring(0, FoundryDbName.Length - 3));

    public override string ToString() => $"{Id} - {FrenchId}";

    public static DataGroup FromFoundrySystemName(string foundrySystemName)
    {
        if (foundrySystemName == "conditionspf2e") foundrySystemName = "conditionitems";
        return All.FirstOrDefault(x => x.FoundrySystemName == foundrySystemName) ?? throw new NotSupportedException($"Système {foundrySystemName} non supporté");
    }

    public override int GetHashCode() => Id.GetHashCode();

    public override bool Equals(object obj)
    {
        if (obj is DataGroup other) return Id == other.Id;
        return base.Equals(obj);
    }
}