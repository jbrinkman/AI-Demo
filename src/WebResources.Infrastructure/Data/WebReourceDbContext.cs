
using Microsoft.EntityFrameworkCore;
using WebResources.Domain.Entities;

namespace WebResources.Infrastructure.Data
{
    public class WebResourceDbContext : DbContext
    {
        public WebResourceDbContext(DbContextOptions<WebResourceDbContext> options) : base(options) { }

        public DbSet<WebResource> WebResources { get; set; }
    }
}