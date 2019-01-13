using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyContact.DAL
{
    public interface IRepository<TEntity, in TKey> where TEntity : class 
    {
        Task<IEnumerable<TEntity>> GetAsync(string userId);
        Task<TEntity> GetAsync(string userId, TKey key);
        Task<bool> InsertAsync(string userId, TEntity entity);
        Task<bool> UpdateAsync(string userId, TEntity entity);
        Task<bool> DeleteAsync(string userId, TKey key);
        void Dispose();
    }
}
