using Domain.Entities;
using Domain.Entities.Post;
using Domain.Entities.User;
using Domain.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Infrastructure.Data;

public class DataContext : IdentityDbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.HasPostgresEnum<Gender>();
        modelBuilder.HasPostgresEnum<Active>();
        modelBuilder.Entity<FollowingRelationShip>()
            .HasOne<User>(u => u.User)
            .WithMany(f => f.FollowingRelationShips)
            .HasForeignKey(u => u.UserId);
        modelBuilder.Entity<User>()
            .HasIndex(u => u.UserName)
            .IsUnique();
        modelBuilder.Entity<UserProfile>()
            .HasIndex(u => u.UserId)
            .IsUnique();
        modelBuilder.Entity<Category>()
            .HasIndex(u => u.CategoryName)
            .IsUnique();

        modelBuilder.Entity<PostLike>()
            .ToTable(p => p.HasCheckConstraint("PostLikes", @" ""LikeCount"" >= 0"));
        
        base.OnModelCreating(modelBuilder);
    }

    public new DbSet<User> Users { get; set; }
    public DbSet<Story> Stories { get; set; }
    public DbSet<StoryLike> StoryLikes { get; set; }
    public DbSet<StoryUser> StoryUsers { get; set; }
    public DbSet<StoryView> StoryViews { get; set; }
    public DbSet<StoryStat> StoryStats { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<PostCategory> PostCategories { get; set; }
    public DbSet<PostComment> PostComments { get; set; }
    public DbSet<PostFavorite> PostFavorites { get; set; }
    public DbSet<PostLike> PostLikes { get; set; }
    public DbSet<ExternalAccount> ExternalAccounts { get; set; }
    public DbSet<FollowingRelationShip> FollowingRelationShips { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<UserSetting> UserSettings { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<PostUserLike> PostUserLikes { get; set; }
    public DbSet<PostView> PostViews { get; set; }
    public DbSet<PostViewUser> PostViewUsers { get; set; }
    public DbSet<PostCommentLike> PostCommentLikes { get; set; }
    public DbSet<ListOfUserCommentLike> ListOfUserCommentLikes { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<PostFavoriteUser> PostFavoriteUsers { get; set; }
    public DbSet<SearchHistory> SearchHistories { get; set; } = null!;
    public DbSet<UserSearchHistory> UserSearchHistories { get; set; } = null!;
}