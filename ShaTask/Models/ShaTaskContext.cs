﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ShaTask.Models;

public partial class ShaTaskContext : DbContext
{
    public ShaTaskContext()
    {
    }

    public ShaTaskContext(DbContextOptions<ShaTaskContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Cashier> Cashiers { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }

    public virtual DbSet<InvoiceHeader> InvoiceHeaders { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=OZIL;Initial Catalog=ShaTask;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Branch>(entity =>
        {
            entity.Property(e => e.BranchName).HasDefaultValue("");

            entity.HasOne(d => d.City).WithMany(p => p.Branches)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Branches_Cities");
        });

        modelBuilder.Entity<Cashier>(entity =>
        {
            entity.Property(e => e.CashierName).HasDefaultValue("");

            entity.HasOne(d => d.Branch).WithMany(p => p.Cashiers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cashier_Branches");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.Property(e => e.CityName).HasDefaultValue("");
        });

        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.Property(e => e.ItemName).HasDefaultValue("");

            entity.HasOne(d => d.InvoiceHeader).WithMany(p => p.InvoiceDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceDetails_InvoiceHeader");
        });

        modelBuilder.Entity<InvoiceHeader>(entity =>
        {
            entity.Property(e => e.CustomerName).HasDefaultValue("");
            entity.Property(e => e.Invoicedate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Branch).WithMany(p => p.InvoiceHeaders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceHeader_Branches");

            entity.HasOne(d => d.Cashier).WithMany(p => p.InvoiceHeaders).HasConstraintName("FK_InvoiceHeader_Cashier");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}