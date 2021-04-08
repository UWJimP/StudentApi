using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StudentApi.Models;

namespace StudentApi
{
    public class StudentContext : DbContext
    {
        public DbSet<Student> Students;
        public DbSet<Classroom> Classrooms;
        
        public StudentContext(DbContextOptions<StudentContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Student>().HasKey(student => student.EntityID);

            builder.Entity<Classroom>().HasKey(classroom => classroom.EntityID);
            builder.Entity<Classroom>().HasMany(classroom => classroom.Students);
            
            SeedData(builder);
        }

        private void SeedData(ModelBuilder builder)
        {
            Classroom class1 = new Classroom() { EntityID = 1, ClassCode = "COM101" };
            Classroom class2 = new Classroom() { EntityID = 2, ClassCode = "COM201" };
            Classroom class3 = new Classroom() { EntityID = 3, ClassCode = "ENG101" };
            Classroom class4 = new Classroom() { EntityID = 4, ClassCode = "CHI101" };
            Classroom class5 = new Classroom() { EntityID = 5, ClassCode = "MAT101" };
            Classroom class6 = new Classroom() { EntityID = 6, ClassCode = "MAT201" };

            builder.Entity<Classroom>().HasData(new List<Classroom>(){
                class1, class2, class3, class4, class5, class6
            });
            builder.Entity<Student>().HasData(new List<Student>(){
                new Student(){ 
                    EntityID = 1, 
                    FirstName = "Moka", 
                    LastName = "Mogami", 
                    Age = 16
                },
                new Student(){ 
                    EntityID = 2, 
                    FirstName = "Mary", 
                    LastName = "Jane", 
                    Age = 17
                },
                new Student(){ 
                    EntityID = 3, 
                    FirstName = "Michael", 
                    LastName = "Vodka", 
                    Age = 18
                },
            });
        }
    }
}
