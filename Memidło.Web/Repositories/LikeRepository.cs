using Memidło.Web.Data;
using Memidło.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Memidło.Web.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly MemidłoDbContext memidłoDbContext;

        public LikeRepository(MemidłoDbContext memidłoDbContext)
        {
            this.memidłoDbContext = memidłoDbContext;
        }

        public async Task AddLikeForMem(int memId, Guid userId)
        {
            var like = new Like
            {
                MemId = memId,
                UserId = userId
            };

            await memidłoDbContext.Likes.AddAsync(like);
            await memidłoDbContext.SaveChangesAsync();
        }

        public async Task<int> GetTotalLikesForMem(int id)
        {
            return await memidłoDbContext.Likes.CountAsync(x => x.MemId == id);
        }

        public async Task<IEnumerable<Like>> GetLikesForMem(int memId)
        {
            return await memidłoDbContext.Likes.Where(x=>x.MemId== memId).ToListAsync();
        }

        public async Task<List<Like>> GetAllLikes()
        {
            return await memidłoDbContext.Likes.ToListAsync();
        }
    }
}
