using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentApi.Models;

namespace StudentApi.Repository
{
    public class UnitOfWork
    {
        private readonly StudentContext _context;

        public IRepository<Student> Student { get; }
        public IRepository<Classroom> Classroom { get; }

        public UnitOfWork(DbContextOptions<StudentContext> options)
        {
            _context = new StudentContext(options);
            Student = new Repository<Student>(_context);
            Classroom = new Repository<Classroom>(_context);
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
