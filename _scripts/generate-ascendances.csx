#load "include.csx"

// Génère les pages des ascendances
//
// https://gitlab.com/pathfinder-fr/pf2-data-fr/-/blob/master/ancestries.json

using System.Text.Json;

var count = await GenerateData(DataPack.Ancestries, ParseAncestry);

WriteLine($"Génération terminée : {count} ascendances traitées");

static void ParseAncestry(JsonElement item, JsonDocument frJsonDoc, StreamWriter writer)
{
    var hp = item.GetProperty("hp").GetInt32();
    writer.WriteLine($"  pv: {hp}");

    // liste des langues en anglais
    var languages = item.GetProperty("languages").EnumerateArray().Select(x => x.GetString()).ToArray();

    // liste des langues traduites en FR d'après le glossaire
    var languagesFr = languages
        .Select(l =>
        {
            // le nom de l'entrée du glossaire = Language + Nom anglais avec la première lettre en majuscules
            var itemName = "Language" + l.FirstCharUpper();

            // on récupère l'entrée du glossaire FR que l'on renvoie
            return frJsonDoc.RootElement.GetProperty("PF2E").GetPropertyOrDefault(itemName)?.GetString();
        });

    // écriture de l'entête langues avec la liste des langues françaises
    writer.WriteArray("  langues:", languagesFr);

    languages = item.GetProperty("additionalLanguages").EnumerateArray().Select(x => x.GetString()).ToArray();

    languagesFr = languages
        .Select(l =>
        {
            // le nom de l'entrée du glossaire = Language + Nom anglais avec la première lettre en majuscules
            var itemName = "Language" + FirstCharUpper(l);

            // on récupère l'entrée du glossaire FR que l'on renvoie
            return frJsonDoc.RootElement.GetProperty("PF2E").GetPropertyOrDefault(itemName)?.GetString();
        });

    // écriture de l'entête langues avec la liste des langues françaises
    writer.WriteArray("  languesComplémentaires:", languagesFr);
}
