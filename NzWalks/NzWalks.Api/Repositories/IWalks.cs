using NzWalks.Api.Models.Domain;

namespace NzWalks.Api.Repositories
{
    public interface IWalks
    {
        Task<List<Walks>> GetWalks(string filterOn, string filterValue, int pageSize, int pageNumber); 
        Task<Walks> AddWalk(Walks walks);
        Task<Walks?> UpdateWalk(Guid id, Walks walks);
        Task<Walks?> DeleteWalk(Guid id);
        Task<Walks?> GetWalksById(Guid id);
    }
}
