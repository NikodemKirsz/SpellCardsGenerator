using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpellCardsGenerator.Data.Entities.Abstract;
using SpellCardsGenerator.Data.Repositories.Abstract;

namespace SpellCardsGenerator.Data.Services.Abstract;

public abstract class EntityService<TId, TEntity, TContext>
  where TId : IConvertible, IComparable, IComparable<TId>, IEquatable<TId>
  where TEntity : Entity<TId>
  where TContext : DbContext
{
  protected readonly ILogger<EntityService<TId, TEntity, TContext>> _logger;
  protected readonly EntityRepository<TId, TEntity, TContext> _entityRepository;

  protected EntityService(
    ILogger<EntityService<TId, TEntity, TContext>> logger,
    EntityRepository<TId, TEntity, TContext> entityRepository)
  {
    _logger = logger;
    _entityRepository = entityRepository;
  }

  public virtual async Task<TEntity[]> GetAll(CancellationToken token = default)
  {
    TEntity[] entities = await _entityRepository.GetAll(token);

    _logger.LogInformation("Successfully got all '{Type}' entities, count = '{Count}'",
      typeof(TEntity).Name, entities.Length);
    return entities;
  }

  public virtual async Task<TEntity[]> GetAllWithIds(ICollection<TId> ids, CancellationToken token = default)
  {
    TEntity[] entities = await _entityRepository.GetAllWithIds(ids, token);

    _logger.LogInformation("Successfully got all '{Type}' entities with specific IDs, IDs count = '{Count}'",
      typeof(TEntity).Name, ids.Count);
    return entities;
  }

  public virtual async Task<TEntity> Get(TId id, CancellationToken token = default)
  {
    TEntity entity = await _entityRepository.Get(id, token);

    _logger.LogInformation("Successfully got '{Type}' entity with ID = '{ID}'",
      typeof(TEntity).Name, id);
    return entity;
  }

  public virtual async Task<int> AddRange(ICollection<TEntity> entities, CancellationToken token = default)
  {
    await _entityRepository.AddRange(entities, token: token);
    
    _logger.LogInformation("Added many '{Type}' entities, Count = '{Count}'",
      typeof(TEntity).Name, entities.Count);
    return entities.Count;
  }

  public virtual async Task<TEntity> Add(TEntity entity, CancellationToken token = default)
  {
    TEntity createdEntity = await _entityRepository.Add(entity, token: token);

    _logger.LogInformation("Added '{Type}' entity, ID = '{ID}'",
      typeof(TEntity).Name, createdEntity.Id);
    return createdEntity;
  }
}
