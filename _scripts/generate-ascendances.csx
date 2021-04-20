#load "include.csx"

// Génère les pages des actions
//
// https://gitlab.com/pathfinder-fr/pf2-data-fr/-/blob/master/ancestries.json

using System.Text.Json;

await GenerateData("ancestries", "ancestries", "ascendances", DataGroup.Ancestries, ParseAncestry);

static void ParseAncestry(JsonElement item, JsonDocument frJsonDoc, StreamWriter writer)
{
    var hp = item.GetProperty("data").GetProperty("hp").GetInt32();
    writer.WriteLine($"hp: {hp}");

    // liste des langues en anglais
    var languages = item.GetProperty("data").GetProperty("languages").GetProperty("value").EnumerateArray().Select(x => x.GetString()).ToArray();

    // liste des langues traduites en FR d'après le glossaire
    var languagesFr = languages
        .Select(l =>
        {
            // le nom de l'entrée du glossaire = Language + Nom anglais avec la première lettre en majuscules
            var itemName = "Language" + FirstCharUpper(l);

            // on récupère l'entrée du glossaire FR que l'on renvoie
            return frJsonDoc.RootElement.GetProperty("PF2E").GetPropertyOrDefault(itemName)?.GetString();
        });

    // écriture de l'entête langues avec la liste des langues françaises
    writer.WriteArray("langues:", languagesFr);

    languages = item.GetProperty("data").GetProperty("additionalLanguages").GetProperty("value").EnumerateArray().Select(x => x.GetString()).ToArray();

    languagesFr = languages
        .Select(l =>
        {
            // le nom de l'entrée du glossaire = Language + Nom anglais avec la première lettre en majuscules
            var itemName = "Language" + FirstCharUpper(l);

            // on récupère l'entrée du glossaire FR que l'on renvoie
            return frJsonDoc.RootElement.GetProperty("PF2E").GetPropertyOrDefault(itemName)?.GetString();
        });

    // écriture de l'entête langues avec la liste des langues françaises
    writer.WriteArray("languesComplémentaires:", languagesFr);

    // liste des langues complémentaires

    // chargement des infos utiles d'après le fichier ancestries.json
    // var actionType = item.GetProperty("data").GetProperty("actionType").GetProperty("value").GetString();

    // // on détermine le nom de l'entrée du glossaire pour avoir le texte FR
    // // pour le type d'action, on recherche ActionType + type d'action du json
    // var itemName = "ActionType" + FirstCharUpper(actionType);

    // // à partir de ce nom ActionType... on recherche la traduction française dans le glossaire
    // var actionTypeLang = frJsonDoc.RootElement.GetProperty("PF2E").GetProperty(itemName).GetString();

    // writer.WriteLine($"type: {actionType}");
    // writer.WriteLine($"typeFr: {actionTypeLang}");
}