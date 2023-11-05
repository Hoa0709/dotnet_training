using Microsoft.EntityFrameworkCore;
using app.Models;

namespace app.Connects
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            // ../
        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder){
            base.OnConfiguring(builder);
        }
        protected override void OnModelCreating(ModelBuilder builder){
            base.OnModelCreating (builder); 
        }

        public DbSet<ProgramInfo> programs {set; get;}
        public DbSet<News> news {set; get;}
        public DbSet<Location> locations {set; get;}
        public DbSet<Support> supports {set; get;}
        public DbSet<Ticket> tickets {set; get;}
        public DbSet<Artist> artists {set; get;}
        public DbSet<BookTicket> bookTickets {set; get;}
    }
}