namespace NewsRESTapi.Models;

/// <summary>
/// Шаблон модели новости для Б.Д
/// </summary>
public class News
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Link { get; set; }
    public string? LastBuildDate { get; set; }
}