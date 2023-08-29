using Memidło.Web.Models.Domain;

namespace Memidło.Web.Repositories
{
    public interface ITagRespository
    {
        Task<IEnumerable<Tag>> GetAllAsync();
    }
}
