#load "include.csx"

using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;

var data = new Dictionary<string, Dictionary<string, StatusEntry>>();

foreach (var file in Directory.GetFiles("../_ext/data-fr", "*.json"))
{
    var systemId = Path.GetFileNameWithoutExtension(file);
    var group = DataGroup.All.FirstOrDefault(g => g.FoundrySystemName == systemId);
    if (group == null)
    {
        WriteLine($"Fichier de données {systemId} non supporté, ignoré");
        continue;
    }

    JsonDocument dataDoc;
    using (var input = File.OpenRead(file))
    {
        dataDoc = JsonDocument.Parse(input);
    }

    using (dataDoc)
    {
        if (!data.TryGetValue(group.Id, out var ids))
        {
            ids = data[group.Id] = new Dictionary<string, StatusEntry>();
        }

        foreach (var item in dataDoc.RootElement.EnumerateArray().OrderBy(x => x.GetProperty("_id").GetString()))
        {
            var id = item.GetProperty("_id").GetString();
            var enName = item.GetProperty("name").GetString();
            var frName = item.GetProperty("translations").GetProperty("fr").GetPropertyOrDefault("name")?.GetString();

            ids.Add(id, new StatusEntry(frName, enName));
        }
    }
}

Directory.CreateDirectory("../_data");
using (var jsonFile = File.Create("../_data/ids.json"))
{
    await System.Text.Json.JsonSerializer.SerializeAsync(jsonFile, data, new JsonSerializerOptions() { WriteIndented = true });
}
