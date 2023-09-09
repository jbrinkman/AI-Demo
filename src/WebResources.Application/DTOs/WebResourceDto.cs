using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebResources.Application.DTOs
{
    public class WebResourceDto
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTimeOffset PublishDate { get; set; } = DateTimeOffset.MinValue;
    }


}
