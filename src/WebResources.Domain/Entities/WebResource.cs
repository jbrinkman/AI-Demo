namespace WebResources.Domain.Entities
{
    public class WebResource
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Category { get; set; } // Will ensure validation in Application Layer
        public string Author { get; set; }
        public DateTimeOffset PublishDate { get; set; }
    }
}