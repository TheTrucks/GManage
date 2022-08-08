using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GManage.Models.Auth
{
    [Table("user", Schema = "auth")]
    public class User
    {
        public User(int id, string login, string passHash)
        {
            Id = id;
            Login = login;
            PassHash = passHash;
        }

        public int Id { get; private set; }
        [Required]
        public string Login { get; private set; }
        [Required]
        [Column("pass_hash")]
        public string PassHash { get; set; }

        public IList<Session> SessionList { get; set; } = new List<Session>();
        public IList<Resource> AvailableResources { get; set; } = new List<Resource>();
        public IList<ResourceUser> ResourceUsers = new List<ResourceUser>();
    }

    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> CBuild)
        {
            CBuild.HasKey(x => x.Id);
            CBuild
                .HasMany(x => x.SessionList)
                .WithOne(x => x.User);
            CBuild
                .Property(x => x.Id)
                .HasDefaultValueSql("nextval('\"auth.user_id_seq\"')");
        }
    }
}
