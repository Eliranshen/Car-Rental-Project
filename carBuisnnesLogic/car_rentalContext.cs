using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace carBuisnnesLogic
{
    public partial class car_rentalContext : DbContext
    {
        public car_rentalContext()
        {
        }

        public car_rentalContext(DbContextOptions<car_rentalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<CarField> CarFields { get; set; }
        public virtual DbSet<CarRentalField> CarRentalFields { get; set; }
        public virtual DbSet<CarType> CarTypes { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=ELIRANSH-LAP\\SQLEXPRESS01;Initial Catalog=car_rental;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Hebrew_CI_AS");

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.HasKey(e => e.BranchNumber);

                entity.ToTable("Branch");

                entity.Property(e => e.BranchNumber)
                    .ValueGeneratedNever()
                    .HasColumnName("branch_number");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .HasColumnName("address");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .HasColumnName("location");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<CarField>(entity =>
            {
                entity.HasKey(e => new { e.IdOfCar, e.IdOfField, e.CarType, e.CarNumber });

                entity.Property(e => e.IdOfCar).HasColumnName("id_of_car");

                entity.Property(e => e.IdOfField).HasColumnName("id_of_field");

                entity.Property(e => e.CarType)
                    .HasMaxLength(50)
                    .HasColumnName("car_type");

                entity.Property(e => e.CarNumber).HasColumnName("car_number");

                entity.Property(e => e.BranchNumber).HasColumnName("branch_number");

                entity.Property(e => e.CurrentKm).HasColumnName("current_km");

                entity.Property(e => e.FreeForRent)
                    .HasMaxLength(50)
                    .HasColumnName("free_for_rent");

                entity.Property(e => e.Picture).HasColumnName("picture");

                entity.Property(e => e.ProperToRent)
                    .HasMaxLength(50)
                    .HasColumnName("proper_to_rent");
            });

            modelBuilder.Entity<CarRentalField>(entity =>
            {
                entity.HasKey(e => new { e.StartDay, e.ReturnDate, e.CarNumber, e.UserId })
                    .HasName("PK__car_rent__9F8196C1E5783C6B");

                entity.ToTable("car_rental_fields");

                entity.Property(e => e.StartDay)
                    .HasColumnType("date")
                    .HasColumnName("start_day");

                entity.Property(e => e.ReturnDate)
                    .HasColumnType("date")
                    .HasColumnName("return_date");

                entity.Property(e => e.CarNumber).HasColumnName("car_number");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.ActualReturnDate)
                    .HasColumnType("date")
                    .HasColumnName("actual_return_date");

                entity.Property(e => e.No)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("no");
            });

            modelBuilder.Entity<CarType>(entity =>
            {
                entity.HasKey(e => e.IdOfCar);

                entity.Property(e => e.IdOfCar)
                    .ValueGeneratedNever()
                    .HasColumnName("id_of_car");

                entity.Property(e => e.CarModel)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("car_model");

                entity.Property(e => e.DailyCost).HasColumnName("daily_cost");

                entity.Property(e => e.DailyDelayCost).HasColumnName("daily_delay_cost");

                entity.Property(e => e.Gear)
                    .HasMaxLength(50)
                    .HasColumnName("gear");

                entity.Property(e => e.ManufactureName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("manufacture_name");

                entity.Property(e => e.YearOfProduction).HasColumnName("year_of_production");
            });

            modelBuilder.Entity<UserDetail>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.UserName });

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasColumnName("user_name");

                entity.Property(e => e.Bdate)
                    .HasColumnType("date")
                    .HasColumnName("bdate");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.FullName)
                    .HasMaxLength(50)
                    .HasColumnName("full_name");

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .HasColumnName("gender");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Picture).HasColumnName("picture");

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .HasColumnName("role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
