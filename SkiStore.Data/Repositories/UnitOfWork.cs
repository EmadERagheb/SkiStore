using AutoMapper;
using SkiStore.Domain.Contracts;
using SkiStore.Domain.Models;
using System.Collections;

namespace SkiStore.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SkiStoreDbContext _context;
        private readonly IMapper _mapper;
        private Hashtable _repositories;

        public UnitOfWork(SkiStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> CompleteAsync()
        {
      return  await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel
        {
            if (_repositories is null) _repositories = new Hashtable();
            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type.Name))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(type), _context, _mapper);
                _repositories[type.Name] = repositoryInstance;
            }
            return (IGenericRepository<TEntity>)_repositories[type.Name];
        }







    }
}
