﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RequestHandler.Models;

public partial class RequestHandlerContext : DbContext
{
    public RequestHandlerContext()
    {
    }

    public RequestHandlerContext(DbContextOptions<RequestHandlerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppintmentId).HasName("pk_appintment");

            entity.Property(e => e.AppintmentId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("appintment_id");
            entity.Property(e => e.Approval).HasColumnName("approval");
            entity.Property(e => e.DateApprove)
                .HasColumnType("datetime")
                .HasColumnName("date_approve");
            entity.Property(e => e.DateCreate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_create");
            entity.Property(e => e.DateFix)
                .HasColumnType("datetime")
                .HasColumnName("date_fix");
            entity.Property(e => e.DiscriptionProblem).HasColumnName("discription_problem");
            entity.Property(e => e.Document).HasColumnName("document");
            entity.Property(e => e.Master).HasColumnName("master");
            entity.Property(e => e.Place).HasColumnName("place");
            entity.Property(e => e.Problem)
                .HasMaxLength(50)
                .HasColumnName("problem");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.User).HasColumnName("user");

            entity.HasOne(d => d.ApprovalNavigation).WithMany(p => p.AppointmentApprovalNavigations)
                .HasForeignKey(d => d.Approval)
                .HasConstraintName("fk_approval_to_appintment");

            entity.HasOne(d => d.MasterNavigation).WithMany(p => p.AppointmentMasterNavigations)
                .HasForeignKey(d => d.Master)
                .HasConstraintName("fk_master_to_appintment");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.Status)
                .HasConstraintName("fk_status_to_appintment");

            entity.HasOne(d => d.UserNavigation).WithMany(p => p.AppointmentUserNavigations)
                .HasForeignKey(d => d.User)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_to_appintment");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("pk_document");

            entity.HasIndex(e => e.Title, "un_document_title").IsUnique();

            entity.Property(e => e.DocumentId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("document_id");
            entity.Property(e => e.Path).HasColumnName("path");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("pk_role");

            entity.HasIndex(e => e.Title, "un_role_title").IsUnique();

            entity.Property(e => e.RoleId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("role_id");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("pk_status");

            entity.ToTable("Status");

            entity.HasIndex(e => e.Title, "un_status_title").IsUnique();

            entity.Property(e => e.StatusId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("status_id");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("pk_user");

            entity.ToTable("User");

            entity.HasIndex(e => e.Login, "un_user_login").IsUnique();

            entity.Property(e => e.UserId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("user_id");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .HasColumnName("login");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .HasColumnName("surname");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Role)
                .HasConstraintName("fk_role_to_user");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}