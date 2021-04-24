#load "include.csx"

// Génère les pages des archétypes
//
// https://gitlab.com/pathfinder-fr/pf2-data-fr/-/blob/master/archetypes.json

using System.Text.Json;

// génération archétypes classiques
var count = await GenerateCollection(DataPack.Archetypes, ParseArchetype);
WriteLine($"Génération terminée : {count} archétypes traités");
static void ParseArchetype(JsonElement item, JsonDocument frJsonDoc, StreamWriter writer)
{
}

// génération des dons d'archétypes de classes
//count = await GenerateData(DataPack.Feats, ParseFeat, e => e.GetPropertyString("featType") == "archetype");