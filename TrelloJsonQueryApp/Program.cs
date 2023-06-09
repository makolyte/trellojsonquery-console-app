
using System.Text.Json;


Console.WriteLine("Version 1 - Loads Trello JSON from hardcoded path, gets lists and cards, then displays card names for list you pick");
Console.WriteLine();

var trelloJsonPath = @"C:\temp\trello.json";

if (!File.Exists(trelloJsonPath))
{
    Console.WriteLine($"File doesn't exist: {trelloJsonPath}");
    return;
}


Console.WriteLine("Loading Trello JSON");
//1 - Load Trello JSON into a JsonDocument (DOM)
var jsonOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

using var jsonStream = File.OpenRead(trelloJsonPath);
using var jsonDoc = JsonDocument.Parse(jsonStream);

//2 - Get all lists and cards
var allLists = jsonDoc.RootElement.GetProperty("lists").Deserialize<List<TrelloList>>(jsonOptions);
var allCards = jsonDoc.RootElement.GetProperty("cards").Deserialize<List<TrelloCard>>(jsonOptions);

//3 - Filter out closed (archived?) lists and cards
var activeLists = allLists.Where(t => !t.Closed).ToList();
var activeCards = allCards.Where(t => !t.Closed).ToList();

//4 - Link up cards to lists
var listMap = activeLists.ToDictionary(list => list.Id);

foreach(var card in activeCards)
{
    if (listMap.TryGetValue(card.IdList, out TrelloList list))
    {
        list.Cards.Add(card);
    }

}

//5 - Let user pick list to show cards for
Console.WriteLine($"Found {allLists.Count} list(s). {activeLists.Count} active, {allLists.Count - activeLists.Count} closed.");
Console.WriteLine();

Console.WriteLine($"Here are the {activeLists.Count()} active lists: ");

foreach(var list in activeLists)
{
    Console.WriteLine(list.Name);
}


var listMapByName = activeLists.ToDictionary(list => list.Name); //This'll blow up if the names aren't unique
while (true)
{

    Console.WriteLine();
    Console.Write("Show cards for list (enter exact name): ");

    var queryListName = Console.ReadLine();

    if (listMapByName.TryGetValue(queryListName, out TrelloList trelloList))
    {
        var fileName = $@"C:\temp\trello-query-{DateTime.Now.ToFileTime()}.csv";
        File.WriteAllLines(fileName, trelloList.Cards.Select(c => c.Name));
        Console.WriteLine($"Output query results to: {fileName}");
    }
    else
    {
        Console.WriteLine("Invalid list name. Try again.");
    }

}
