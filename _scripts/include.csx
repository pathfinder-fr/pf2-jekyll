using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

// initialisation du script : positionne automatiquement le dossier courant dans le dossier où se trouve ce script
// permet d'être sur que le dossier courant est le bon, peu importe d'où le script est lancé
Environment.CurrentDirectory = GetScriptFolder();

/// <summary>
/// Génère un unique fichier de données yaml à partir d'un fichier de données <paramref name="DataGroup" />.
/// </summary>
/// <param name="group">Groupe de données pour lequel on souhaite générer le fichier de données.</param>
/// <param name="misc">Traitement complémentaire à invoquer pour ajouter des propriétés au fichier de données.</param>
/// <param name="filter">Filtre permettant d'exclure certaines lignes du fichier de données en lecture. <see langword="null"/> pour ne pas filtrer et inclure toutes les données.</param>
/// <param name="customDataFileName">Nom du fichier en sortie dans le dossier _data. <see langword="null"/> pour utiliser l'identifiant du fichier de données en français <see cref="DataPack.FrenchId" />.</param>
static async Task<int> GenerateData(DataPack group, ParseElementMethod misc = null, Func<JsonElement, bool> filter = null, string customDataFileName = null)
{
    int count = 0;

    await Ids.EnsureIds();

    CurrentGroup = group;

    // nom du fichier en sortie : on privilégie customDataFileName si renseigné,
    // sinon on se base sur le nom du groupe de données FR
    customDataFileName = customDataFileName ?? group.FrenchId;
    var targetPath = $"../_data/{customDataFileName}.yml";

    // on s'assure que le dossier existe déjà
    Directory.CreateDirectory($"../_data");

    // fichier de données fr
    JsonDocument dataDoc;
    using (var input = File.OpenRead($"../_ext/data-fr/{group.FoundryPackName}.json"))
    {
        dataDoc = JsonDocument.Parse(input);
    }

    // glossaire fr
    JsonDocument frJsonDoc;
    using (var input = File.OpenRead("../_ext/module-fr/fr.json"))
    {
        frJsonDoc = JsonDocument.Parse(input);
    }

    // c'est parti...
    using (dataDoc)
    using (frJsonDoc)
    using (var writer = new StreamWriter(targetPath))
    {
        writer.WriteLine("# ATTENTION : Ne modifiez pas ce fichier");
        writer.WriteLine("# Ce fichier est généré automatiquement par un script d'après les données du module Foundry VTT officiel et de sa traduction");

        // on parcourt chaque élément du fichier de données, triés par la propriété "name"
        foreach (var item in dataDoc.RootElement.EnumerateArray().OrderBy(i => i.GetProperty("name").GetString()))
        {
            // on applique le filtre éventuel
            if (filter != null && filter(item) == false)
            {
                continue;
            }

            // chargement données communes
            var id = item.GetProperty("_id").GetString();
            var enName = item.GetProperty("name").GetString();
            var enDesc = item.GetProperty("description").GetString();

            try
            {

                // chargement des traductions
                string frName, frDesc, status;
                try
                {
                    status = item.GetProperty("translations").GetProperty("fr").GetProperty("status").GetString();
                    frName = item.GetProperty("translations").GetProperty("fr").GetPropertyOrDefault("name")?.GetString();
                    frDesc = item.GetProperty("translations").GetProperty("fr").GetPropertyOrDefault("description")?.GetString();
                }
                catch (Exception ex)
                {
                    WriteError($"Élément '{enName}' (id {id}) ignoré : erreur de lecture de la traduction : {ex.Message}");
                    continue;
                }

                var trad = new TradDataEntry(id, frName, enName, frDesc, enDesc, status, string.Empty);

                if (string.IsNullOrEmpty(trad.French))
                {
                    WriteWarning($"Élément '{enName}' (id {id}) ignoré : traduction française manquante (ID {trad.Id})");
                    continue;
                }

                // on détermine le nom du fichier en anglais contenant les données
                var enNameId = AsNameId(trad.English);

                // on génère le nom unique en français qui sera utilisé comme nom de fichier final
                var frNameId = AsNameId(trad.French);

                // on va adapter la description française au markdown
                var description = CleanupDescription(trad.FrenchDescription, out var hasErrors).Trim();
                if (hasErrors)
                {
                    WriteWarning($"Élément '{enName}' (id {id}) : au moins une erreur durant la transformation de la description");
                }

                // écriture des données
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

                count++;
            }
            catch (Exception ex)
            {
                WriteError($"Élément '{enName}' (id {id}) ignoré : {ex}");
            }
        }
    }

    return count;
}

