using BlogApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Context;

public class BlogContext : DbContext
{
        public BlogContext()
        {
        }

        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            // modelBuilder.Entity<Item>()
            //     .HasOne(a => a.Item_Code)
            //     .WithMany()
            //     .HasForeignKey(i=>i.ItemCode)
            //     .IsRequired();
            //
            // modelBuilder.Entity<Item>()
            //     .Navigation(i => i.Item_Code)
            //     .UsePropertyAccessMode(PropertyAccessMode.Property)
            //     .AutoInclude();
            
            // modelBuilder.Entity<OrderItem>().HasKey(oi => new { oi.IdOrder, oi.IdItem });
            //
            // modelBuilder.Entity<OrderItem>()
            //     .HasOne<Item>(sc => sc.Item)
            //     .WithMany()
            //     .HasForeignKey(oi => oi.IdItem);
            //
            // modelBuilder.Entity<OrderItem>().Navigation(oi => oi.Item)
            //     .UsePropertyAccessMode(PropertyAccessMode.Property).AutoInclude();
            // modelBuilder.Entity<OrderItem>().Navigation(oi => oi.Order)
            //     .UsePropertyAccessMode(PropertyAccessMode.Property).AutoInclude();
            //
            // modelBuilder.Entity<Order>().Navigation(o => o.OrderItems)
            //     .UsePropertyAccessMode(PropertyAccessMode.Property).AutoInclude();
            //
            // modelBuilder.Entity<OrderItem>()
            //     .HasOne<Order>(oi => oi.Order)
            //     .WithMany(o => o.OrderItems)
            //     .HasForeignKey(oi => oi.IdOrder);

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Admin> Admins { get; set; }

        public virtual DbSet<Blog> Blogs { get; set; }

        public virtual DbSet<Notification> Notifications { get; set; }

        public virtual DbSet<NotificationType> NotificationTypes { get; set; }

        public virtual DbSet<Post> Posts { get; set; }

        public virtual DbSet<PostTag> PostTags { get; set; }

        public virtual DbSet<Subscription> Subscriptions { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }

        public virtual DbSet<User> Users { get; set; }
}