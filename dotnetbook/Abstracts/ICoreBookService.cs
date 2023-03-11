namespace dotnetbook.Abstracts;

public interface ICoreBookService
{
    public Book? GetBook(int id);
    public bool GetAnyItem(int id);
    public Item? GetItem(int id);
    public Task<MarkupString?> GetHtml(string path);
}