/// <summary>
/// Génère les pages markdown à partir d'un fichier de données <paramref name="DataGroup" />.
/// </summary>
/// <param name="group">Groupe de données parmi les différentes valeurs de <see cref="DataPack" />.</param>
/// <param name="misc">Traitement complémentaire à invoquer pour ajouter des entêtes YAML front matter dans le fichier markdown généré.</param>
/// <param name="filter">Filtre permettant d'exclure certaines lignes du fichier de données en lecture. <see langword="null"/> pour ne pas filtrer et inclure toutes les données.</param>
/// <param name="customFolderName">Nom du dossier dans lequel les pages doivent être écrites. <see langword="null"/> pour utiliser l'identifiant du fichier de données en français <see cref="DataPack.FrenchId" /> préfixé d'un underscore.</param>
static async Task<int> GenerateCollection(DataPack group, ParseElementMethod misc = null, Func<JsonElement, bool> filter = null, string customFolderName = null)
{
    var count = 0;

    await Ids.EnsureIds();

    CurrentGroup = group;

    // nom du fichier en sortie : on privilégie customDataFileName si renseigné,
    // sinon on se base sur le nom du groupe de données FR préfixé d'un _
    customFolderName = customFolderName ?? $"_{group.FrenchId}";

    // on s'assure que le dossier existe déjà
    Directory.CreateDirectory($"../{customFolderName}/");

    JsonDocument dataDoc;
    using (var input = File.OpenRead($"../_ext/data-fr/{group.FoundryPackName}.json"))
    {
        dataDoc = JsonDocument.Parse(input);
    }

    // glossaire fr
    JsonDocument frJsonDoc;
    using (var input = File.OpenRead("../_ext/module-fr/fr.json"))
    {
        frJsonDoc = JsonDocument.Parse(input);
    }

    // c'est parti...
    using (dataDoc)
    using (frJsonDoc)
    {
        // on parcourt chaque élément du fichier de données
        foreach (var item in dataDoc.RootElement.EnumerateArray())
        {
            // on applique le filtre éventuel
            if (filter != null && filter(item) == false)
            {
                continue;
            }

            // chargement données communes
            var id = item.GetProperty("_id").GetString();
            var enName = item.GetProperty("name").GetString();
            var enDesc = item.GetProperty("description").GetString();

            try
            {
                // chargement des traductions
                string frName, frDesc, status;
                try
                {
                    status = item.GetProperty("translations").GetProperty("fr").GetProperty("status").GetString();
                    frName = item.GetProperty("translations").GetProperty("fr").GetPropertyOrDefault("name")?.GetString();
                    frDesc = item.GetProperty("translations").GetProperty("fr").GetPropertyOrDefault("description")?.GetString();
                }
                catch (Exception ex)
                {
                    WriteError($"Élément '{enName}' (id {id}) ignoré : erreur de lecture de la traduction : {ex.Message}");
                    continue;
                }

                var trad = new TradDataEntry(id, frName, enName, frDesc, enDesc, status, string.Empty);

                if (string.IsNullOrEmpty(trad.French))
                {
                    WriteWarning($"Élément '{enName}' (id {id}) ignoré : traduction française manquante");
                    continue;
                }

                // on détermine le nom du fichier en anglais contenant les données
                var enNameId = AsNameId(trad.English);

                // on génère le nom unique en français qui sera utilisé comme nom de fichier final
                var frNameId = AsNameId(trad.French);

                // on va adapter la description française au markdown
                var description = CleanupDescription(trad.FrenchDescription, out var hasErrors);
                if (hasErrors)
                {
                    WriteWarning($"Élément '{enName}' (id {id}) : au moins une erreur durant la transformation de la description");
                }

                // ensuite on peut générer le fichier markdown final
                var targetPath = $"../_{group.FrenchId}/{frNameId}.md";
                using (var writer = new StreamWriter(targetPath))
                {
                    var layout = group.FrenchId;

                    WritePageHeader(writer, trad, group, enNameId, layout);

                    if (misc != null)
                    {
                        try
                        {
                            misc(item, frJsonDoc, writer);
                        }
                        catch (Exception ex)
                        {
                            WriteError($"Élément '{enName}' (id {id}) ignoré : {ex}");
                            throw;
                        }
                    }

                    writer.WriteLine("---");
                    writer.WriteLine(description.Trim());
                }

                count++;
            }
            catch (Exception ex)
            {
                WriteError($"Impossible de lire l'élément '{enName}' (id {id}) : {ex}");
            }
        }
    }

    return count;
}

