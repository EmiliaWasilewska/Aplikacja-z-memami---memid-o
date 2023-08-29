using Memidło.Web.Data;
using Memidło.Web.Models.Domain;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;
using System.Linq;

namespace Memidło.Web.Repositories
{
    public class MemRepository : IMemRepository
    {
        private readonly MemidłoDbContext memidłoDbContext;

        public MemRepository(MemidłoDbContext memidłoDbContext)
        {
            this.memidłoDbContext = memidłoDbContext;
        }
        public async Task<Mem> AddAsync(Mem mem)
        {
            await memidłoDbContext.Mems.AddAsync(mem);
            await memidłoDbContext.SaveChangesAsync();
            return mem;
        }

        public async Task<IEnumerable<Mem>> GetAllAsync()
        {
            return await memidłoDbContext.Mems.Include(nameof(Mem.Tags)).Include(nameof(Mem.Likes)).ToListAsync();
        }

        public async Task<IEnumerable<Mem>> GetAllAsync(string tagName)
        {
            return await memidłoDbContext.Mems.Include(nameof(Mem.Tags)).Where(x => x.Tags.Any(x => x.Name == tagName)).ToListAsync();
        }

        public async Task<Mem> GetAsync(int id)
        {
            return await memidłoDbContext.Mems.Include(nameof(Mem.Tags)).FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<Mem> MoveToMainAsync(int memID)
        {
            var existingMem = await memidłoDbContext.Mems.FirstOrDefaultAsync(x => x.Id == memID);
            if (existingMem != null)
            {
                existingMem.Main = true;
                existingMem.PublishDate = DateTime.Now;
            }

            await memidłoDbContext.SaveChangesAsync();

            return existingMem;
        }

        public async Task<Mem> UpdateAsync(Mem mem)
        {
            var existingMem = await memidłoDbContext.Mems.Include(nameof(Mem.Tags)).FirstOrDefaultAsync(x => x.Id == mem.Id);

            if (existingMem != null)
            {
                existingMem.PageTitle = mem.PageTitle;
                existingMem.Heading = mem.Heading;
                existingMem.Content = mem.Content;
                existingMem.FeaturedImageUrl = mem.FeaturedImageUrl;
                existingMem.Author = mem.Author;
                existingMem.Main = mem.Main;
                existingMem.PublishDate = mem.PublishDate;

                if (mem.Tags != null && mem.Tags.Any())
                {
                    memidłoDbContext.Tags.RemoveRange(existingMem.Tags);
                }

                mem.Tags.ToList().ForEach(x => x.MemId = existingMem.Id);
                await memidłoDbContext.Tags.AddRangeAsync(mem.Tags);
            }

            await memidłoDbContext.SaveChangesAsync();
            return existingMem;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingMem = await memidłoDbContext.Mems.FindAsync(id);

            if (existingMem != null)
            {
                memidłoDbContext.Mems.Remove(existingMem);
                await memidłoDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<int> CountMainAsync()
        {
            return await memidłoDbContext.Mems.CountAsync(x => x.Main);
        }

        public async Task<int> CountWaitingRoomAsync()
        {
            return await memidłoDbContext.Mems.CountAsync(x => !x.Main);
        }

        public async Task<int> CountTaggedAsync(string tagName)
        {
            return (await GetAllAsync(tagName)).Count();
        }

        public async Task<Mem?> GetRandom()
        {
            return await memidłoDbContext.Mems
                .Include(x => x.Tags)
                .Include(x => x.Likes)
                .OrderBy(x=> Guid.NewGuid())
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Mem>> GetPaginated(int pageSize, int pageNumber, bool? isMain = null)
        {
            var baseQuery = memidłoDbContext.Mems.Include(nameof(Mem.Tags)).Include(nameof(Mem.Likes));

            if(isMain is not null) baseQuery = baseQuery.Where(x => x.Main == isMain.Value);

            return await baseQuery.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
