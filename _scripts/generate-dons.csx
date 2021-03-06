#load "include.csx"

// Génère les pages des dons
//
// en : https://gitlab.com/hooking/foundry-vtt---pathfinder-2e/-/tree/master/packs/data/feats.db
// fr : https://gitlab.com/pathfinder-fr/foundryvtt-pathfinder2-fr/-/tree/master/data/feats

using System.Text.Json;

var count = await GenerateCollection(DataPack.Feats, ParseFeat);

WriteLine($"Génération terminée : {count} dons traités");

static void ParseFeat(JsonElement jsonDoc, JsonDocument frJsonDoc, StreamWriter writer)
{
}