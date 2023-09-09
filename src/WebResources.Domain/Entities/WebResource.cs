namespace WebResources.Domain.Entities
{
    public class WebResource
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTimeOffset PublishDate { get; set; } = DateTimeOffset.MinValue;
    }
}