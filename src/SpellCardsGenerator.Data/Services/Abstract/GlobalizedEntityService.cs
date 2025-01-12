using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpellCardsGenerator.Data.Entities.Abstract;
using SpellCardsGenerator.Data.Repositories.Abstract;

namespace SpellCardsGenerator.Data.Services.Abstract;

public class GlobalizedEntityService<TId, TData, TContent, TModel, TContext>
  where TId : IConvertible, IComparable, IComparable<TId>, IEquatable<TId>
  where TData : Entity<TId>
  where TContent : GlobalizedEntity<TId>
  where TContext : DbContext
{
  protected readonly ILogger<GlobalizedEntityService<TId, TData, TContent, TModel, TContext>> _logger;
  protected readonly EntityRepository<TId, TData, TContext> _dataRepository;
  protected readonly GlobalizedEntityRepository<TId, TContent, TContext> _contentRepository;

  public GlobalizedEntityService(
    ILogger<GlobalizedEntityService<TId, TData, TContent, TModel, TContext>> logger,
    EntityRepository<TId, TData, TContext> dataRepository,
    GlobalizedEntityRepository<TId, TContent, TContext> contentRepository)
  {
    _logger = logger;
    _dataRepository = dataRepository;
    _contentRepository = contentRepository;
  }

  public virtual async Task<TData[]> GetAllData(CancellationToken token = default)
  { 
    TData[] datas = await _dataRepository.GetAll(token);

    _logger.LogInformation("Successfully got all '{Type}' entities, count = '{Count}'",
      typeof(TContent).Name, datas.Length);
    return datas;
  }

  public virtual async Task<TContent[]> GetAllContent(string language, CancellationToken token = default)
  {
    TContent[] contents = await _contentRepository.GetAll(language, token);

    _logger.LogInformation("Successfully got all '{Type}' entities for language '{Language}', count = '{Count}'",
      typeof(TContent).Name, language, contents.Length);
    return contents;
  }
  
  public virtual async Task<TData[]> GetAllDataWithIds(ICollection<TId> ids, CancellationToken token = default)
  {
    TData[] datas = await _dataRepository.GetAllWithIds(ids, token);

    _logger.LogInformation("Successfully got all '{Type}' entities with specific IDs, IDs count = '{Count}'",
      typeof(TContent).Name, ids.Count);
    return datas;
  }

  public virtual async Task<TContent[]> GetAllContentWithIds(ICollection<TId> ids, string language, CancellationToken token = default)
  {
    TContent[] content = await _contentRepository.GetAllWithIds(ids, language, token);

    _logger.LogInformation("Successfully got all '{Type}' entities with specific IDs for language '{Language}', IDs count = '{Count}'",
      typeof(TContent).Name, language, ids.Count);
    return content;
  }

  public virtual Task<TData> GetData(TId id, CancellationToken token = default) =>
    base.Get(id, token);

  public virtual async Task<TContent> GetContent(TId id, string language, CancellationToken token = default)
  {
    TContent content = await _contentRepository.Get(id, language, token);

    _logger.LogInformation("Successfully got '{Type}' entity with ID = '{ID}' for language '{Language}'",
      typeof(TContent).Name, id, language);
    return content;
  }

  public virtual Task<int> AddDataRange(ICollection<TData> datas, CancellationToken token = default) =>
    base.AddRange(datas, token);

  public virtual async Task<int> AddContentRange(ICollection<TContent> contents, CancellationToken token = default)
  {
    await _contentRepository.AddRange(contents, token: token);
    
    _logger.LogInformation("Added many '{Type}' entities, Count = '{Count}'",
      typeof(TContent).Name, contents.Count);
    return contents.Count;
  }

  public virtual Task<TData> AddData(TData data, CancellationToken token = default) =>
    base.Add(data, token);

  public virtual async Task<TContent> AddContent(TContent content, CancellationToken token = default)
  {
    TContent createdContent = await _contentRepository.Add(content, token: token);

    _logger.LogInformation("Added '{Type}' entity, ID = '{ID}'",
      typeof(TContent).Name, createdContent.Id);
    return createdContent;
  }
}