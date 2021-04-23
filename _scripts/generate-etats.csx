#load "include.csx"

// Génère les pages des états préjudiciables
//
// en : https://gitlab.com/hooking/foundry-vtt---pathfinder-2e/-/tree/master/packs/data/conditionitems.db
// fr : https://gitlab.com/pathfinder-fr/foundryvtt-pathfinder2-fr/-/tree/master/data/conditionitems

using System.Text.Json;

var count = await GenerateCollection(DataPack.Conditions, ParseCondition);

WriteLine($"Génération terminée : {count} états traités");

static void ParseCondition(JsonElement jsonDoc, JsonDocument frJsonDoc, StreamWriter writer)
{
}