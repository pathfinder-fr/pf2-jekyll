#load "include.csx"

// Génère les pages des états préjudiciables
//
// en : https://gitlab.com/hooking/foundry-vtt---pathfinder-2e/-/tree/master/packs/data/conditionitems.db
// fr : https://gitlab.com/pathfinder-fr/foundryvtt-pathfinder2-fr/-/tree/master/data/conditionitems

using System.Text.Json;

await GenerateFiles("conditionitems", "_etats", DataGroup.Condition_Items, "conditionitems.db", ParseAction);

static void ParseAction(JsonDocument jsonDoc, JsonDocument frJsonDoc, StreamWriter writer)
{
}