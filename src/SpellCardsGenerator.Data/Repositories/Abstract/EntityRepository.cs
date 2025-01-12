using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using SpellCardsGenerator.Common.Exceptions;
using SpellCardsGenerator.Data.Data;
using SpellCardsGenerator.Data.Entities.Abstract;

namespace SpellCardsGenerator.Data.Repositories.Abstract;

public abstract class EntityRepository<TId, TEntity, TContext>
  where TId : IConvertible, IComparable, IComparable<TId>, IEquatable<TId>
  where TEntity : Entity<TId>
  where TContext : DbContext
{
  protected readonly ILogger<EntityRepository<TId, TEntity, TContext>> _logger;
  protected readonly SpellCardsDataContext _context;
  
  protected EntityRepository(
    ILogger<EntityRepository<TId, TEntity, TContext>> logger,
    SpellCardsDataContext context)
  {
    _logger = logger;
    _context = context;
  }

  public async Task<TEntity[]> GetAll(CancellationToken token = default)
  {
    DbSet<TEntity> entitiesSet = _context.Set<TEntity>();

    try
    {
      TEntity[] entities = await entitiesSet.ToArrayAsync(token);

      _logger.LogInformation("Retrieved all '{Type}' entities, count = {Count}",
        entitiesSet.EntityType.Name, entities.Length);
      return entities;
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Unknown error occured when retrieving all '{Type}' entities!",
        entitiesSet.EntityType.Name);
      throw;
    }
  }

  public async Task<TEntity[]> GetAllWithIds(ICollection<TId> ids, CancellationToken token = default)
  {
    DbSet<TEntity> entitiesSet = _context.Set<TEntity>();

    try
    {
      TEntity[] entities = await entitiesSet
        .Where(entity => ids.Contains(entity.Id))
        .ToArrayAsync(token);

      _logger.LogInformation("Retrieved all '{Type}' entities with specific IDs, IDs count = '{Count}'",
        entitiesSet.EntityType.Name, ids.Count);
      return entities;
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Unknown error occured when retrieving all '{Type}' entities with specific IDs!",
        entitiesSet.EntityType.Name);
      throw;
    }
  }

  public async Task<TEntity> Get(TId id, CancellationToken token = default)
  {
    DbSet<TEntity> entitiesSet = _context.Set<TEntity>();

    try
    {
      TEntity entity = await entitiesSet.FindAsync([id], cancellationToken: token)
        ?? throw new NotFoundException(typeof(TEntity), new { id });

      _logger.LogInformation("Retrieved '{Type}' entity with ID '{ID}'",
        entitiesSet.EntityType.Name, id);
      return entity;
    }
    catch (NotFoundException nfe)
    {
      _logger.LogError(nfe, "Entity '{Type}' with ID '{ID}' not found!",
        entitiesSet.EntityType.Name, id);
      throw;
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Unknown error occured when retrieving '{Type}' entity with ID '{ID}'!",
        entitiesSet.EntityType.Name, id);
      throw;
    }
  }
  
  public async Task AddRange(ICollection<TEntity> entities, bool save = true, CancellationToken token = default)
  {
    DbSet<TEntity> entitiesSet = _context.Set<TEntity>();

    try
    {
      await entitiesSet.AddRangeAsync(entities, token);

      if (save)
        await _context.SaveChangesAsync(token);
      
      _logger.LogInformation("Inserted many '{Type}' entities, Count = '{Count}'",
        entitiesSet.EntityType.Name, entities.Count);
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Unknown error occured when adding many '{Type}'!",
        entitiesSet.EntityType.Name);
      throw;
    }
  }

  public async Task<TEntity> Add(TEntity entity, bool save = true, CancellationToken token = default)
  {
    DbSet<TEntity> entitiesSet = _context.Set<TEntity>();

    try
    {
      EntityEntry<TEntity> entityEntry = await entitiesSet.AddAsync(entity, token);

      if (save)
        await _context.SaveChangesAsync(token);
      
      _logger.LogInformation("Inserted '{Type}' entity, ID = '{ID}'",
        entitiesSet.EntityType.Name, entityEntry.Entity.Id);
      return entityEntry.Entity;
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Unknown error occured when adding '{Type}'!",
        entitiesSet.EntityType.Name);
      throw;
    }
  }
}