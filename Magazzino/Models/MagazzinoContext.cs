using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Magazzino.Models
{
    public partial class MagazzinoContext : DbContext
    {
        public MagazzinoContext()
        {
        }

        public MagazzinoContext(DbContextOptions<MagazzinoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Anagrafica> Anagrafica { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-HT0MQRV;Database=Magazzino;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anagrafica>(entity =>
            {
                entity.Property(e => e.Codice)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Descrizione)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Note).HasMaxLength(250);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
