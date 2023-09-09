using WebResources.Application.DTOs;

namespace WebResources.Application.Interfaces
{
    public interface IWebResourceRepository
    {
        Task<IEnumerable<WebResourceDto>> GetAllAsync();
        Task<WebResourceDto> GetByIdAsync(int id);
        Task AddAsync(WebResourceDto webResource);
        Task UpdateAsync(WebResourceDto webResource);
        Task DeleteAsync(int id);
    }
}