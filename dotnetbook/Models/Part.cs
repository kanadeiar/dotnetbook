namespace dotnetbook.Models;

public class Part : Model
{
    public string? Name { get; set; }
    public IEnumerable<Item> Items { get; set; } = Array.Empty<Item>();
}