public delegate void ParseElementMethod(JsonElement item, JsonDocument frJsonDoc, StreamWriter writer);

/// <summary>Contient les données de traduction française d'une entrée du compendium anglais.</summary>
public record TradDataEntry(string Id, string French, string English, string FrenchDescription, string EnglishDescription, string Status, string OldStatus);

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

public static string CleanupDescription(string description, out bool hasErrors)
{
    hasErrors = false;
    var privateHasErrors = false;

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
    description = Regex.Replace(description, @"@Compendium\[(?<link>[a-zA-Z0-9\.\-]+)\]{(?<text>[^}]+)}", new MatchEvaluator(ReplaceCompendiumLinkMatch));

    // format <a class="entity-link" draggable="true" data-pack="pf2e.equipment-srd" data-id="4ftXXUCBHcf4b0MH"><i class="fas fa-suitcase"></i>outils alchimiques</a>
    description = Regex.Replace(description, @"<a class=""entity-link"" draggable=""true"" data-pack=""(?<pack>[a-zA-Z0-9\.\-]+)"" data-id=""(?<id>[a-zA-Z0-9]+)"">(?<text>[^<]+)</a>", new MatchEvaluator(ReplaceCompendiumLinkMatch));

    // liens de jet de dé
    // <a class="inline-roll roll" title="1d6" data-mode="roll" data-flavor="" data-formula="1d6">d6</a>
    description = Regex.Replace(description, @"<a class=""inline-roll roll"" title=""[^""]*"" data-mode=""roll"" data-flavor=""[^""]*"" data-formula=""[\dd+\-]+"">([\dd+\-]+)</a>", "$1");

    // liens externes
    description = Regex.Replace(description, @"<a (?:style=""text-decoration: underline;"" )?href=""([^""]+)"">([^<]+)</a>", @"<a href=""$1"">$2</a>");

    // nettoyage double retours chariots
    description = Regex.Replace(description, @"(\r\n){3,}", "\r\n\r\n");
    //description = Regex.Replace(description, @"\n{2,}", "\n");

    hasErrors = privateHasErrors;

    return description;

    // fonction invoquée pour remplacer les liens vers le compendium
    string ReplaceCompendiumLinkMatch(Match match)
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

        var dataGroup = DataPack.FromFoundrySystemName(groupName);

        if (dataGroup == null)
        {
            WriteWarning($"Impossible de trouver le groupe de données {groupName} pour le lien {link}");
            privateHasErrors = true;
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
        catch (Exception ex)
        {
            WriteWarning($"Impossible de trouver la traduction pour le lien {link} : {ex.Message}");
            privateHasErrors = true;
            return text;
        }

        if (CurrentGroup == dataGroup)
        {
            return $"[{text}]({frName}.html)";
        }
        else
        {
            return $"[{text}](../{groupFolder}/{frName}.html)";
        }
    }
}



/// <summary>Ecrit la partie commune à tous les scripts générant un fichier.</summary>
public static void WritePageHeader(StreamWriter writer, TradDataEntry trad, DataPack group, string enName, string layout)
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

#region String Extensions

/// <summary>Renvoie la valeur avec la première lettre en majuscules.</summary>
public static string FirstCharUpper(this string value)
{
    if (value == null) return null;
    if (value.Length < 2) return value.ToUpperInvariant();
    return char.ToUpperInvariant(value[0]) + value.Substring(1);
}

#endregion

#region Json Extensions

public static JsonElement? GetPropertyOrDefault(this JsonElement @this, string propertyName)
{
    if (@this.TryGetProperty(propertyName, out var property))
    {
        return property;
    }

    return null;
}

public static string GetPropertyString(this JsonElement @this, string propertyName)
=> @this.GetPropertyOrDefault(propertyName)?.GetString();

public static int? GetPropertyInt32(this JsonElement @this, string propertyName)
=> @this.GetPropertyOrDefault(propertyName)?.GetInt32();

public static object GetPropertyIntOrString(this JsonElement @this, string propertyName)
{
    var property = @this.GetPropertyOrDefault(propertyName);
    if (property == null)
        return null;

    if (property.Value.ValueKind == JsonValueKind.Number)
        return property.Value.GetInt32();

