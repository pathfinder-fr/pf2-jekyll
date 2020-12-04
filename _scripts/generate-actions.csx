#load "include.csx"

using System.Text.Json;
using static System.Console;

var files = Directory.GetFiles("../_ext/trads/data/actions", "*.htm");

WriteLine($"Examen de {files.Length} fichiers...");
foreach (var file in files)
{
    // chargement données traduction et correspondance id <=> nom
    var id = ReadTradDataEntry(file);

    if (string.IsNullOrEmpty(id.French))
    {
        WriteLine($"L'action {id.English} n'a pas été traduite en français et n'est donc pas disponible (ID {id.Id})");
        continue;
    }

    // on détermine le nom du fichier en anglais contenant les données
    var enNameId = AsNameId(id.English);

    // on génère le nom du fichier en français
    var frNameId = AsNameId(id.French);

    // chemin du fichier complet contenant les données en anglais
    var path = $"../_ext/module-en/packs/data/actions.db/{enNameId}.json";
    if (!File.Exists(path))
    {
        WriteLine($"Impossible de trouver le fichier d'action {enNameId} correspondant à {id.English} (ID {id.Id})");
        continue;
    }

    // chargement document json
    JsonDocument jsonDoc;
    using (var input = File.OpenRead(path))
    {
        jsonDoc = JsonDocument.Parse(input);
    }

    // de là on peut générer un fichier destination
    var targetPath = $"../_actions/{frNameId}.md";

    Directory.CreateDirectory($"../_actions/");
    using (var writer = new StreamWriter(targetPath))
    {
        writer.WriteLine("---");
        writer.WriteLine("# ATTENTION : Ne modifiez pas ce fichier");
        writer.WriteLine("# Ce fichier est généré automatiquement par un script d'après les données du module Foundry VTT officiel et de sa traduction");
        writer.WriteLine($"title: {id.French}");
        writer.WriteLine($"titleEn: {id.English}");
        writer.WriteLine($"id: {id.Id}");
        writer.WriteLine($"group: {id.Group}");
        writer.WriteLine($"layout: action");
        writer.WriteLine("---");
        writer.WriteLine(id.FrenchDescription);
    }
}