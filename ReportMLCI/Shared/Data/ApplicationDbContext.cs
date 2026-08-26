using Microsoft.EntityFrameworkCore;
using ReportMLCI.Shared.Models;

namespace ReportMLCI.Shared.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public DbSet<MasterClause> MasterClauses { get; set; }
        public DbSet<MasterClausesDetails> MasterClausesDetails { get; set; }
        public DbSet<MasterClausesSubDetails> MasterClausesSubDetails { get; set; }
        public DbSet<MasterHeaderClause> MasterHeaderClauses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=ReportMLCI;Integrated Security=true;";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure MasterClause table
            modelBuilder.Entity<MasterClause>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasDefaultValueSql("NEWID()");
                entity.Property(e => e.ClauseCode).HasMaxLength(50);
                entity.Property(e => e.ClauseTitle).HasMaxLength(200);
                entity.Property(e => e.ClauseContent).HasColumnType("nvarchar(max)");
                entity.Property(e => e.IsActive).HasDefaultValue(true);
            });

            // Configure MasterClausesDetails table
            modelBuilder.Entity<MasterClausesDetails>(entity =>
            { 
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasDefaultValueSql("NEWID()");
                entity.Property(e => e.MasterClauseId).IsRequired();
                entity.Property(e => e.ClauseSubCode).HasMaxLength(50);
                entity.Property(e => e.ClauseSubTitle).HasMaxLength(200);
                entity.Property(e => e.ClauseSubContent).HasColumnType("nvarchar(max)");
                entity.Property(e => e.IsActive).HasDefaultValue(true);

                // Configure foreign key relationship
                entity.HasOne<MasterClause>()
                      .WithMany()
                      .HasForeignKey(d => d.MasterClauseId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure MasterClausesSubDetails table
            modelBuilder.Entity<MasterClausesSubDetails>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasDefaultValueSql("NEWID()");
                entity.Property(e => e.MasterClauseDetailId).IsRequired();
                entity.Property(e => e.SubDetailCode).HasMaxLength(10);
                entity.Property(e => e.SubDetailTitle).HasMaxLength(200);
                entity.Property(e => e.SubDetailContent).HasColumnType("nvarchar(max)");
                entity.Property(e => e.IsActive).HasDefaultValue(true);

                // Foreign Key
                entity.HasOne<MasterClausesDetails>()
                      .WithMany()
                      .HasForeignKey(d => d.MasterClauseDetailId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure MasterHeaderClause table
            modelBuilder.Entity<MasterHeaderClause>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasDefaultValueSql("NEWID()");
                entity.Property(e => e.ClauseHeaderCode).HasMaxLength(50);
                entity.Property(e => e.ClauseHeaderTitle).HasMaxLength(200);
                entity.Property(e => e.ClauseHeaderDescription).HasColumnType("nvarchar(max)");
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");
            });
        }

        public void EnsureDatabaseCreated()
        {
            this.Database.EnsureCreated();
        }
    }
}