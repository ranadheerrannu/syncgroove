using Microsoft.EntityFrameworkCore;
using SyncGroove.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SyncGroove
{
    public class SyncGrooveDbContext : DbContext
    {
        public SyncGrooveDbContext(DbContextOptions<SyncGrooveDbContext> options)
            : base(options)
        {
        }

        public DbSet<WorkItemMappings> WorkItemMappings { get; set; }
        public DbSet<CommentsMapping> CommentsMappings { get; set; }
    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkItemMappings>(entity =>
            {
                entity.HasKey(m => new { m.AzureId,m.JiraId});

                entity.Property(m => m.AzureId)
                  .IsRequired();

                entity.Property(m => m.JiraId)
                  .IsRequired();
            });


           
        }
    }
}