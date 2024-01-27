using Microsoft.EntityFrameworkCore;
using FrameWorkRHP_Mono.Core.Models.EF;
using FrameWorkRHP_Mono.Infrastructure.Context.Builder;


namespace FrameWorkRHP_Mono.Infrastructure.Context
{
    public class AppsDbContext : DbContext
    {
        //Constructor calling the Base DbContext Class Constructor
        public AppsDbContext(DbContextOptions<AppsDbContext> options) : base(options)
        {
        }
        //OnConfiguring() method is used to select and configure the data source
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //We will store the connection string in AppSettings.json file instead of hard coding here
        }


        public virtual DbSet<Mrole> Mroles { get; set; }

        public virtual DbSet<Muser> Musers { get; set; }

        public virtual DbSet<Muserrole> Muserroles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ClsMroleBuilder.Builder(ref modelBuilder);
            ClsMuserBuilder.Builder(ref modelBuilder);
            ClsMuserroleBuilder.Builder(ref modelBuilder);
        }
    } 
}
