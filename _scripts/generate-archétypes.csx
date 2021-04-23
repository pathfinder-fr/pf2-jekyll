#load "include.csx"

// Génère les pages des actions
//
// https://gitlab.com/pathfinder-fr/pf2-data-fr/-/blob/master/actionspf2e.json

using System.Text.Json;

var count = await GenerateCollection(DataPack.Archetypes, ParseArchetype);

WriteLine($"Génération terminée : {count} archétypes traités");

static void ParseArchetype(JsonElement item, JsonDocument frJsonDoc, StreamWriter writer)
{
}