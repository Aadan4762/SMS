using SMS.Entities;

namespace SMS.Interface;

public interface IUnitOfWork
{
      IRepository<School> Schools { get; }
      IRepository<Teacher> Teachers { get; }
      IRepository<Student> Students { get; }
      Task<int> CommitAsync();
}