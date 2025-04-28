using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HotelLibrary.Models;

public partial class HotelDbContext : DbContext
{
    public HotelDbContext()
    {
    }

    public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Room> Rooms { get; set; }
    public virtual DbSet<Reservation> Reservations { get; set; }
    public virtual DbSet<Customer> Customers { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=tcp:dat154-oblig4.database.windows.net,1433;Database=Hotel-project-DAT154;Authentication=Active Directory Interactive;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Danish_Norwegian_CI_AS");

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Roomnumber).HasName("pk_room");

            entity.ToTable("room");

            entity.Property(e => e.Roomnumber)
            .HasColumnName("roomnumber");

            entity.Property(e => e.Roomtype)
            .HasColumnName("roomtype");

            entity.Property(e => e.IsAvailable)
            .HasColumnName("isavailable");

            entity.Property(e => e.Cleaned)
            .HasColumnName("cleaned");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.ReservationId).HasName("pk_reservation");

            entity.ToTable("reservation");

            entity.Property(e => e.ReservationId)
            .HasColumnName("reservationid");

            entity.Property(e => e.RoomNumber)
            .HasColumnName("roomnumber");

            entity.HasOne(e => e.ReservedRoom).WithMany()
            .HasForeignKey(e => e.RoomNumber)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_room");

            entity.HasOne(e => e.Customer)
            .WithMany(c => c.Reservations)
            .HasForeignKey(e => e.CustomerId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_customer");

            entity.Property(e => e.InDate).HasColumnName("inDate");
            entity.Property(e => e.OutDate).HasColumnName("outDate");

        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("pk_customer");

            entity.ToTable("customer");

            entity.Property(e => e.CustomerId)
            .HasColumnName("customerid");

            entity.Property(e => e.CustomerName)
            .HasColumnName("customername");

            entity.Property(e => e.CustomerPassword)
            .HasColumnName("customerpassword");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
