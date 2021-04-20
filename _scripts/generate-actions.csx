#load "include.csx"

// Génère les pages des actions
//
// https://gitlab.com/pathfinder-fr/pf2-data-fr/-/blob/master/actionspf2e.json

using System.Text.Json;

await GenerateCollection(DataGroup.Actions, ParseAction);

static void ParseAction(JsonElement item, JsonDocument frJsonDoc, StreamWriter writer)
{
    // chargement des infos utiles d'après le fichier actionspf2e.json
    var actionType = item.GetProperty("data").GetProperty("actionType").GetProperty("value").GetString();

    // on détermine le nom de l'entrée du glossaire pour avoir le texte FR
    // pour le type d'action, on recherche ActionType + type d'action du json
    var itemName = "ActionType" + FirstCharUpper(actionType);

    // à partir de ce nom ActionType... on recherche la traduction française dans le glossaire
    var actionTypeLang = frJsonDoc.RootElement.GetProperty("PF2E").GetProperty(itemName).GetString();

    writer.WriteLine($"type: {actionType}");
    writer.WriteLine($"typeFr: {actionTypeLang}");
}