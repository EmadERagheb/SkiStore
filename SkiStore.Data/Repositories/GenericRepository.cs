using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SkiStore.Domain.Contracts;
using System.Linq.Expressions;


namespace SkiStore.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly SkiStoreDbContext _context;
        private readonly IMapper _mapper;

        public GenericRepository(SkiStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<TResult>> GetAllAsync<TResult>(
            int pageNmuber,
            int pageSize,
            Expression<Func<T, bool>?> filter = null
            , Expression<Func<T, object>?> order = null
            , Expression<Func<T, object>?> orderDesc = null
            , params string[] properties)
        {
            IQueryable<T> query = _context.Set<T>();
            if (filter is not null)
            {
                query = query.Where(filter);
            }

            if (properties is not null)
            {
                foreach (var property in properties)
                {
                    query = query.Include(property);
                }
            }
            if (order is not null)
            {
                query = query.OrderBy(order);
            }
            if (orderDesc is not null)
            {
                query = query.OrderByDescending(orderDesc);
            }
            query = query.Skip(pageSize * (pageNmuber - 1)).Take(pageSize);
            return await query.ProjectTo<TResult>(_mapper.ConfigurationProvider).ToListAsync();
        }
        public async Task<TResult> GetAsync<TResult>(Expression<Func<T, bool>> filter, params string[] includedProperires)
        {
            IQueryable<T> query = _context.Set<T>().Where(filter);

            if (includedProperires is not null)
            {
                foreach (string property in includedProperires)
                {
                    query = query.Include(property);
                }
            }
            return await query.ProjectTo<TResult>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }
        public async Task<TResult> AddAsync<TSource, TResult>(TSource entity)
        {
            T item = _mapper.Map<T>(entity);
            await _context.AddAsync(item);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<TResult>(item);
            return result;

        }

        public async Task<int> UpdateAsync<TSource>(int id, TSource source)
        {
            T entity = await _context.Set<T>().FindAsync(id);
            if (entity is not null)
            {
                _mapper.Map(source, entity);
                _context.Update(entity);
                return await _context.SaveChangesAsync();
            }
            else
                return 0;

        }
        public async Task DeleteAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(Expression<Func<T, bool>> filter)
        {
            var entity = await GetAsync<T>(filter);
            return entity is not null;
        }

        public async Task<int> GetCountAsync(Expression<Func<T, bool>?> filter)
        {
            IQueryable<T> query = _context.Set<T>();
            if (filter is not null)
                query = query.Where(filter);
            return await query.CountAsync();
        }





    }
}
