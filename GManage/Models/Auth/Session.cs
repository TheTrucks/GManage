using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GManage.Models.Auth
{
    [Table("session", Schema = "auth")]
    public class Session
    {
        public Session(int userId, string sessionId, string clientAddress, DateTime lastAccessed)
        {
            UserId = userId;
            SessionId = sessionId;
            ClientAddress = clientAddress;
            LastAccessed = lastAccessed;
        }

        [Required]
        [Column("user_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; private set; }
        public User User { get; private set; } = null!;

        [Required]
        [Column("session_id")]
        public string SessionId { get; private set; }

        [Required]
        [Column("client_addr")]
        public string ClientAddress { get; private set; }

        [Required]
        [Column("accessed_at")]
        public DateTime LastAccessed { get; set; }
    }

    public class SessionConfig : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> SBuild)
        {
            SBuild
                .HasKey(composite => new { composite.UserId, composite.SessionId });
            SBuild.Property(x => x.ClientAddress);
            SBuild
                .Property(x => x.LastAccessed)
                .HasColumnType("timestamp without timezone");
            SBuild
                .HasOne(x => x.User)
                .WithMany(x => x.SessionList)
                .HasForeignKey(x => x.UserId);
        }
    }
}
