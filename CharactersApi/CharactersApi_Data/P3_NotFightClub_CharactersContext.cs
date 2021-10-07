using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CharactersApi_Data
{
  public partial class P3_NotFightClub_CharactersContext : DbContext
  {
    public P3_NotFightClub_CharactersContext()
    {
    }

    public P3_NotFightClub_CharactersContext(DbContextOptions<P3_NotFightClub_CharactersContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Character> Characters { get; set; }
    public virtual DbSet<Trait> Traits { get; set; }
    public virtual DbSet<Weapon> Weapons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=P3_NotFightClub_Characters;Trusted_Connection=true");
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

      modelBuilder.Entity<Character>(entity =>
      {
        entity.ToTable("Character");

        entity.Property(e => e.Baseform).HasMaxLength(100);

        entity.Property(e => e.Name)
                  .IsRequired()
                  .HasMaxLength(100);

        entity.HasOne(d => d.Trait)
                  .WithMany(p => p.Characters)
                  .HasForeignKey(d => d.TraitId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__Character__Trait__36B12243");

        entity.HasOne(d => d.Weapon)
                  .WithMany(p => p.Characters)
                  .HasForeignKey(d => d.WeaponId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__Character__Weapo__37A5467C");
      });

      modelBuilder.Entity<Trait>(entity =>
      {
        entity.ToTable("Trait");

        entity.Property(e => e.Description).HasMaxLength(300);
      });

      modelBuilder.Entity<Weapon>(entity =>
      {
        entity.ToTable("Weapon");

        entity.Property(e => e.Description).HasMaxLength(300);
      });

      OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
  }
}
