using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Model
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        public SchoolContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost; Database=SchoolManagement; User Id=sa; Password=mamed2001");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Student Related Model Builders

            modelBuilder.Entity<Student>()
                .Property(t => t.Name)
                .HasMaxLength(40)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .Property(t => t.Surname)
                .HasMaxLength(40)
                .IsRequired();


            modelBuilder.Entity<Student>()
                .Property(t => t.BirthDate)
                .HasDefaultValue(DateTime.Now);


            modelBuilder.Entity<Student>()
                .Property(t => t.RegisterDate)
                .HasDefaultValue(DateTime.Now);


            #endregion


            #region Teacher Related Model Builders

            modelBuilder.Entity<Teacher>()
                .Property(t => t.Name)
                .HasMaxLength(40)
                .IsRequired();


            modelBuilder.Entity<Teacher>()
                .Property(t => t.Surname)
                .HasMaxLength(40)
                .IsRequired();

            modelBuilder.Entity<Teacher>()
                .Property(t => t.License)
                .HasMaxLength(50)
                .IsRequired();


            modelBuilder.Entity<Teacher>()
                .Property(t => t.BirthDate)
                .HasDefaultValue(DateTime.Now);

            #endregion


            #region Relationships

            modelBuilder.Entity<Student>()
                .HasMany(s => s.Teachers)
                .WithMany(t => t.Students)
                .UsingEntity<Dictionary<string, object>>
                (
                    "StudentTeachers",
                    j => j.HasOne<Teacher>()
                            .WithMany()
                            .HasForeignKey("TeacherId"),

                    j => j.HasOne<Student>()
                            .WithMany()
                            .HasForeignKey("StudentId")
                );

            #endregion
        }
    }
}