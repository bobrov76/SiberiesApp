using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebAPI
{
    public partial class testDbContext : DbContext
    {
        public testDbContext()
        {
        }

        public testDbContext(DbContextOptions<testDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Projects> Projects { get; set; }
        public virtual DbSet<ProjectsEmployees> ProjectsEmployees { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }
        public virtual DbSet<TasksEmployees> TasksEmployees { get; set; }
        public virtual DbSet<TasksProgects> TasksProgects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-5LAKVSV;Initial Catalog=testDb;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.IdEmployee);

                entity.ToTable("employees");

                entity.Property(e => e.IdEmployee).HasColumnName("idEmployee");

                entity.Property(e => e.AuthRol)
                    .IsRequired()
                    .HasColumnName("authRol")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ComandRol)
                    .IsRequired()
                    .HasColumnName("comandRol")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NameEmployee)
                    .IsRequired()
                    .HasColumnName("nameEmployee")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PatronimicEmployee)
                    .IsRequired()
                    .HasColumnName("patronimicEmployee")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SurnameEmployee)
                    .IsRequired()
                    .HasColumnName("surnameEmployee")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Projects>(entity =>
            {
                entity.ToTable("projects");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EndData)
                    .HasColumnName("endData")
                    .HasColumnType("date");

                entity.Property(e => e.FilePath)
                    .HasColumnName("filePath")
                    .IsUnicode(false);

                entity.Property(e => e.NameExecutingCompany)
                    .IsRequired()
                    .HasColumnName("nameExecutingCompany")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NameProject)
                    .IsRequired()
                    .HasColumnName("nameProject")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NameСustomerCompany)
                    .IsRequired()
                    .HasColumnName("nameСustomerCompany")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Priority).HasColumnName("priority");

                entity.Property(e => e.StartData)
                    .HasColumnName("startData")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<ProjectsEmployees>(entity =>
            {
                entity.HasKey(e => e.IdTasksEmployees);

                entity.ToTable("projectsEmployees");

                entity.Property(e => e.IdTasksEmployees).HasColumnName("idTasksEmployees");

                entity.Property(e => e.IdEmployee).HasColumnName("idEmployee");

                entity.Property(e => e.IdProject).HasColumnName("idProject");
            });

            modelBuilder.Entity<Tasks>(entity =>
            {
                entity.HasKey(e => e.Idtask);

                entity.ToTable("tasks");

                entity.Property(e => e.Idtask).HasColumnName("idtask");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnName("comment")
                    .IsUnicode(false);

                entity.Property(e => e.IdExecutor).HasColumnName("idExecutor");

                entity.Property(e => e.IdSupervisor).HasColumnName("idSupervisor");

                entity.Property(e => e.NameTask)
                    .IsRequired()
                    .HasColumnName("nameTask")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Priority).HasColumnName("priority");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TasksEmployees>(entity =>
            {
                entity.HasKey(e => e.IdTasksEmployees);

                entity.ToTable("tasksEmployees");

                entity.Property(e => e.IdTasksEmployees)
                    .HasColumnName("idTasksEmployees")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdEmployee).HasColumnName("idEmployee");

                entity.Property(e => e.IdProject).HasColumnName("idProject");
            });

            modelBuilder.Entity<TasksProgects>(entity =>
            {
                entity.HasKey(e => e.IdTasksProgects);

                entity.ToTable("tasksProgects");

                entity.Property(e => e.IdTasksProgects)
                    .HasColumnName("idTasksProgects")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdProgects).HasColumnName("idProgects");

                entity.Property(e => e.IdTasks).HasColumnName("idTasks");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
