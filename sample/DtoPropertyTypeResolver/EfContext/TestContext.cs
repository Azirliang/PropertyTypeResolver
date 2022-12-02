using DtoPropertyTypeResolver.Entity;
using Microsoft.EntityFrameworkCore;

namespace DtoPropertyTypeResolver.EfContext
{
    public class TestContext : DbContext
    {
        public TestContext(DbContextOptions dbContextOptions):base(dbContextOptions) { }

        public DbSet<TemplateEntity>? Templates { get; set; }

    }
}
