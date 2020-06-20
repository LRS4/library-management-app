using System;
using ILS.Library.DataAccess.SecurityDb.Entities.Asset;
using ILS.Library.DataAccess.SecurityDb.Entities.Branch;
using ILS.Library.DataAccess.SecurityDb.Entities.Comms;
using ILS.Library.DataAccess.SecurityDb.Entities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ILS.Library.DataAccess.SecurityDb.Entities
{
    public partial class ILSContext : IdentityDbContext<ApplicationUser>
    {
        #region Constructors

        public ILSContext()
        {
        }

        public ILSContext(DbContextOptions<ILSContext> options)
            : base(options)
        {
        }

        #endregion

        #region DbSets

        public virtual DbSet<BranchDetails> BranchDetails { get; set; }
        public virtual DbSet<BranchHours> BranchHours { get; set; }
        public virtual DbSet<Checkout> Checkout { get; set; }
        public virtual DbSet<CheckoutHistory> CheckoutHistory { get; set; }
        public virtual DbSet<Hold> Hold { get; set; }
        public virtual DbSet<LibraryAsset> LibraryAsset { get; set; }
        public virtual DbSet<LibraryCard> LibraryCard { get; set; }
        public virtual DbSet<Notices> Notices { get; set; }
        public virtual DbSet<Patron> Patron { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUser { get; set; }
        public virtual DbSet<Status> Status { get; set; }

        #endregion

        #region Stored procedures

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ILS");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BranchDetails>(entity =>
            {
                entity.HasKey(e => e.BranchId);

                entity.ToTable("BranchDetails", "Branch");

                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.OpenDate).HasColumnType("datetime");

                entity.Property(e => e.Telephone).HasMaxLength(50);
            });

            modelBuilder.Entity<BranchHours>(entity =>
            {
                entity.ToTable("BranchHours", "Branch");

                entity.Property(e => e.BranchId).HasComment("Identifier for the branch in which the hours belong to.");

                entity.Property(e => e.CloseTime).HasComment("The closing time of the branch.");

                entity.Property(e => e.DayOfWeek).HasComment("The days of the week that the branch is open.");

                entity.Property(e => e.OpenTime).HasComment("The hour at which the branch opens.");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.BranchHours)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK_BranchHours_BranchId");
            });

            modelBuilder.Entity<Checkout>(entity =>
            {
                entity.ToTable("Checkout", "Asset");

                entity.Property(e => e.Since).HasColumnType("datetime");

                entity.Property(e => e.Until).HasColumnType("datetime");

                entity.HasOne(d => d.LibraryAsset)
                    .WithMany(p => p.Checkout)
                    .HasForeignKey(d => d.LibraryAssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Checkout_LibraryAssetId");

                entity.HasOne(d => d.LibraryCard)
                    .WithMany(p => p.Checkout)
                    .HasForeignKey(d => d.LibraryCardId)
                    .HasConstraintName("FK_Checkout_LibraryCardId");
            });

            modelBuilder.Entity<CheckoutHistory>(entity =>
            {
                entity.ToTable("CheckoutHistory", "Asset");

                entity.Property(e => e.CheckedIn).HasColumnType("datetime");

                entity.Property(e => e.CheckedOut).HasColumnType("datetime");

                entity.HasOne(d => d.LibraryAsset)
                    .WithMany(p => p.CheckoutHistory)
                    .HasForeignKey(d => d.LibraryAssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CheckoutHistory_LibraryAssetId");

                entity.HasOne(d => d.LibraryCard)
                    .WithMany(p => p.CheckoutHistory)
                    .HasForeignKey(d => d.LibraryCardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CheckoutHistory_LibraryCardId");
            });

            modelBuilder.Entity<Hold>(entity =>
            {
                entity.ToTable("Hold", "Asset");

                entity.Property(e => e.HoldPlaced).HasColumnType("datetime");

                entity.HasOne(d => d.LibraryAsset)
                    .WithMany(p => p.Hold)
                    .HasForeignKey(d => d.LibraryAssetId)
                    .HasConstraintName("FK_Hold_LibraryAssetId");

                entity.HasOne(d => d.LibraryCard)
                    .WithMany(p => p.Hold)
                    .HasForeignKey(d => d.LibraryCardId)
                    .HasConstraintName("FK_Hold_LibraryCardId");
            });

            modelBuilder.Entity<LibraryAsset>(entity =>
            {
                entity.ToTable("LibraryAsset", "Asset");

                entity.Property(e => e.Author).HasMaxLength(256);

                entity.Property(e => e.Cost).HasColumnType("decimal(19, 4)");

                entity.Property(e => e.DeweyIndex).HasMaxLength(256);

                entity.Property(e => e.Director).HasMaxLength(256);

                entity.Property(e => e.Discriminator)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.ISBN)
                    .HasColumnName("ISBN")
                    .HasMaxLength(256);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.LibraryCard)
                    .WithMany(p => p.LibraryAsset)
                    .HasForeignKey(d => d.LibraryCardId)
                    .HasConstraintName("FK_LibraryAsset_LibraryCardId");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.LibraryAsset)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_LibraryAsset_LocationId");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.LibraryAsset)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LibraryAsset_StatusId");
            });

            modelBuilder.Entity<LibraryCard>(entity =>
            {
                entity.ToTable("LibraryCard", "Branch");

                entity.Property(e => e.LibraryCardId).HasComment("The surrogate primary key for this table.");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasComment("The date and time when the library card was issued.");

                entity.Property(e => e.Fees)
                    .HasColumnType("decimal(19, 4)")
                    .HasComment("Any outstanding fees on the library card.");
            });

            modelBuilder.Entity<Notices>(entity =>
            {
                entity.HasKey(e => e.NoticeId);

                entity.ToTable("Notices", "Comms");

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.NoticeId).ValueGeneratedOnAdd();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<Patron>(entity =>
            {
                entity.ToTable("Patron", "Users");

                entity.Property(e => e.PatronId).HasComment("Surrogate primary key for this table.");

                entity.Property(e => e.Address).HasComment("The address of the patron.");

                entity.Property(e => e.DateOfBirth)
                    .IsRequired()
                    .HasComment("The date of birth of the patron.");

                entity.Property(e => e.FirstName).HasComment("The first name of the patron.");

                entity.Property(e => e.Gender).HasMaxLength(50);

                entity.Property(e => e.LastName).HasComment("The last name of the patron.");

                entity.Property(e => e.TelephoneNumber)
                    .HasMaxLength(50)
                    .HasComment("The contact telephone number of the patron.");

                entity.HasOne(d => d.HomeLibraryBranch)
                    .WithMany(p => p.Patron)
                    .HasForeignKey(d => d.HomeLibraryBranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Patron_HomeLibraryBranchId");

                entity.HasOne(d => d.LibraryCard)
                    .WithMany(p => p.Patron)
                    .HasForeignKey(d => d.LibraryCardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Patron_LibraryCardId");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status", "Asset");

                entity.Property(e => e.StatusId).HasComment("The surrogate primary key for this table.");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasComment("The description of the asset status");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("The name of the status.");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        #endregion
    }
}
