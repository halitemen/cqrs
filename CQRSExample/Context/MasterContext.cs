using CQRSExample.Dto;
using Microsoft.EntityFrameworkCore;
using System;

namespace CQRSExample.Context
{
    public class MasterContext : DbContext
    {
        public MasterContext()
        {
        }

        public MasterContext(DbContextOptions<MasterContext> options)
        {

        }

        public DbSet<CommentDto> CommentDto { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommentDto>().HasKey(x => x.Id);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL(Environment.GetEnvironmentVariable("MYSQL_URI"));
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
