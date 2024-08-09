using SMS.Entities;

namespace SMS.Interface;

public interface IUnitOfWork
{
      IRepository<School> Schools { get; }
      IRepository<Teacher> Teachers { get; }
      Task<int> CommitAsync();
}