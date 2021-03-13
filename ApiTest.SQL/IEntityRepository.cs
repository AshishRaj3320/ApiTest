using ApiTest.SQL.DBModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiTest.SQL
{
    public interface IEntityRepository<TEntity> where TEntity : BaseEntity 
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
    }
}
