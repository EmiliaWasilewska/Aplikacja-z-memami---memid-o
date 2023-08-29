using Memidło.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Memidło.Web.Data
{
    public class MemidłoDbContext : DbContext
    {
        public MemidłoDbContext(DbContextOptions<MemidłoDbContext>options) : base(options)
        {
        }

        public DbSet<Mem> Mems { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }

    }
}
