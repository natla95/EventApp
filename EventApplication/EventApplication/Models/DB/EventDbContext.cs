namespace EventApplication.Models.DB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EventDbContext : DbContext
    {
        public EventDbContext()
            : base("name=EventDbContext")
        {
        }

        public virtual DbSet<EventOption> EventOptions { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<GuestOption> GuestOptions { get; set; }
        public virtual DbSet<Guest> Guests { get; set; }
        public virtual DbSet<InvitationOption> InvitationOptions { get; set; }
        public virtual DbSet<Invitation> Invitations { get; set; }
        public virtual DbSet<Option> Options { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserEvent> UserEvents { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .HasMany(e => e.EventOptions)
                .WithRequired(e => e.Event)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Invitations)
                .WithRequired(e => e.Event)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.UserEvents)
                .WithRequired(e => e.Event)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Guest>()
                .HasMany(e => e.GuestOptions)
                .WithRequired(e => e.Guest)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Invitation>()
                .HasMany(e => e.Guests)
                .WithRequired(e => e.Invitation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Invitation>()
                .HasMany(e => e.InvitationOptions)
                .WithRequired(e => e.Invitation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Option>()
                .HasMany(e => e.EventOptions)
                .WithRequired(e => e.Option)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Option>()
                .HasMany(e => e.GuestOptions)
                .WithRequired(e => e.Option)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Option>()
                .HasMany(e => e.InvitationOptions)
                .WithRequired(e => e.Option)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Invitations)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserEvents)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
