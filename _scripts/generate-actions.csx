#load "include.csx"

// Ce fichier permet de générer les fichiers .md du dossier _actions à partir des données de traduction FoundryVTT
//
// Le script fonctionne ainsi :
//
// pour chaque page .htm du dossier actions du projet trads :
//
// - on lit les données de traduction du projet trads
// - on charge le fichier anglais pour récupérer des données utiles qui ne sont pas présentes dans le fichier de traduction (type d'action)
// - on utilise le glossaire français pour traduire certaines infos (type d'action)
//
// en : https://gitlab.com/hooking/foundry-vtt---pathfinder-2e/-/tree/master/packs/data/actions.db
// fr : https://gitlab.com/pathfinder-fr/foundryvtt-pathfinder2-fr/-/tree/master/data/actions

using System.Text.Json;
using System.Text.RegularExpressions;
using static System.Console;

var files = Directory.GetFiles("../_ext/trads/data/actions", "*.htm");

// chargement du glossaire anglais
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

CurrentGroup = DataGroup.Actions;

// on s'assure que le dossier des actions existe déjà
Directory.CreateDirectory($"../_actions/");

WriteLine($"Examen de {files.Length} fichiers...");
foreach (var file in files)
{
    // chargement données traduction et correspondance id <=> nom
    var trad = ReadTradDataEntry(file);

    if (string.IsNullOrEmpty(trad.French))
    {
        WriteLine($"L'action {trad.English} n'a pas été traduite en français et n'est donc pas disponible (ID {trad.Id})");
        continue;
    }

    // on détermine le nom du fichier en anglais contenant les données
    var enName = AsNameId(trad.English);

    // on génère le nom unique en français qui sera utilisé comme nom de fichier final
    var frNameId = AsNameId(trad.French);

    // chemin du fichier complet contenant les données en anglais
    var path = $"../_ext/module-en/packs/data/actions.db/{enName}.json";
    if (!File.Exists(path))
    {
        WriteLine($"Impossible de trouver le fichier d'action {enName} correspondant à {trad.English} (ID {trad.Id})");
        continue;
    }

    // chargement document json des données en anglais
    JsonDocument jsonDoc;
    using (var input = File.OpenRead(path))
    {
        jsonDoc = JsonDocument.Parse(input);
    }

    // chargement des infos utiles d'après le fichier anglais
    var actionType = jsonDoc.RootElement.GetProperty("data").GetProperty("actionType").GetProperty("value").GetString();

    // on détermine le nom de l'entrée du glossaire pour avoir le texte FR
    // pour le type d'action, on recherche ActionType + type d'action du json anglais
    var itemName = "ActionType" + FirstCharUpper(actionType);

    // à partir de ce nom ActionType... on recherche la traduction française dans le glossaire
    var actionTypeLang = frJsonDoc.RootElement.GetProperty("PF2E").GetProperty(itemName).GetString();

    // on va adapter la description française au markdown
    var description = CleanupDescription(trad.FrenchDescription);

    // ensuite on peut générer le fichier markdown final
    var targetPath = $"../_actions/{frNameId}.md";
    using (var writer = new StreamWriter(targetPath))
    {
        WriteFileHeader(writer, trad, enName, "action");

        writer.WriteLine($"type: {actionType}");
        writer.WriteLine($"typeFr: {actionTypeLang}");

        writer.WriteLine("---");
        writer.WriteLine(description);
    }
}