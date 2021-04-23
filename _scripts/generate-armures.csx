#load "include.csx"

// Génère les pages des armures
//
// https://gitlab.com/pathfinder-fr/pf2-data-fr/-/raw/master/equipment-srd.json

using System.Text.Json;

var count = await GenerateData(DataPack.Equipments, ParseArmor, o => o.GetPropertyString("type") == "armor", "armures");

WriteLine($"Génération terminée : {count} armures traitées");

static void ParseArmor(JsonElement item, JsonDocument frJsonDoc, StreamWriter writer)
{
    writer.WriteLine($"  type: {item.GetPropertyString("armorType")}");
    writer.WriteLine($"  bonus: {item.GetPropertyInt32("armor")}");
    writer.WriteLine($"  level: {item.GetPropertyInt32("level")}");

    // Prix TODO : Gérer la traduction en français
    writer.WriteLine($"  prix: {item.GetPropertyString("price")}");

    writer.WriteYamlProperty($"  dexMax", item.GetPropertyInt32("armorMaxDex"));
    writer.WriteYamlProperty($"  malusTests", item.GetPropertyInt32("armorCheck"));
    writer.WriteYamlProperty($"  force", item.GetPropertyInt32("armorStrength"));
    writer.WriteYamlProperty($"  encombrement", item.GetPropertyIntOrString("armorEquippedBulk"));
    writer.WriteYamlProperty($"  groupe", item.GetPropertyString("group"));;

    // Traits
    var traits = item.GetProperty("traits").EnumerateArray().Select(x => x.GetString()).ToArray();
    var traitsFr = traits.Select(l => frJsonDoc.RootElement.GetProperty("PF2E").GetPropertyOrDefault("Trait" + FirstCharUpper(l))?.GetString());    
    writer.WriteArray("  traits:", traitsFr);
}
