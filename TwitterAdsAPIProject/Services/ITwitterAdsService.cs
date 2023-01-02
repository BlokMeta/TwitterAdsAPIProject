using TwitterAdsAPIProject.Models;

namespace TwitterAdsAPIProject.Services
{
    public interface ITwitterAdsService
    {
        Task<List<LineItem>> GetLineItems();
    }
}