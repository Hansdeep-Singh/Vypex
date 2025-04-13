using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vypex.CodingChallenge.API.Repositories
{
    public interface IRepository<T>
    {
        void Update(T entity);
        void UpdateAll(List<T> entity);
        Task AddAsync(T entity);
        Task AddRangeAsync(List<T> entities);
        void Delete(T entity);
        void DeleteAll();
        void DeleteRange(List<T> entities);
        void Entry(T entity);
        void EntryAdded(T entity);
        void EntryDetached(T entity);
        void EntryUnchanged(T entity);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> GetAllQuery();
        Task<T> GetOneGuidIdAsync(Guid? id);
        Task<T> GetOneIntIdAsync(int id);
        Task<T> GetOneStringIdAsync(string? id);
        Task SaveAsync();
    }
}
