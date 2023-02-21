namespace dotnetbook.Models;

public class Book : Model
{
    public string? Name { get; set; }
    public IEnumerable<Part> Parts { get; set; } = Array.Empty<Part>();
}