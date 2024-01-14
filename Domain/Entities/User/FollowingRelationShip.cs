using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.User;

[Index("UserId", "FollowingId", IsUnique = true)]
public class FollowingRelationShip
{
    [Key]
    public int FollowingRelationShipId { get; set; }

    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
    public string FollowingId { get; set; } = null!;
    public User Following { get; set; } = null!;
    public DateTime DateFollowed { get; set; }
}