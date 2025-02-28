namespace api.Data
{
    using api.Models;
    using Microsoft.EntityFrameworkCore;


    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Stock> Stocks { get; set; }
        
        public DbSet<Comment> Comments { get; set; }
    }
}
