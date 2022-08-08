using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GManage.Models.Auth
{
    [Table("resource_user", Schema = "auth")]
    public class ResourceUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ResourceId { get; set; }
        public Resource Resource { get; set; } = null!;
    }
}
