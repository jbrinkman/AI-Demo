using Microsoft.EntityFrameworkCore;
using WebResources.Application.DTOs;
using WebResources.Application.Interfaces;
using WebResources.Domain.Entities;
using WebResources.Infrastructure.Data;

namespace WebResources.Infrastructure.Repositories
{
    public class WebResourceRepository : IWebResourceRepository
    {
        private readonly WebResourceDbContext _context;

        public WebResourceRepository(WebResourceDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WebResourceDto>> GetAllAsync()
        {
            return await _context.WebResources
                                 .Select(w => new WebResourceDto
                                 {
                                     Id = w.Id,
                                     Url = w.Url,
                                     Title = w.Title,
                                     Category = w.Category,
                                     Author = w.Author,
                                     PublishDate = w.PublishDate
                                 })
                                 .ToListAsync();
        }

        public async Task<WebResourceDto> GetByIdAsync(int id)
        {
            var resource = await _context.WebResources.FindAsync(id);
            return resource == null ? null : new WebResourceDto
            {
                Id = resource.Id,
                Url = resource.Url,
                Title = resource.Title,
                Category = resource.Category,
                Author = resource.Author,
                PublishDate = resource.PublishDate
            };
        }

        public async Task AddAsync(WebResourceDto webResourceDto)
        {
            var entity = new WebResource
            {
                Url = webResourceDto.Url,
                Title = webResourceDto.Title,
                Category = webResourceDto.Category,
                Author = webResourceDto.Author,
                PublishDate = webResourceDto.PublishDate
            };

            _context.WebResources.Add(entity);
            await _context.SaveChangesAsync();

            webResourceDto.Id = entity.Id;  // To return the ID after inserting
        }

        public async Task UpdateAsync(WebResourceDto webResourceDto)
        {
            var entity = await _context.WebResources.FindAsync(webResourceDto.Id);
            if (entity != null)
            {
                entity.Url = webResourceDto.Url;
                entity.Title = webResourceDto.Title;
                entity.Category = webResourceDto.Category;
                entity.Author = webResourceDto.Author;
                entity.PublishDate = webResourceDto.PublishDate;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.WebResources.FindAsync(id);
            if (entity != null)
            {
                _context.WebResources.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }

}
