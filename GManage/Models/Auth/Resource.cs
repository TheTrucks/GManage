using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GManage.Models.Auth
{
    [Table("service", Schema = "auth")]
    public class Resource
    {
        public int Id { get; private set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public IList<User> AvailableUsers { get; set; } = new List<User>();
        public IList<ResourceUser> ResourceUsers { get; set; } = new List<ResourceUser>();
    }

    public class ResourceConfig : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> SBuild)
        {
            SBuild
                .HasKey(x => x.Id);
            SBuild
                .HasMany(x => x.AvailableUsers)
                .WithMany(x => x.AvailableResources)
                .UsingEntity<ResourceUser>(
                    RU => RU
                    .HasOne(x => x.User)
                    .WithMany(x => x.ResourceUsers)
                    .HasForeignKey(x => x.UserId),
                    RU => RU
                    .HasOne(x => x.Resource)
                    .WithMany(x => x.ResourceUsers)
                    .HasForeignKey(x => x.ResourceId),
                    RU => RU
                    .HasKey(composite => new { composite.UserId, composite.ResourceId })
            );
            SBuild
                .Property(x => x.Id)
                .HasDefaultValueSql("nextval('\"auth.resource_id_seq\"')");
        }
    }
}
