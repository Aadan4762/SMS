using SMS.Data;
using SMS.Entities;
using SMS.Interface;
using System.Threading.Tasks;

namespace SMS.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IRepository<School> _schoolRepository;
        private IRepository<Teacher> _teacherRepository;
        private IRepository<Student> _studentRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<School> Schools 
        {
            get { return _schoolRepository ??= new Repository<School>(_context); }
        }

        public IRepository<Teacher> Teachers 
        {
            get { return _teacherRepository ??= new Repository<Teacher>(_context); }
        }

        public IRepository<Student> Students 
        {
            get { return _studentRepository ??= new Repository<Student>(_context); }
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}