    return property.Value.GetString();
}

#endregion

#region StreamWriter Extensions

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

public static void WriteYamlProperty(this StreamWriter @this, string name, object value, bool omitIfNull = true)
{
    if (value == null && omitIfNull)
        return;

    @this.WriteLine($"{name}: {value}");
}

#endregion

public static string GetScriptFolder([CallerFilePath] string path = null) => Path.GetDirectoryName(path);

public record StatusEntry(string French, string English);

#region Console Extensions

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

#endregion

#region Ids

public static class Ids
{
    public const string FileName = "foundry-ids.json";
    private static Dictionary<string, Dictionary<string, StatusEntry>> entries;

    public static async Task EnsureIds()
    {
        if (entries != null)
        {
            return;
        }

        using (var jsonFile = File.OpenRead($"../_data/{FileName}"))
        {
            entries = await System.Text.Json.JsonSerializer.DeserializeAsync<Dictionary<string, Dictionary<string, StatusEntry>>>(jsonFile);
        }
    }

    public static async Task GenerateIds()
    {
        var data = new Dictionary<string, Dictionary<string, StatusEntry>>();

        foreach (var file in Directory.GetFiles("../_ext/data-fr", "*.json"))
        {
            var systemId = Path.GetFileNameWithoutExtension(file);
            var group = DataPack.All.FirstOrDefault(g => g.FoundryPackName == systemId);
            if (group == null)
            {
                WriteWarning($"Fichier de données {systemId} non supporté, ignoré");
                continue;
            }

            JsonDocument dataDoc;
            using (var input = File.OpenRead(file))
            {
                dataDoc = JsonDocument.Parse(input);
            }

            var count = 0;
            using (dataDoc)
            {
                if (!data.TryGetValue(group.Id, out var ids))
                {
                    ids = data[group.Id] = new Dictionary<string, StatusEntry>();
                }

                foreach (var item in dataDoc.RootElement.EnumerateArray().OrderBy(x => x.GetProperty("_id").GetString()))
                {
                    var id = item.GetProperty("_id").GetString();
                    var enName = item.GetProperty("name").GetString();
                    var frName = item.GetProperty("translations").GetProperty("fr").GetPropertyOrDefault("name")?.GetString();

                    ids.Add(id, new StatusEntry(frName, enName));
                    count++;
                }
            }

            WriteLine($"Fichier de données {systemId} traité : {count} éléments lus");
        }

        Directory.CreateDirectory("../_data");
        using (var jsonFile = File.Create($"../_data/{FileName}"))
        {
            await System.Text.Json.JsonSerializer.SerializeAsync(jsonFile, data, new JsonSerializerOptions() { WriteIndented = true });
        }

    }

    public static string ResolveFrenchNameId(DataPack pack, string id)
    {
        if (entries == null) throw new InvalidOperationException("Vous devez appeller Ids.EnsureIds() avant de pouvoir utiliser le dictionnaire des Ids");

        var packId = pack.Id;

        if (!entries.TryGetValue(packId, out var packEntries))
        {
            throw new ArgumentException($"Il n'existe pas d'entrées pour le pack {pack}");
        }

        if (!packEntries.TryGetValue(id, out var status))
        {
            throw new ArgumentException($"Il n'existe pas d'entrées avec l'id {id} pour le pack {pack}");
        }

        return AsNameId(status.French);
    }
}


#endregion

#region DataPack

public static DataPack CurrentGroup;

public class DataPack
{
    public static readonly DataPack ActionMacros = new DataPack("action-macros", "action-macros", "action-macros", "action-macros.db", ignoreLink: true);

    public static readonly DataPack Actions = new DataPack("actions", "actions", "actionspf2e", "actions.db");

    public static readonly DataPack Ancestries = new DataPack("ancestries", "ascendances", "ancestries", "ancestries.db");

    public static readonly DataPack AncestryFeatures = new DataPack("ancestry-features", "capacités-ascendances", "ancestryfeatures", "ancestryfeatures.db");

    public static readonly DataPack Archetypes = new DataPack("archetypes", "archetypes", "archetypes", "archetypes.db");

    public static readonly DataPack Backgrounds = new DataPack("backgrounds", "backgrounds", "backgrounds", "backgrounds.db");

    public static readonly DataPack BestiaryAbilities = new DataPack("bestiary-ability-glossary-srd", "capacités-monstres", "bestiary-ability-glossary-srd", "bestiary-ability-glossary-srd.db");

