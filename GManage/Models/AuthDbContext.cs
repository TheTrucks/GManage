using Microsoft.EntityFrameworkCore;

namespace GManage.Models
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> opts) : base(opts) { }

        public DbSet<Auth.User> Users => Set<Auth.User>();
        public DbSet<Auth.Session> Sessions => Set<Auth.Session>();
        public DbSet<Auth.Resource> Resources => Set<Auth.Resource>();

        protected override void OnModelCreating(ModelBuilder MBuild)
        {            
            MBuild.HasSequence<int>("auth.user_id_seq").StartsAt(1).IncrementsBy(1);
            MBuild.HasSequence<int>("auth.resource_id_seq").StartsAt(1).IncrementsBy(1);

            new Auth.UserConfig().Configure(MBuild.Entity<Auth.User>());
            new Auth.SessionConfig().Configure(MBuild.Entity<Auth.Session>());
            new Auth.ResourceConfig().Configure(MBuild.Entity<Auth.Resource>());
        }
    }
}
