#load "include.csx"

// Génère les pages des actions
//
// en : https://gitlab.com/hooking/foundry-vtt---pathfinder-2e/-/tree/master/packs/data/actions.db
// fr : https://gitlab.com/pathfinder-fr/foundryvtt-pathfinder2-fr/-/tree/master/data/actions

using System.Text.Json;

await GenerateFiles("actions", "_actions", DataGroup.Actions, "actions.db", ParseAction);

static void ParseAction(JsonDocument jsonDoc, JsonDocument frJsonDoc, StreamWriter writer)
{
    // chargement des infos utiles d'après le fichier anglais
    var actionType = jsonDoc.RootElement.GetProperty("data").GetProperty("actionType").GetProperty("value").GetString();

    // on détermine le nom de l'entrée du glossaire pour avoir le texte FR
    // pour le type d'action, on recherche ActionType + type d'action du json anglais
    var itemName = "ActionType" + FirstCharUpper(actionType);

    // à partir de ce nom ActionType... on recherche la traduction française dans le glossaire
    var actionTypeLang = frJsonDoc.RootElement.GetProperty("PF2E").GetProperty(itemName).GetString();

    writer.WriteLine($"type: {actionType}");
    writer.WriteLine($"typeFr: {actionTypeLang}");
}