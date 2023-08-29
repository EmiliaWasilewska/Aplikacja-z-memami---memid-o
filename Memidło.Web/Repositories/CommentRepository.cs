using Memidło.Web.Data;
using Memidło.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Memidło.Web.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly MemidłoDbContext memidłoDbContext;

        public CommentRepository(MemidłoDbContext memidłoDbContext)
        {
            this.memidłoDbContext = memidłoDbContext;
        }
        public async Task<Comment> AddAsync(Comment comment)
        {
            await memidłoDbContext.Comments.AddAsync(comment);
            await memidłoDbContext.SaveChangesAsync();

            return comment;
        }

       
        public async Task<IEnumerable<Comment>> GetAllForMemAsync(int id)
        {
            return await memidłoDbContext.Comments.Where(x => x.MemId == id).ToListAsync();
        }
    }
}