    public static readonly DataPack BoonsAndCurses = new DataPack("boons-and-curses", "bénédictions-et-malédictions", "boons-and-curses", "boons-and-curses.db");

    public static readonly DataPack Classes = new DataPack("classes", "classes", "classes", "classes.db");

    public static readonly DataPack ClassFeatures = new DataPack("class-features", "capacité-classe", "classfeatures", "classfeatures.db");

    public static readonly DataPack Conditions = new DataPack("conditions", "conditions", "conditionitems", "conditionitems.db");

    public static readonly DataPack Deities = new DataPack("deities", "divinités", "deities", "deities.db");

    public static readonly DataPack EquipmentEffects = new DataPack("equipment-effects", "effets-équipements", "equipment-effects", null, ignoreLink: true);

    public static readonly DataPack Equipments = new DataPack("equipments", "équipements", "equipment-srd", "equipment.db");

    public static readonly DataPack FamiliarAbilities = new DataPack("familiar-abilities", "capacités-familiers", "familiar-abilities", "familiar-abilities.db");

    public static readonly DataPack FeatEffects = new DataPack("feat-effects", "effets-don", "feat-effects", "feat-effects.db", ignoreLink: true);

    public static readonly DataPack Feats = new DataPack("feats", "dons", "feats-srd", "feats.db");

    public static readonly DataPack Gmg = new DataPack("gmg", "gdm", "gmg-srd", "gmg-srd.db", ignoreLink: true);

    public static readonly DataPack SpellEffects = new DataPack("spell-effects", "effet-sorts", "spell-effects", "spell-effects.db");

    public static readonly DataPack Spells = new DataPack("spells", "sorts", "spells-srd", "spells.db");

    public static readonly DataPack[] All = new[] {
        ActionMacros,
        Actions,
        Ancestries,
        AncestryFeatures,
        Archetypes,
        Backgrounds,
        BestiaryAbilities,
        BoonsAndCurses,
        Classes,
        ClassFeatures,
        Conditions,
        Deities,
        EquipmentEffects,
        Equipments,
        FamiliarAbilities,
        FeatEffects,
        Feats,
        Gmg,
        SpellEffects,
        Spells,
    };

    /// <summary />
    /// <param name="foundrySystemName">Nom du pack foundry de la donnée. Doit correspondre à la valeur name du fichier system.json (https://gitlab.com/hooking/foundry-vtt---pathfinder-2e/-/raw/master/system.json).</param>
    /// <param name="foundryDbName">Nom du fichier de données foundry. Doit correspondre a un nom de fichier dans le dossier `ext/data-fr`.</param>
    /// <param name="ignoreLink">Indique que ce pack ne désigne pas des pages, et que les liens pointant vers une entrée de ce pack sont ignorés et remplacés par un simple texte.</param>
    private DataPack(string id, string frenchId, string foundryPackName, string foundryDbName, bool ignoreLink = false)
    {
        Id = id;
        FrenchId = frenchId;
        FoundryPackName = foundryPackName;
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

    /// <summary>Nom du pack foundry de la donnée. Correspond à la propriété "name" du fichier system.json de foundry (https://gitlab.com/hooking/foundry-vtt---pathfinder-2e/-/raw/master/system.json).</summary>
    public string FoundryPackName { get; }

    /// <summary>Nom du dossier de base de données sous foundry.
    /// Correspond au nom du dossier de la propriété "path" du fichier system.json de foundry.
    /// Doit correspondre a un nom de fichier dans le dossier `ext/data-fr`.
    /// </summary>
    public string FoundryDbName { get; }

    public bool IgnoreLink { get; }

    string _tradFrFolderName;

    public string TradFrFolderName => _tradFrFolderName ?? (_tradFrFolderName = FoundryDbName.Substring(0, FoundryDbName.Length - 3));

    public override string ToString() => Id;

    public static DataPack FromFoundrySystemName(string foundrySystemName)
    {
        if (foundrySystemName == "conditionspf2e") foundrySystemName = "conditionitems";
        return All.FirstOrDefault(x => x.FoundryPackName == foundrySystemName) ?? throw new NotSupportedException($"Pack {foundrySystemName} non supporté");
    }

    public override int GetHashCode() => Id.GetHashCode();

    public override bool Equals(object obj)
    {
        if (obj is DataPack other) return Id == other.Id;
        return base.Equals(obj);
    }
}

#endregion