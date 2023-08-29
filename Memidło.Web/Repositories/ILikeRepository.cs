using Memidło.Web.Models.Domain;

namespace Memidło.Web.Repositories
{
    public interface ILikeRepository
    {
        Task<int> GetTotalLikesForMem(int id);

        Task AddLikeForMem(int memId, Guid userId);

        Task<IEnumerable<Like>> GetLikesForMem(int memId);

        Task<List<Like>> GetAllLikes();
    }
}
