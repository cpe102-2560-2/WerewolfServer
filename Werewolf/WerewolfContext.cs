
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DNWS.Werewolf 
{
    /// <summary>
    /// 
    /// </summary>
    public partial class WerewolfContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<ActionRole> ActionRoles { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        private static bool _created = false;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ActionRole>()
                .HasKey(ar => new {ar.ActionId, ar.RoleId});
            modelBuilder.Entity<ActionRole>()
                .HasOne(ar => ar.Action) 
                .WithMany(r => r.ActionRoles)
                .HasForeignKey(ar => ar.ActionId);
            modelBuilder.Entity<ActionRole>()
                .HasOne(ar => ar.Role)
                .WithMany(a => a.ActionRoles)
                .HasForeignKey(ar => ar.RoleId);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=werewolf.db");
        }
        public WerewolfContext()
        {
            // if (!_created) {
            //     _created = true;
            //     Database.EnsureDeleted();
            //     Database.EnsureCreated();
            // }
        }
    }
}