namespace DevFreela.Core.Entities;

public class Pagination
{
    public Pagination(string search, int size, int page)
    {
        Search = search;
        Size = size;
        Page = page;
    }

    public string? Search { get; set; }
    public int Size { get; set; }
    public int Page { get; set; }
}