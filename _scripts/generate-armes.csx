#load "include.csx"

// Génère les pages des armes
//
// https://gitlab.com/pathfinder-fr/pf2-data-fr/-/raw/master/equipment-srd.json

using System.Text.Json;

var count = await GenerateData(DataPack.Equipments, ParseWeapon, o => o.GetPropertyString("type") == "weapon", "armes");

WriteLine($"Génération terminée : {count} armes traitées");

static void ParseWeapon(JsonElement item, JsonDocument frJsonDoc, StreamWriter writer)
{
    writer.WriteLine($"  type: {item.GetPropertyString("weaponType")}");
    writer.WriteLine($"  niveau: {item.GetPropertyInt32("level")}");

    // Prix TODO : Gérer la traduction en français
    writer.WriteLine($"  prix: {item.GetPropertyString("price")}");

    writer.WriteYamlProperty($"  rareté", item.GetPropertyString("rarity"));
    writer.WriteYamlProperty($"  groupe", FromGlossaire(frJsonDoc, item.GetPropertyString("group"), "WeaponGroup"));
    writer.WriteYamlProperty($"  dommagesNb", item.GetProperty("damage").GetPropertyInt32("dice"));
    writer.WriteYamlProperty($"  dommagesDé", item.GetProperty("damage").GetPropertyString("die"));
    writer.WriteYamlProperty($"  dommagesType", FromGlossaire(frJsonDoc, item.GetProperty("damage").GetPropertyString("type"), "DamageType"));
    writer.WriteYamlProperty($"  poids", item.GetPropertyString("weight"));

    // Traits
    var traits = item.GetProperty("traits").EnumerateArray().Select(x => x.GetString()).ToArray();
    var traitsFr = traits.Select(x => FromGlossaire(frJsonDoc, x, "Trait", specialCases: specialTraits));
    writer.WriteArray("  traits:", traitsFr);
}

static Dictionary<string, string> specialTraits = new Dictionary<string, string>() {
    {"modular-b-P-or-s", "Modular"},
};

static string FromGlossaire(JsonDocument frJsonDoc, string name, string prefix, string parent = "PF2E", bool allowMissing = false, IDictionary<string, string> specialCases = null)
{
    if (string.IsNullOrEmpty(name))
        return name;

    // cas spéciaux
    if (specialCases != null && specialCases.TryGetValue(name, out var specalCaseName))
    {
        name = specalCaseName;
    }
    else
    {
        name = name
            .ReplaceRegex(@"-\w", m => m.Value.ToUpper())
            .Replace("-", "")
            .Replace(" ", "")
            .FirstCharUpper();
    }

    var propertyName = prefix + name;

    var parentProperty = frJsonDoc.RootElement.GetProperty(parent);
    if (!parentProperty.TryGetProperty(propertyName, out var property))
    {
        if (!allowMissing)
        {
            throw new ArgumentOutOfRangeException(nameof(name), name, $"Il n'existe pas de propriété {propertyName} dans le noeud JSON {parent} du glossaire");
        }

        return null;
    }

    return property.GetString();
}

static string ReplaceRegex(this string value, string pattern, string replacement, System.Text.RegularExpressions.RegexOptions options = System.Text.RegularExpressions.RegexOptions.None)
{
    return System.Text.RegularExpressions.Regex.Replace(value, pattern, replacement, options);
}

static string ReplaceRegex(this string value, string pattern, System.Text.RegularExpressions.MatchEvaluator evaluator, System.Text.RegularExpressions.RegexOptions options = System.Text.RegularExpressions.RegexOptions.None)
{
    return System.Text.RegularExpressions.Regex.Replace(value, pattern, evaluator, options);
}