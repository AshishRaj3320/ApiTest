using ApiTest.SQL.Data;
using ApiTest.SQL.DBModels;
using System;
using System.Threading.Tasks;

namespace ApiTest.SQL
{
    public class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : BaseEntity 
    {
        protected readonly PaymentProcessContext _repositoryPatternPaymentContext;

        public EntityRepository(PaymentProcessContext repositoryPatternPaymentContext)
        {
            _repositoryPatternPaymentContext = repositoryPatternPaymentContext;
        }
        
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                entity.UniqueId = Guid.NewGuid();
                entity.CreatedDate = System.DateTime.Now;
                entity.IsActive = true;
                entity.IsDeleted = false;

                await _repositoryPatternPaymentContext.AddAsync(entity);
                await _repositoryPatternPaymentContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                _repositoryPatternPaymentContext.Update(entity);
                await _repositoryPatternPaymentContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }
        }
    }
}
