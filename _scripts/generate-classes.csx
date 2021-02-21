#load "include.csx"

// Ce fichier permet de générer les fichiers .md du dossier _classes à partir des données de traduction FoundryVTT
//
//
// en : https://gitlab.com/hooking/foundry-vtt---pathfinder-2e/-/tree/master/packs/data/classes.db
// fr : https://gitlab.com/pathfinder-fr/foundryvtt-pathfinder2-fr/-/tree/master/data/classes

using System.Text.Json;
using System.Text.RegularExpressions;
using static System.Console;

var files = Directory.GetFiles("../_ext/trads/data/classes", "*.htm");

// glossaire fr
JsonDocument frJsonDoc;
using (var input = File.OpenRead("../_ext/module-fr/fr.json"))
{
    frJsonDoc = JsonDocument.Parse(input);
}

// on s'assure que le dossier des actions existe déjà
Directory.CreateDirectory($"../_classes/");

WriteLine($"Examen de {files.Length} fichiers...");
foreach (var file in files)
{
    // chargement données traduction et correspondance id <=> nom
    var trad = ReadTradDataEntry(file);

    if (string.IsNullOrEmpty(trad.French))
    {
        WriteLine($"La classe {trad.English} n'a pas été traduite en français et n'est donc pas disponible (ID {trad.Id})");
        continue;
    }

    // on détermine le nom du fichier en anglais contenant les données
    var enName = AsNameId(trad.English);

    // on génère le nom unique en français qui sera utilisé comme nom de fichier final
    var frNameId = AsNameId(trad.French);

    // chemin du fichier complet contenant les données en anglais
    var path = $"../_ext/module-en/packs/data/classes.db/{enName}.json";
    if (!File.Exists(path))
    {
        WriteLine($"Impossible de trouver le fichier de classe {enName} correspondant à {trad.English} (ID {trad.Id})");
        continue;
    }

    // chargement document json des données en anglais
    JsonDocument jsonDoc;
    using (var input = File.OpenRead(path))
    {
        jsonDoc = JsonDocument.Parse(input);
    }

    // on va adapter la description française au markdown
    // par des opérations successives sur description
    var description = trad.FrenchDescription;

    // paragraphes <p>
    description = Regex.Replace(description, @"<p>", string.Empty);
    description = Regex.Replace(description, @"</p>", Environment.NewLine + Environment.NewLine);

    // séparateurs <hr>
    description = Regex.Replace(description, @"<hr( /)?>", "----" + Environment.NewLine + Environment.NewLine);

    // gras <strong>
    description = Regex.Replace(description, @"</?strong>", "**");

    // espaces insécables &nbsp;
    description = Regex.Replace(description, @"&nbsp;", " ");

    // listes <ul> / <li>
    description = Regex.Replace(description, @"</?ul>", "");
    description = Regex.Replace(description, @"<li>", "- ");
    description = Regex.Replace(description, @"</li>", Environment.NewLine);

    // titres <h1>, <h2>, etc.
    description = Regex.Replace(description, @"<h2 class=""title"">", "## ");
    description = Regex.Replace(description, @"</h\d>", Environment.NewLine + Environment.NewLine);

    // ensuite on peut générer le fichier markdown final
    var targetPath = $"../_actions/{frNameId}.md";
    using (var writer = new StreamWriter(targetPath))
    {
        writer.WriteLine("---");
        writer.WriteLine("# ATTENTION : Ne modifiez pas ce fichier");
        writer.WriteLine("# Ce fichier est généré automatiquement par un script d'après les données du module Foundry VTT officiel et de sa traduction");
        writer.WriteLine($"title: {trad.French}");
        writer.WriteLine($"titleEn: {trad.English}");
        writer.WriteLine($"id: {trad.Id}");
        writer.WriteLine($"group: {trad.Group}");
        writer.WriteLine($"layout: action");

        writer.WriteLine("---");
        writer.WriteLine(description);
    }
}