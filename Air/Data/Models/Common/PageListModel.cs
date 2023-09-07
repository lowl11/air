namespace Air.Data.Models.Common;

public class PageListModel<T>
{
    
    [JsonPropertyName("max_pages")]
    public int MaxPages { get; set; }
    
    [JsonPropertyName("count")]
    public int Count { get; set; }
    
    [JsonPropertyName("list")]
    public List<T> List { get; set; }

}