using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpellCardsGenerator.Common.Exceptions;
using SpellCardsGenerator.Data.Data;
using SpellCardsGenerator.Data.Entities.Abstract;

namespace SpellCardsGenerator.Data.Repositories.Abstract;

public abstract class GlobalizedEntityRepository<TId, TEntity, TContext>
  where TId : IConvertible, IComparable, IComparable<TId>, IEquatable<TId>
  where TEntity : GlobalizedEntity<TId>
  where TContext : DbContext
{
  protected readonly ILogger<GlobalizedEntityRepository<TId, TEntity, TContext>> _logger;
  protected readonly SpellCardsDataContext _context;

  protected GlobalizedEntityRepository(
    ILogger<GlobalizedEntityRepository<TId, TEntity, TContext>> logger,
    SpellCardsDataContext context)
  {
    _logger = logger;
    _context = context;
  }
  
  public virtual async Task<TEntity[]> GetAll(string language, CancellationToken token = default)
  {
    DbSet<TEntity> entitiesSet = _context.Set<TEntity>();

    try
    {
      TEntity[] entities = await entitiesSet
        .Where(entity => entity.LanguageId == language)
        .ToArrayAsync(token);
    
      _logger.LogInformation("Retrieved all '{Type}' entities with language '{Language}', count = {Count}",
        entitiesSet.EntityType.Name, language, entities.Length);
      return entities;
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Unknown error occured when retrieving all '{Type}' entities with language '{Language}'",
        entitiesSet.EntityType.Name, language);
      throw;
    }
  }

  public virtual async Task<TEntity[]> GetAllWithIds(ICollection<TId> ids, string language, CancellationToken token = default)
  {
    DbSet<TEntity> entitiesSet = _context.Set<TEntity>();

    try
    {
      TEntity[] entities = await entitiesSet
        .Where(entity => entity.LanguageId == language)
        .Where(entity => ids.Contains(entity.Id))
        .ToArrayAsync(token);

      _logger.LogInformation("Retrieved all '{Type}' entities with specific IDs, IDs count = '{Count}', language = '{Language}'",
        entitiesSet.EntityType.Name, ids.Count, language);
      return entities;
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Unknown error occured when retrieving all '{Type}' entities with specific IDs and language '{Language}'!",
        entitiesSet.EntityType.Name, language);
      throw;
    }
  }

  public virtual async Task<TEntity> Get(TId id, string language, CancellationToken token = default)
  {
    DbSet<TEntity> entitiesSet = _context.Set<TEntity>();

    try
    {
      TEntity entity = await entitiesSet.FindAsync([id, language], cancellationToken: token)
        ?? throw new NotFoundException(typeof(TEntity), new { id, language });

      _logger.LogInformation("Retrieved '{Type}' entity with ID '{ID}' and language '{Language}'",
        entitiesSet.EntityType.Name, id, language);
      return entity;
    }
    catch (NotFoundException nfe)
    {
      _logger.LogError(nfe, "Entity '{Type}' with ID '{ID}' and language '{Language}' not found!",
        entitiesSet.EntityType.Name, id, language);
      throw;
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Unknown error occured when retrieving '{Type}' entity with ID '{ID}' and language '{Language}'!",
        entitiesSet.EntityType.Name, id, language);
      throw;
    }
  }
}