namespace dotnetbook.Services;

public class CoreBookService : ICoreBookService
{
    private readonly HttpClient _httpClient;
    private readonly ICollection<Book> _books;
    private readonly ICollection<Item> _items;
    private readonly IDictionary<string, MarkupString> _htmls = new Dictionary<string, MarkupString>(); 
    public CoreBookService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _books = StartData.GetBooks();
        var items = _books.SelectMany(x => x.Parts).SelectMany(x => x.Items);
        _items = items.ToArray();
        ThreadPool.QueueUserWorkItem(async _ => {
            var pipeline = new Markdig.MarkdownPipelineBuilder().UseBootstrap().UseAdvancedExtensions().Build();
            foreach (var item in _items)
            {
                var text = await _httpClient.GetStringAsync($"files/core/{item.Path}");
                var html = (MarkupString)Markdown.ToHtml(text, pipeline);
                _htmls.Add(item.Path!, html);
            }
        });
    }
    public Book? GetBook(int id)
    {
        var book = _books.FirstOrDefault(x => x.Id == id);
        return book;
    }
    public bool GetAnyItem(int id)
    {
        var found = _items.Any(x => x.Id == id);
        return found;
    }
    public Item? GetItem(int id)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);
        return item;
    }
    public async Task<MarkupString?> GetHtml(string path)
    {
        if (_htmls.TryGetValue(path, out MarkupString html))
        {
            return html;
        }
        var pipeline = new Markdig.MarkdownPipelineBuilder().UseBootstrap().UseAdvancedExtensions().Build();
        var text = await _httpClient.GetStringAsync($"files/core/{path}");
        return (MarkupString)Markdown.ToHtml(text, pipeline);
    }
}