using Memidło.Web.Data;
using Memidło.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Memidło.Web.Repositories
{
    public class TagRespository : ITagRespository
    {
        private readonly MemidłoDbContext memidłoDbContext;

        public TagRespository(MemidłoDbContext memidłoDbContext)
        {
            this.memidłoDbContext = memidłoDbContext;
        }
        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            var tags =await memidłoDbContext.Tags.ToListAsync();

            return tags.DistinctBy(x => x.Name.ToLower()); ;
        }
    }
}
