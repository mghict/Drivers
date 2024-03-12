using Moneyon.Common.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("UsersTokens")]
public class UsersToken : Entity<long>
{
    public string Token { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; } = System.DateTime.UtcNow;
    public bool IsExpire { get; set; } = false;

    public long UserId { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }
}