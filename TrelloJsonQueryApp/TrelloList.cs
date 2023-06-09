public class TrelloList
{
    public string Id { get; set; }
    public string Name { get; set; }
    public bool Closed { get; set; }
    public List<TrelloCard> Cards { get; set; } = new List<TrelloCard>();
}
