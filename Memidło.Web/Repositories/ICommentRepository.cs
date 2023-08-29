using Memidło.Web.Models.Domain;

namespace Memidło.Web.Repositories
{
    public interface ICommentRepository
    {
        Task<Comment> AddAsync(Comment comment);

        Task<IEnumerable<Comment>> GetAllForMemAsync(int id);

       
    }
}
